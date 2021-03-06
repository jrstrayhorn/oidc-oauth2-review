using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Marvin.IDP.Entities;
using Marvin.IDP.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerHost.Quickstart.UI
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class ExternalController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly ILogger<ExternalController> _logger;
        private readonly IEventService _events;
        private readonly ILocalUserService _localUserService;
        private readonly Dictionary<string, string> _facebookClaimTypeMap =
            new Dictionary<string, string>()
            {
                {
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname",
                    JwtClaimTypes.GivenName  
                },
                {
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname",
                    JwtClaimTypes.FamilyName
                },
                {
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
                    JwtClaimTypes.Email
                }
            };

        private readonly Dictionary<string, string> _oktaClaimTypeMap =
            new Dictionary<string, string>()
            {
                {
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname",
                    JwtClaimTypes.GivenName  
                },
                {
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname",
                    JwtClaimTypes.FamilyName
                },
                {
                    "preferred_username",
                    JwtClaimTypes.Email
                }
            };

        public ExternalController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IEventService events,
            ILogger<ExternalController> logger,
            ILocalUserService localUserService
            )
        {
            _localUserService = localUserService ??
                throw new ArgumentNullException(nameof(localUserService));
            _interaction = interaction;
            _clientStore = clientStore;
            _logger = logger;
            _events = events;
        }

        /// <summary>
        /// initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet]
        public IActionResult Challenge(string scheme, string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl)) returnUrl = "~/";

            // validate returnUrl - either it is a valid OIDC URL or back to a local page
            if (Url.IsLocalUrl(returnUrl) == false && _interaction.IsValidReturnUrl(returnUrl) == false)
            {
                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }
            
            // start challenge and roundtrip the return URL and scheme 
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(Callback)), 
                Items =
                {
                    { "returnUrl", returnUrl }, 
                    { "scheme", scheme },
                }
            };

            // the scheme triggers the middle ware necessary to "answer" this challenge
            return Challenge(props, scheme);
            
        }

        /// <summary>
        /// Post processing of external authentication
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Callback()
        {
            // read external identity from the temporary cookie
            var result = await HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
            if (result?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                var externalClaims = result.Principal.Claims.Select(c => $"{c.Type}: {c.Value}");
                _logger.LogDebug("External claims: {@claims}", externalClaims);
            }

            // lookup our user and external provider info
            var (user, provider, providerUserId, claims) = await FindUserFromExternalProvider(result);
            if (user == null)
            {
                if (provider == "okta") {
                    user = await AutoProvisionOktaUser(provider, providerUserId, claims);
                }
                else {
                // this might be where you might initiate a custom workflow for user registration
                // in this sample we don't show how that would be done, as our sample implementation
                // simply auto-provisions new external user
                user = await AutoProvisionUser(provider, providerUserId, claims);
                }
            }

            // this allows us to collect any additional claims or properties
            // for the specific protocols used and store them in the local auth cookie.
            // this is typically used to store data needed for signout from those protocols.
            var additionalLocalClaims = new List<Claim>();
            var localSignInProps = new AuthenticationProperties();
            ProcessLoginCallback(result, additionalLocalClaims, localSignInProps);
            
            // issue authentication cookie for user
            var isuser = new IdentityServerUser(user.Subject)
            {
                DisplayName = user.UserName,
                IdentityProvider = provider,
                AdditionalClaims = additionalLocalClaims
            };

            await HttpContext.SignInAsync(isuser, localSignInProps);

            // delete temporary cookie used during external authentication
            await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);

            // retrieve return URL
            var returnUrl = result.Properties.Items["returnUrl"] ?? "~/";

            // check if external login is in the context of an OIDC request
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            await _events.RaiseAsync(new UserLoginSuccessEvent(provider, providerUserId, user.Subject, user.UserName, true, context?.Client.ClientId));

            if (context != null)
            {
                if (context.IsNativeClient())
                {
                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    return this.LoadingPage("Redirect", returnUrl);
                }
            }

            return Redirect(returnUrl);
        }

        private async Task<(User user, string provider, 
            string providerUserId, IEnumerable<Claim> claims)> FindUserFromExternalProvider(AuthenticateResult result)
        {
            // user from external provider
            var externalUser = result.Principal;

            // try to determine the unique id of the external user (issued by the provider)
            // the most common claim type for that are the sub claim and the NameIdentifier
            // depending on the external provider, some other claim type might be used
            var userIdClaim = externalUser.FindFirst(JwtClaimTypes.Subject) ??
                              externalUser.FindFirst(ClaimTypes.NameIdentifier) ??
                              throw new Exception("Unknown userid");

            // remove the user id claim so we don't include it as an extra claim if/when we provision the user
            var claims = externalUser.Claims.ToList();
            claims.Remove(userIdClaim);

            var provider = result.Properties.Items["scheme"];
            var providerUserId = userIdClaim.Value;

            // find external user
            var user = await _localUserService.GetUserByExternalProvider(provider, providerUserId);

            return (user, provider, providerUserId, claims);
        }

        private async Task<User> AutoProvisionUser(string provider, string providerUserId, IEnumerable<Claim> claims)
        {
            // can do more here!
            // could update claim from 3rd party that is stored in database at certain intervals
            // automatically add or remove claims from 3rd party that are now needed or no longer used
            // depends on the use case
            var mappedClaims = new List<Claim>();
            // map the claims, and ignore those for which no mapping exists
            foreach (var claim in claims)
            {
                if (_facebookClaimTypeMap.ContainsKey(claim.Type))
                {
                    mappedClaims.Add(new Claim(_facebookClaimTypeMap[claim.Type], claim.Value));
                }
            }
            var user = _localUserService.ProvisionUserFromExternalIdentity(provider, providerUserId, mappedClaims);
            await _localUserService.SaveChangesAsync();
            return user;
        }

        private async Task<User> AutoProvisionOktaUser(string provider, string providerUserId, IEnumerable<Claim> claims)
        {
            // what is email claim
            string email = claims.FirstOrDefault(c => c.Type == "preferred_username").Value;

            // try to find user by email address
            var existingUser = await _localUserService.GetUserByEmailAsync(email);

            if (existingUser != null)
            {
                await _localUserService.AddExternalProviderToUser(
                    existingUser.Subject,
                    provider,
                    providerUserId
                );
                await _localUserService.SaveChangesAsync();

                return existingUser;
            }

            // if existingUser is null, meaning a new user, we'd need to mapped the okta claims and use those when
            // provisioning the user instead of a new List<Claim>();


            var user = _localUserService.ProvisionUserFromExternalIdentity(provider, providerUserId,
                new List<Claim>());
            await _localUserService.SaveChangesAsync();
            return user;
        }

        // if the external login is OIDC-based, there are certain things we need to preserve to make logout work
        // this will be different for WS-Fed, SAML2p or other protocols
        private void ProcessLoginCallback(AuthenticateResult externalResult, List<Claim> localClaims, AuthenticationProperties localSignInProps)
        {
            // if the external system sent a session id claim, copy it over
            // so we can use it for single sign-out
            var sid = externalResult.Principal.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
            if (sid != null)
            {
                localClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
            }

            // if the external provider issued an id_token, we'll keep it for signout
            var idToken = externalResult.Properties.GetTokenValue("id_token");
            if (idToken != null)
            {
                localSignInProps.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = idToken } });
            }
        }
    }
}