
using Duende.IdentityServer.Services;
using Identity.Services;
using Identity.Services.Configuration;
using Identity.Services.Initializer;
using Microsoft.Extensions.DependencyInjection;
using Identity.Services.Configuration;
using Identity.Services.Data;
using Identity.Services.Models;
using Identity.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

var identityServerBuilder = builder.Services.AddIdentityServer(options =>
    {
        // set path where to store keys
        options.KeyManagement.KeyPath = "/Users/Shared/Key";
    
        // new key every 30 days
        options.KeyManagement.RotationInterval = TimeSpan.FromDays(30);
    
        // announce new key 2 days in advance in discovery
        options.KeyManagement.PropagationTime = TimeSpan.FromDays(2);
    
        // keep old key for 7 days in discovery for validation of tokens
        options.KeyManagement.RetentionDuration = TimeSpan.FromDays(7);
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
    })
    .AddAspNetIdentity<ApplicationUser>();

// codes, tokens, consents
identityServerBuilder.AddOperationalStore<AppPersistedGrantDbContext>(options =>
    options.ConfigureDbContext = option =>
        option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// clients, resources
identityServerBuilder.AddConfigurationStore<AppConfigurationDbContext>(options =>
    options.ConfigureDbContext = option =>
        option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddMvc();

builder.Services.AddScoped<IProfileService, ProfileService>();

// add new 

builder.Services.AddCorsConfiguration(builder.Environment);

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCookiePolicy();

app.UseCors("cors_policy");

app.UseHealthChecks("/health");

app.UseStaticFiles();

app.UseRouting(); 
        
app.UseIdentityServer(); // Enable handling of OpenId Connect and OAuth.

app.UseAuthentication();

app.UseAuthorization();

app.MapGet("/testidentity", () => "Hello identity services");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

DbInitializer.Initialize(app, app.Environment.IsProduction());

app.Run();