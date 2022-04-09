using ASP.NETCoreReact.Configuration;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

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

builder.Services.AddIdentityConfiguration(builder.Configuration);

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

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseBff();

app.UseAuthorization();

app.MapGet("/testfrontend", () => "Hello frontend services");

app.MapBffManagementEndpoints();

app.MapControllers()
    .RequireAuthorization()
    .AsBffApiEndpoint();

app.MapRemoteBffApiEndpoint("/api/Exams/GetAllExam", $"{builder.Configuration["ManagementServices"]}/api/Exams/GetAllExam",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Exams", $"{builder.Configuration["ManagementServices"]}/api/Exams",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Exams/Create", $"{builder.Configuration["ManagementServices"]}/api/Exams/Create",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Exams/Edit", $"{builder.Configuration["ManagementServices"]}/api/Exams/Edit",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Exams/GetExamById", $"{builder.Configuration["ManagementServices"]}/api/Exams/GetExamById",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Exams/Upload", $"{builder.Configuration["ManagementServices"]}/api/Exams/Upload",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Students/ListAllStudent", $"{builder.Configuration["ManagementServices"]}/api/Students/ListAllStudent",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Students", $"{builder.Configuration["ManagementServices"]}/api/Students",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Students/Create", $"{builder.Configuration["ManagementServices"]}/api/Students/Create",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Students/CreateQrCode", $"{builder.Configuration["ManagementServices"]}/api/Students/CreateQrCode",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Students/Edit", $"{builder.Configuration["ManagementServices"]}/api/Students/Edit",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Students/GetStudentById", $"{builder.Configuration["ManagementServices"]}/api/Students/GetStudentById",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Students/Upload", $"{builder.Configuration["ManagementServices"]}/api/Students/Upload",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/StudentExams/GetAllStudentExam", $"{builder.Configuration["ManagementServices"]}/api/StudentExams/GetAllStudentExam",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/StudentExams", $"{builder.Configuration["ManagementServices"]}/api/StudentExams",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/StudentExams/Create", $"{builder.Configuration["ManagementServices"]}/api/StudentExams/Create",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/StudentExams/Edit", $"{builder.Configuration["ManagementServices"]}/api/StudentExams/Edit",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/StudentExams/GetStudentExamById", $"{builder.Configuration["ManagementServices"]}/api/StudentExams/GetStudentExamById",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/StudentExams/Upload", $"{builder.Configuration["ManagementServices"]}/api/StudentExams/Upload",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Setting/GetSetting", $"{builder.Configuration["ManagementServices"]}/api/Setting/GetSetting",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Setting/Update", $"{builder.Configuration["ManagementServices"]}/api/Setting/Update",false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/SchedulingGenerate", $"{builder.Configuration["ScheduleServices"]}/api/SchedulingGenerate", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Schedule/GetAll", $"{builder.Configuration["ManagementServices"]}/api/Schedule/GetAll", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Rooms/GetAll", $"{builder.Configuration["ManagementServices"]}/api/Rooms/GetAll", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Rooms", $"{builder.Configuration["ManagementServices"]}/api/Rooms", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Rooms/Create", $"{builder.Configuration["ManagementServices"]}/api/Rooms/Create", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Rooms/Edit", $"{builder.Configuration["ManagementServices"]}/api/Rooms/Edit", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Rooms/GetRoomById", $"{builder.Configuration["ManagementServices"]}/api/Rooms/GetRoomById", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Rooms/RoomsPagination", $"{builder.Configuration["ManagementServices"]}/api/Rooms/RoomsPagination", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Checkin/CheckInConfirm", $"{builder.Configuration["ManagementServices"]}/api/Checkin/CheckInConfirm", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Checkin/CheckIn", $"{builder.Configuration["ManagementServices"]}/api/Checkin/CheckIn", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Checkin/Excel", $"{builder.Configuration["ManagementServices"]}/api/Checkin/Excel", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Checkin/Detail", $"{builder.Configuration["ManagementServices"]}/api/Checkin/Detail", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Users/UsersPagination", $"{builder.Configuration["UserServices"]}/api/Users/UsersPagination", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Users", $"{builder.Configuration["UserServices"]}/api/Users", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Users/CreateUser", $"{builder.Configuration["UserServices"]}/api/Users/CreateUser", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Users/UpdateUser", $"{builder.Configuration["UserServices"]}/api/Users/UpdateUser", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Users/GetUserById", $"{builder.Configuration["UserServices"]}/api/Users/GetUserById", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Users/Upload", $"{builder.Configuration["UserServices"]}/api/Users/Upload", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Users/GetUserPersionalId", $"{builder.Configuration["UserServices"]}/api/Users/GetUserPersionalId", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/CheckQr/GetQrCode", $"{builder.Configuration["ManagementServices"]}/api/CheckQr/GetQrCode", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Schedule/GetByStudentId", $"{builder.Configuration["ManagementServices"]}/api/Schedule/GetByStudentId", false).RequireAccessToken();

app.MapRemoteBffApiEndpoint("/api/Charts/Index", $"{builder.Configuration["ManagementServices"]}/api/Charts/Index", false).RequireAccessToken();

app.MapFallbackToFile("index.html");

app.Run();