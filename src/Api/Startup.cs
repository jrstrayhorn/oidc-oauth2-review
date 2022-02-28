using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Api
{
    public class Startup
    {
        private string _baseAuthURL = "https://localhost:44318";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options => 
                {
                    options.Authority = "https://localhost:44318";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // using scope only model so no aud claim will be added to the token
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy => 
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "api1");
                });
            });

            // the above policy can be enforced at various levels
            // globally
            // for all API endpoints
            // for specific controllers/actions

            // but typically you'd setup the policy for all API endpoints in the routing system.

            // adding swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Test API",
                    Version = "v1"
                });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    In = ParameterLocation.Header,
                    Flows = new OpenApiOAuthFlows()
                    {
                        ClientCredentials = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri(_baseAuthURL + "/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "api1", "Test API" }
                            },
                            AuthorizationUrl = new Uri(_baseAuthURL + "/connect/authorize")
                        }
                    },
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ClientCredentials"
                    }
                };

                c.AddSecurityDefinition("ClientCredentials", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement {{ securitySchema, new[] { "ClientCredentials" }}};
                c.AddSecurityRequirement(securityRequirement);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API");
                });
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireAuthorization("ApiScope");
            });
        }
    }
}
