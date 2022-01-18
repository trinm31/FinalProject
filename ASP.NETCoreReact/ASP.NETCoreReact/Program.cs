using ASP.NETCoreReact.Configuration;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

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
    .AddServerSideSessions();


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

app.UseHttpsRedirection();

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseSpaStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseBff();

app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller}/{action=Index}/{id?}");

app.MapControllers().RequireAuthorization().AsBffApiEndpoint();

app.MapBffManagementEndpoints();

 app.MapFallbackToFile("index.html");

app.Run();