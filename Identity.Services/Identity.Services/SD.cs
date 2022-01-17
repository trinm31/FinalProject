using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace Identity.Services;

public static class SD
{
    public const string Admin = "Admin";
    public const string Staff = "Staff";
    public const string Student = "Student";
    

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
            Name = "role",
            UserClaims = new List<string> {"role"}
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope> {
            new ApiScope("esm", "Esm Server"),
            new ApiScope(name: "read",   displayName: "Read your data."),
            new ApiScope(name: "write",  displayName: "Write your data."),
            new ApiScope(name: "delete", displayName: "Delete your data."),
            new ApiScope("api", new[] {
                JwtClaimTypes.Name,
                JwtClaimTypes.Role,
                JwtClaimTypes.Email,
                JwtClaimTypes.ClientId,
                JwtClaimTypes.SessionId
            })
        };

    public static IEnumerable<ApiResource> GetApiResources =>
        new[]
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
    
    
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId="client",
                ClientSecrets= { new Secret("secretlalalaAiMaBik".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes={ "read", "write","profile"}
            },
            new Client
            {
                ClientId="esm",
                ClientSecrets= { new Secret("secretlalalaAiMaBik".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris={ "http://localhost:3000/signin-oidc" },
                PostLogoutRedirectUris={"https://localhost:3000/signout-callback-oidc" },
                AllowedScopes=new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "esm"
                }
            },
            new Client
            {
                ClientId = "spa",

                ClientSecrets = { new Secret("secretlalalaAiMaBik".Sha256()) },

                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,

                RedirectUris = { "https://localhost:5015/signin-oidc" },

                BackChannelLogoutUri = "https://localhost:5015/bff/backchannel",

                PostLogoutRedirectUris = { "https://localhost:5015/signout-callback-oidc" },

                AllowedCorsOrigins = new List<string>
                {
                    "http://localhost:3000", "http://localhost:5001",
                },

                AllowOfflineAccess = true,

                AllowedScopes = { "openid", "profile", "api" }
            },
        };
}