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
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(), // must have this for oidc, gives subject id
                new IdentityResources.Profile(), // returns given_name, family_name anyother profile info
                new IdentityResources.Address()
            };

        // what apis a client can access, map to scope that give access to apis
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { };

        // what clients this IDP is for
        // need at least one client per app
        public static IEnumerable<Client> Clients =>
            new Client[] 
            { 
                new Client
                {
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
                        IdentityServerConstants.StandardScopes.Address
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