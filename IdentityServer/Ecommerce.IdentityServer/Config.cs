// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Ecommerce.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_Catalog"){Scopes={"Catalog_FullPermission"}},
                new ApiResource("resource_Basket"){Scopes={"Basket_FullPermission"}},
                   new ApiResource("resource_Discount"){Scopes={"Discount_FullPermission"}},
                     new ApiResource("resource_Order"){Scopes={"Order_FullPermission"}},
                        new ApiResource("resource_Payment"){Scopes={"Payment_FullPermission"}},
                            new ApiResource("resource_PhotoStock"){Scopes={"PhotoStock_FullPermission"}},
                                new ApiResource("resource_Gateway"){Scopes={"Gateway_FullPermission"}},
                                    new ApiResource(IdentityServerConstants.LocalApi.ScopeName)

        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),//sub 
                new IdentityResources.Profile(),
                new IdentityResources.Email()
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("Catalog_FullPermission","CatalogApi için full erişim"),
                    new ApiScope("Basket_FullPermission", "SepetApi için full erişim"),
                        new ApiScope("Discount_FullPermission", "İndirimApi için full erişim"),
                            new ApiScope("Order_FullPermission", "SiparişApi için full erişim"),
                                new ApiScope("Payment_FullPermission", "ÖdemeApi için full erişim"),
                                    new ApiScope("PhotoStock_FullPermission", "FotoğrafApi için full erişim"),
                                        new ApiScope("Gateway_FullPermission", "GatewayApi için full erişim"),
                                            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "coremvcclient",
                    ClientName = "ASP.Net Core",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedScopes = { "Catalog_FullPermission","PhotoStock_FullPermission", "Gateway_FullPermission", IdentityServerConstants.LocalApi.ScopeName }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "coremvcclientforuser",
                    ClientName= "ASP.Net Core",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowOfflineAccess=true,
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "Catalog_FullPermission","Basket_FullPermission","Discount_FullPermission","Order_FullPermission", "Payment_FullPermission", "Gateway_FullPermission",IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName },
                    AccessTokenLifetime=300

                },
            };
    }
}