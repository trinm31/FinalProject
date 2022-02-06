using ASP.NETCoreReact.Configuration;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

// In production, the React files will be served from this directory
builder.Services.AddSpaStaticFiles(configuration => {
    configuration.RootPath = "ClientApp/build";
});

builder.Services.AddCorsConfiguration(builder.Environment, builder.Configuration);

// Add BFF services to DI - also add server-side session management
builder.Services.AddBff(options =>
    {
        options.AntiForgeryHeaderValue = "1";
        options.AntiForgeryHeaderName = "X-CSRF";
        options.ManagementBasePath = "/bff";
    })
    .AddServerSideSessions()
    .AddRemoteApis();

builder.Services.AddIdentityConfiguration();

builder.Services.AddHealthChecks();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");

app.UseCors("cors_policy");

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost,
}); 

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseBff();

app.UseAuthorization();

app.MapBffManagementEndpoints();

app.MapControllers()
    .RequireAuthorization()
    .AsBffApiEndpoint();

app.MapRemoteBffApiEndpoint("/WeatherForecast", "https://localhost:7143/WeatherForecast",false).RequireAccessToken();

app.MapFallbackToFile("index.html");

app.Run();