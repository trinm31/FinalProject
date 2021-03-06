using Duende.IdentityServer.Services;

namespace Identity.Services.Configuration;

public static partial class ServiceExtension {
    public static IServiceCollection AddCorsConfiguration(
        this IServiceCollection serviceCollection,
        IWebHostEnvironment Environment) 
    {

        string[] allowed_origins = null;

        if(Environment.IsDevelopment()){
            allowed_origins = new string[]{ 
                "https://localhost:7148",
                "https://localhost:44491", 
                "http://localhost:5055",
                "https://localhost:7153",
                "http://localhost:5114",
                "https://localhost:5001",
                "https://localhost:7065",
                "https://localhost:7132",
            };
        }else{
            // Add your production origins hire
            allowed_origins = new string[]{ 
                "https://localhost:7153",
                "http://localhost:5114",
                "https://localhost:5001"
            };
        }
        
        serviceCollection.AddCors(options =>
        {
            options.AddPolicy("cors_policy", policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                //------------------------------------
                policy.WithOrigins(allowed_origins);
                //policy.AllowAnyOrigin()
                //------------------------------------
                policy.AllowCredentials();
                policy.SetPreflightMaxAge(TimeSpan.FromSeconds(10000));
            });
        });

        // This is IdentityServer part
        serviceCollection.AddSingleton<ICorsPolicyService>((container) =>
        {
            var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();

            return new DefaultCorsPolicyService(logger) 
            {
                AllowedOrigins = allowed_origins
                //AllowAll = true
            };
        });

        return serviceCollection;

    }
}