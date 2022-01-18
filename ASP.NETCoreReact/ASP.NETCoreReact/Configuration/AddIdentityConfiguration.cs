using Duende.Bff.Yarp;

namespace ASP.NETCoreReact.Configuration;

public static partial class ServiceExtension {
    public static IServiceCollection AddIdentityConfiguration(
        this IServiceCollection serviceCollection) {

        // cookie options
        serviceCollection.AddAuthentication(options =>
            {
                options.DefaultScheme = "cookie";
                options.DefaultChallengeScheme = "oidc";
                options.DefaultSignOutScheme = "oidc";
            })
            .AddCookie("cookie", options =>
            {
                // set session lifetime
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
            
                // sliding or absolute
                options.SlidingExpiration = false;
            
                // host prefixed cookie name
                options.Cookie.Name = "__SPA_FF";
            
                // strict SameSite handling
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
            })
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "https://localhost:7153";
            
                // confidential client using code flow + PKCE
                options.ClientId = "spa";
                options.ClientSecret = "secretlalalaAiMaBik";
                options.ResponseType = "code";
                options.ResponseMode = "query";

                options.MapInboundClaims = false;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.SaveTokens = true;

                // request scopes + refresh tokens
                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("api");
                options.Scope.Add("offline_access");
                
                options.TokenValidationParameters = new()
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };
                
            });

        return serviceCollection;
    }
}