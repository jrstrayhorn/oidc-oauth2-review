using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;
using ImageGallery.Client.HttpHandlers;
using Microsoft.Extensions.Options;
using ImageGallery.Client.PostConfigurationOptions;

namespace ImageGallery.Client
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                 .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddAuthorization(authorizationOptions =>
            {
                authorizationOptions.AddPolicy(
                    "CanOrderFrame",
                    policyBuilder =>
                    {
                        policyBuilder.RequireAuthenticatedUser();
                        // if you want to match multiple claims, just add as additional parameters
                        // like RequireClaim("country", "be", "us")
                        policyBuilder.RequireClaim("country", "be");
                        policyBuilder.RequireClaim("subscriptionlevel", "PayingUser");
                        // if you also wat to require a role do this
                        // policyBuilder.RequireRole("admin")
                    }
                );

                authorizationOptions.AddPolicy(
                    "MustBePayingUser",
                    policyBuilder =>
                    {
                        policyBuilder.RequireAuthenticatedUser();
                        policyBuilder.RequireClaim("subscriptionlevel", "PayingUser");
                    }
                );
            });

            services.AddHttpContextAccessor();

            // since it's short lived service, use Transient
            services.AddTransient<BearerTokenHandler>();

            // create an HttpClient used for accessing the API
            services.AddHttpClient("APIClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44366/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<BearerTokenHandler>();

            services.AddHttpClient("BasicAPIClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44366/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });

            // create an HttpClient used for accessing the API
            services.AddHttpClient("IDPClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44318/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });

            services.AddAuthentication(options => {
                // if you have multiple apps on the same domain, you'll want to make sure
                // the cookie names are different so they don't interfere with each other
                // handles sign in, sign out, session management for authentication
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                // handles the clients side of oidc redirect endpoints
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            // enables cookie based authentication
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => {
                options.AccessDeniedPath = "/Authorization/AccessDenied";
            })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = "https://localhost:44318/";
                options.ClientId = "imagegalleryclient";
                options.ResponseType = "code";
                // don't need to set to true as UsePkce is enabled by default
                //options.UsePkce = false;
                // don't need this if using the default value that the middleware uses
                // which is /signin-oidc
                // options.CallbackPath = new Microsoft.AspNetCore.Http.PathString("....")
                // default for signout is signout-callback-oidc

                // open id and profile is added by default so don't need this...
                //options.Scope.Add("openid");
                //options.Scope.Add("profile");
                options.Scope.Add("address");
                options.Scope.Add("roles");
                options.Scope.Add("imagegalleryapi");
                // options.Scope.Add("subscriptionlevel");
                options.Scope.Add("country");
                options.Scope.Add("offline_access");

                //options.ClaimActions.Remove("nbf"); // ensure that will be in claims
                // these options will remove from claims - we will get address from userInfo endpoint
                // removing these claims to keep cookie small
                // the constructor for OpenIdConnectOptions middleware.. deletes a bunch of claims by default
                // it also maps some standard claims by default
                // sub, name, given_name, family_name, profile, email
                // there is no standard mapping for address so it won't show up in claims
                // options.ClaimActions.DeleteClaim("address");
                options.ClaimActions.DeleteClaim("sid");
                options.ClaimActions.DeleteClaim("idp");
                options.ClaimActions.DeleteClaim("s_hash");
                options.ClaimActions.DeleteClaim("auth_time");

                // roles isn't mapped by default, we need to add them ourselves
                // 1st parameter is claim type, 2nd parameter is what name should be in json
                options.ClaimActions.MapUniqueJsonKey("role", "role");
                // options.ClaimActions.MapUniqueJsonKey("subscriptionlevel", "subscriptionlevel");
                options.ClaimActions.MapUniqueJsonKey("country", "country");

                options.SaveTokens = true;
                options.ClientSecret = "secret";
                options.GetClaimsFromUserInfoEndpoint = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.GivenName,
                    RoleClaimType = JwtClaimTypes.Role
                };
                
            });

            services.AddSingleton<IPostConfigureOptions<OpenIdConnectOptions>,
                OpenIdConnectOptionsPostConfigureOptions>();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
                // The default HSTS value is 30 days. You may want to change this for
                // production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Gallery}/{action=Index}/{id?}");
            });
        }
    }
}
