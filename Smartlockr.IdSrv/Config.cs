// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Smartlockr.IdSrv
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("domainapi", "Domain Api")
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {             
                new Client
                {
                    ClientId = "angular-domain-verify",
                    ClientName = "Domain Compliance Info",
                    ClientUri = "https://localhost:44331/",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =
                    {
                        "https://localhost:44331/callback"
                    },

                    PostLogoutRedirectUris = { "https://localhost:44331/" },
                    AllowedCorsOrigins = { "https://localhost:44331" },
                    

                    AllowedScopes = { "openid", "profile", "domainapi" }
                }
            };
    }
}