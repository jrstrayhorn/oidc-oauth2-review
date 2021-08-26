// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Marvin.IDP
{
    public static class Config
    {
        // user claims - map to scope that give access to identity info
        // policies for attribute based access would be setup here as well
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(), // must have this for oidc, gives subject id
                new IdentityResources.Profile(), // returns given_name, family_name anyother profile info
                new IdentityResources.Address(),
                // how to do a custom Identity Resource
                new IdentityResource(
                    "roles", // name of scope
                    "Your role(s)",
                    new List<string>() { "role" } // when you ask for this scope, what is returned
                ),
                new IdentityResource(
                    "country",
                    "The country you're living in",
                    new List<string>() { "country" }
                ),
                // subscriptionlevel is now part of application user profile
                // new IdentityResource(
                //     "subscriptionlevel",
                //     "Your subscription level",
                //     new List<string>() { "subscriptionlevel" }
                // )
            };

        // what apis a client can access, map to scope that give access to apis
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { 
                new ApiScope("imagegalleryapi", "Image Gallery API")
            };

        // logically grouping of Api Scopes, used to define an entire api
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                // you can include identity related scopes with api resources when defining
                // if you need to add identity related scopes to access token
                new ApiResource("imagegalleryapi", "Image Gallery API" 
                    /*part of application user profile nownew List<string>() { "role", "subscriptionlevel" }*/
                    )
                {
                    Scopes = {"imagegalleryapi"},
                    ApiSecrets = { new Secret("apisecret".Sha256()) } // need secret to hit token introspective endpoint to get
                                                                        // access token from reference token
                }
            };

        // what clients this IDP is for
        // need at least one client per app
        public static IEnumerable<Client> Clients =>
            new Client[] 
            { 
                new Client
                {
                    // using a reference token means having to hit IDP
                    // on API call to get the rest of the access token
                    // instead of jwt
                    AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 120,
                    // allows client to request access token
                    AllowOfflineAccess = true,
                    // when refresh token is issued, ensure fresh claims data
                    UpdateAccessTokenClaimsOnRefresh = true,
                    ClientName = "Image Gallery",
                    ClientId = "imagegalleryclient",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:5001/signin-oidc" // configured on web client
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:5001/signout-callback-oidc"
                    },
                    AllowedScopes = // what scopes the client can access
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "imagegalleryapi",
                        "country",
                        // "subscriptionlevel"
                    },
                    ClientSecrets = // used for client authentication to allow client to call the token endpoint
                    {
                        new Secret("secret".Sha256())
                    },
                    // to turn off pkce, by default pkce is turned on in the latest version of IdentityServer
                    //RequirePkce = false
                }
            };
    }
}