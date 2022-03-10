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

app.MapRemoteBffApiEndpoint("/api/Exams/GetAllExam", "https://localhost:7143/api/Exams/GetAllExam",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Exams", "https://localhost:7143/api/Exams",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Exams/Create", "https://localhost:7143/api/Exams/Create",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Exams/Edit", "https://localhost:7143/api/Exams/Edit",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Exams/GetExamById", "https://localhost:7143/api/Exams/GetExamById",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Exams/Upload", "https://localhost:7143/api/Exams/Upload",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Students/ListAllStudent", "https://localhost:7143/api/Students/ListAllStudent",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Students", "https://localhost:7143/api/Students",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Students/Create", "https://localhost:7143/api/Students/Create",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Students/CreateQrCode", "https://localhost:7143/api/Students/CreateQrCode",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Students/Edit", "https://localhost:7143/api/Students/Edit",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Students/GetStudentById", "https://localhost:7143/api/Students/GetStudentById",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Students/Upload", "https://localhost:7143/api/Students/Upload",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/StudentExams/GetAllStudentExam", "https://localhost:7143/api/StudentExams/GetAllStudentExam",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/StudentExams", "https://localhost:7143/api/StudentExams",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/StudentExams/Create", "https://localhost:7143/api/StudentExams/Create",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/StudentExams/Edit", "https://localhost:7143/api/StudentExams/Edit",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/StudentExams/GetStudentExamById", "https://localhost:7143/api/StudentExams/GetStudentExamById",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/StudentExams/Upload", "https://localhost:7143/api/StudentExams/Upload",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Setting/GetSetting", "https://localhost:7143/api/Setting/GetSetting",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Setting/Update", "https://localhost:7143/api/Setting/Update",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/SchedulingGenerate", "https://localhost:7065/api/SchedulingGenerate", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Schedule/GetAll", "https://localhost:7143/api/Schedule/GetAll", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Rooms/GetAll", "https://localhost:7143/api/Rooms/GetAll", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Rooms", "https://localhost:7143/api/Rooms", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Rooms/Create", "https://localhost:7143/api/Rooms/Create", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Rooms/Edit", "https://localhost:7143/api/Rooms/Edit", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Rooms/GetRoomById", "https://localhost:7143/api/Rooms/GetRoomById", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Rooms/RoomsPagination", "https://localhost:7143/api/Rooms/RoomsPagination", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Checkin/CheckInConfirm", "https://localhost:7143/api/Checkin/CheckInConfirm", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Checkin/CheckIn", "https://localhost:7143/api/Checkin/CheckIn", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Checkin/Excel", "https://localhost:7143/api/Checkin/Excel", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Checkin/Detail", "https://localhost:7143/api/Checkin/Detail", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Users/UsersPagination", "https://localhost:7132/api/Users/UsersPagination", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Users", "https://localhost:7132/api/Users", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Users/CreateUser", "https://localhost:7132/api/Users/CreateUser", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Users/UpdateUser", "https://localhost:7132/api/Users/UpdateUser", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Users/GetUserById", "https://localhost:7132/api/Users/GetUserById", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Users/Upload", "https://localhost:7132/api/Users/Upload", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/CheckQr/GetQrCode", "https://localhost:7143/api/CheckQr/GetQrCode", false).AllowAnonymous();

app.MapRemoteBffApiEndpoint("/api/Schedule/GetByStudentId", "https://localhost:7143/api/Schedule/GetByStudentId", false).AllowAnonymous();

app.MapFallbackToFile("index.html");

app.Run();