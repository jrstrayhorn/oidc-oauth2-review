using System;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Extensions;
using Marvin.IDP.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Marvin.IDP.UserRegistration
{
    public class UserRegistrationController : Controller
    {
        private readonly ILocalUserService _localUserService;
        private readonly IIdentityServerInteractionService _interaction;

        private readonly ILogger<UserRegistrationController> _logger;

        public UserRegistrationController(ILocalUserService localUserService,
            IIdentityServerInteractionService interaction,
            ILogger<UserRegistrationController> logger)
        {
            _logger = logger;
            _interaction = interaction;
            _localUserService = localUserService;

        }

        // if this view is accessed, it means the user clicked the verification email
        [HttpGet]
        public async Task<IActionResult> ActivateUser(string securityCode)
        {
            if (await _localUserService.ActivateUser(securityCode))
            {
                ViewData["Message"] = "Your account was successfully activated.   " +
                    "Navigate to your client application to log in.";
            }
            else
            {
                ViewData["Message"] = "Your account couldn't be activated, " +
                    "please contact your administrator.";
            }

            await _localUserService.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public IActionResult RegisterUser(string returnUrl)
        {
            var vm = new RegisterUserViewModel()
            { ReturnUrl = returnUrl };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // this is our custom user entity
            var userToCreate = new Entities.User
            {
                //Password = model.Password,
                UserName = model.UserName,
                Subject = Guid.NewGuid().ToString(),
                Email = model.Email,
                Active = false // by default for now, will activate AFTER confirmation
            };

            // then to the user object we add claims
            userToCreate.Claims.Add(new Entities.UserClaim()
            {
                Type = "country",
                Value = model.Country
            });

            userToCreate.Claims.Add(new Entities.UserClaim()
            {
                Type = JwtClaimTypes.Address,
                Value = model.Address
            });

            userToCreate.Claims.Add(new Entities.UserClaim()
            {
                Type = JwtClaimTypes.GivenName,
                Value = model.GivenName
            });

            userToCreate.Claims.Add(new Entities.UserClaim()
            {
                Type = JwtClaimTypes.FamilyName,
                Value = model.FamilyName
            });

            _localUserService.AddUser(userToCreate, model.Password);
            await _localUserService.SaveChangesAsync();

            // create an activation link
            var link = Url.ActionLink("ActivateUser", "UserRegistration",
                new { securityCode = userToCreate.SecurityCode });

            _logger.LogInformation(link);

            return View("ActivationCodeSent");

            // // if new user and activated we should sign them in
            // var isuser = new IdentityServerUser(userToCreate.Subject)
            // {
            //     DisplayName = userToCreate.UserName
            // };

            // await HttpContext.SignInAsync(isuser, null);

            // // continue with the flow
            // // means returnURL is valid to client
            // if (_interaction.IsValidReturnUrl(model.ReturnUrl)
            //     || Url.IsLocalUrl(model.ReturnUrl))
            // {
            //     return Redirect(model.ReturnUrl);
            // }

            // return Redirect("~/");
        }
    }
}