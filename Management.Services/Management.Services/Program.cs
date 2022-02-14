using AutoMapper;
using Management.Services;
using Management.Services.BackgroudService;
using Management.Services.DbContext;
using Management.Services.RabbitMQSender;
using Management.Services.Services.IRepository;
using Management.Services.Services.Repository;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ESM.Services.Management", Version = "v1" });
});

builder.Services.AddAuthentication("token")
    .AddJwtBearer("token", options =>
    {
        options.Authority = "https://localhost:7153";
        options.MapInboundClaims = false;

        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidTypes = new[] { "at+jwt" },
                
            NameClaimType = "name",
            RoleClaimType = "role"
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "api");
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v12", new OpenApiInfo { Title = "ESM.Services.Management", Version = "v1" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Enter 'Bearer' [space] and your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Scheme="oauth2",
                Name="Bearer",
                In=ParameterLocation.Header
            },
            new List<string>()
        }

    });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IRabbitMQManagementMessageSender, RabbitMQManagementMessageSender>();

builder.Services.AddHostedService<LongRunningService>();

builder.Services.AddSingleton<BackgroundWorkerQueue>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ESM.Services.Management v1"));
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost,
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
