using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;

namespace Identity.Services.Initializer;

public class Clients
    {
        public static IEnumerable<Client> Get(bool isProd)
        {
            if (isProd)
            {
                string baseUrl = "http://trinm.com:80";
                return new List<Client>
                {
                    new Client
                    {
                        ClientId = "spa",

                        ClientSecrets = { new Secret("secret".Sha256()) },

                        AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,

                        RedirectUris = { $"{baseUrl}/signin-oidc" },

                        BackChannelLogoutUri = $"{baseUrl}/bff/backchannel",

                        PostLogoutRedirectUris = { $"{baseUrl}/signout-callback-oidc" },

                        AllowedCorsOrigins = new List<string>
                        {
                            baseUrl,
                        },

                        AllowOfflineAccess = true,

                        AllowedScopes = { "openid", "profile", "api" }
                    },
                    new Client
                    {
                        ClientId = "device",

                        ClientSecrets = { new Secret("secret".Sha256())},

                        ClientName = "Some machine or server using clinet credentials",

                        AllowedGrantTypes = GrantTypes.ClientCredentials,

                        AllowedScopes = { "api" },
                    },
                    new Client
                    {
                        ClientId = "postman",

                        ClientSecrets = { new Secret("postman_secret".Sha256())},

                        ClientName = "Postman password credential flow",

                        RedirectUris = { $"{baseUrl}/signin-oidc" },

                        BackChannelLogoutUri = $"{baseUrl}/bff/backchannel",

                        PostLogoutRedirectUris = { $"{baseUrl}/signout-callback-oidc" },

                        AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,

                        AllowOfflineAccess = true,

                        AllowedScopes = { "openid", "profile", "api" }
                    }
                };
            }
            
            return new List<Client>
            {
                new Client
                {
                    ClientId = "spa",

                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,

                    RedirectUris = { "https://localhost:5001/signin-oidc" },

                    BackChannelLogoutUri = "https://localhost:5001/bff/backchannel",

                    PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },

                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:5001",
                    },

                    AllowOfflineAccess = true,

                    AllowedScopes = { "openid", "profile", "api" }
                },
                new Client
                {
                    ClientId = "device",

                    ClientSecrets = { new Secret("secret".Sha256())},

                    ClientName = "Some machine or server using clinet credentials",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes = { "api" },
                },
                new Client
                {
                    ClientId = "postman",

                    ClientSecrets = { new Secret("postman_secret".Sha256())},

                    ClientName = "Postman password credential flow",

                    RedirectUris = { "https://localhost:5001/signin-oidc" },

                    BackChannelLogoutUri = "https://localhost:5001/bff/backchannel",

                    PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,

                    AllowOfflineAccess = true,

                    AllowedScopes = { "openid", "profile", "api" }
                }
            };
        }
    }
    public class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "api",
                    DisplayName = "API #1",
                    Description = "Allow the application to access API",
                    Scopes = new List<string> {"api.read", "api.write"},
                    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())}, // change me!
                    UserClaims = new List<string> {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.ClientId,
                        JwtClaimTypes.SessionId
                        }

                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new ApiScope[]
            {
                new ApiScope("api", new[] {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.SessionId
                    }),
            };
        }
    }
    