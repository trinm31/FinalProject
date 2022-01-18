using Duende.IdentityServer.Services;


namespace ASP.NETCoreReact.Configuration;

public static partial class ServiceExtension
    {
        public static IServiceCollection AddCorsConfiguration(
        this IServiceCollection serviceCollection,
        IWebHostEnvironment env,
        IConfiguration cfg)
        {

            List<string> allowed_origins = new List<string>();

            if (env.IsDevelopment())
            {
                allowed_origins.AddRange(new string[]{
                    "https://localhost:7148",
                    "https://localhost:44491", 
                    "http://localhost:5055",
                    "https://localhost:7153",
                    "http://localhost:5114",
                });
            }

          

            serviceCollection.AddCors(options =>
            {
                options.AddPolicy("cors_policy", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    //------------------------------------
                    policy.WithOrigins(allowed_origins.ToArray());
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
                    AllowedOrigins = allowed_origins,
                    AllowAll = false
                };
            });

            return serviceCollection;

        }
    }