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
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> {"role"}
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope> {
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
                ClientId = "spa",

                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,

                RedirectUris = { "https://localhost:5001/signin-oidc" },

                PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },

                AllowedCorsOrigins = new List<string>
                {
                    "https://localhost:7148", "https://localhost:44491","https://localhost:5055","https://localhost:7153","https://localhost:5001"
                },

                AllowOfflineAccess = true,

                AllowedScopes = {  
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email, 
                    "api" 
                }
            },
        };
}