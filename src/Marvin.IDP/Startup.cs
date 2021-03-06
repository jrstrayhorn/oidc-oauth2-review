// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServerHost.Quickstart.UI;
using Marvin.IDP.DbContexts;
using Marvin.IDP.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Marvin.IDP
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment, if you want to add an MVC-based UI
            services.AddControllersWithViews();

            services.AddDbContext<IdentityDbContext>(options => 
            {
                options.UseSqlite("DataSource=identityUsers.db");
            });

            services.AddScoped<IPasswordHasher<Entities.User>, PasswordHasher<Entities.User>>();
            services.AddScoped<ILocalUserService, LocalUserService>();

            var builder = services.AddIdentityServer(options =>
            {
                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryClients(Config.Clients);
                //.AddTestUsers(TestUsers.Users);

            // this will pull claims that are need for scope from user claims
            builder.AddProfileService<LocalUserProfileService>();
            
            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            services.AddAuthentication().AddFacebook(
                "Facebook",
                options =>
                {
                    options.AppId = "use-app-fb-app-id-here";
                    options.AppSecret = "use-fb-app-secret-here";
                    options.SignInScheme = 
                        IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme;
                }
            )
            .AddOpenIdConnect("okta", "Okta", options =>
            {
                options.SignInScheme = 
                    IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.Authority = "https://use-dev-here.okta.com";
                options.ClientId = "use-okta-client-id-here";
                options.ClientSecret = "use-okta-client-secret-here";
                options.Scope.Add("openid");
                options.Scope.Add("profile");
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to add MVC
            app.UseStaticFiles();
            app.UseRouting();
            
            // add to request pipeline
            app.UseIdentityServer();

            // uncomment, if you want to add MVC
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
               endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
