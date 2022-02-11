using Microsoft.EntityFrameworkCore;
using SchedulingGenerate.Services.BackgroundService;
using SchedulingGenerate.Services.DbContext;
using SchedulingGenerate.Services.DbInitializer;
using SchedulingGenerate.Services.Messaging;
using SchedulingGenerate.Services.RabbitMQSender;
using SchedulingGenerate.Services.Services.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    }));

var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<LongRunningService>();
builder.Services.AddSingleton<BackgroundWorkerQueue>();
builder.Services.AddSingleton<IRabbitMQSchedulingSVMessageSender, RabbitMQSchedulingSVMessageSender>();
builder.Services.AddHostedService<RabbitMQSchedulingCrudCourseConsumer>();
builder.Services.AddHostedService<RabbitMQSchedulingCrudStudentConsumer>();
builder.Services.AddHostedService<RabbitMQSchedulingCrudStudentCourseConsumer>();
builder.Services.AddSingleton(new ExamRepository(optionBuilder.Options));
builder.Services.AddSingleton(new StudentRepository(optionBuilder.Options));
builder.Services.AddSingleton(new StudentExamRepository(optionBuilder.Options));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

DbInitialize.Initialize(app);

app.Run();