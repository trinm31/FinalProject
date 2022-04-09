using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SchedulingGenerate.Services.Messages;
using SchedulingGenerate.Services.Models;
using SchedulingGenerate.Services.Services.IRepository;
using SchedulingGenerate.Services.Services.Repository;

namespace SchedulingGenerate.Services.Messaging;

public class RabbitMQSchedulingCrudCourseConsumer: Microsoft.Extensions.Hosting.BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private readonly ExamRepository _examRepository;
    private readonly IConfiguration _configuration;

    public RabbitMQSchedulingCrudCourseConsumer(ExamRepository examRepository, IConfiguration configuration)
    {
        _configuration = configuration;
        Console.WriteLine(_configuration["RabbitMQHost"]);
        
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQHost"],
            UserName = "guest",
            Password = "guest",
            Port = int.Parse(_configuration["RabbitMQPort"])
        };

        _examRepository = examRepository;

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "schedulingcrudcoursemessagequeue", false, false, false, arguments: null);
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            SchedulingCrudCourseRequestDto schedulingCrudCourseRequestDto = JsonConvert.DeserializeObject<SchedulingCrudCourseRequestDto>(content);
            HandleCrudCourseMessage(schedulingCrudCourseRequestDto).GetAwaiter().GetResult();

            _channel.BasicAck(ea.DeliveryTag, false);
        };
        _channel.BasicConsume("schedulingcrudcoursemessagequeue", false, consumer);

        return Task.CompletedTask;
    }

    private async Task HandleCrudCourseMessage(SchedulingCrudCourseRequestDto schedulingCrudCourseRequestDto)
    {
        if (schedulingCrudCourseRequestDto.MethodType == "create")
        {
            await _examRepository.Create(new Course()
            {
                ExamId = schedulingCrudCourseRequestDto.Id,
                Name = schedulingCrudCourseRequestDto.Name
            });
        }
        
        if (schedulingCrudCourseRequestDto.MethodType == "update")
        {
            await _examRepository.Update(new Course()
            {
                ExamId = schedulingCrudCourseRequestDto.Id,
                Name = schedulingCrudCourseRequestDto.Name
            });
        }
        
        if (schedulingCrudCourseRequestDto.MethodType == "delete")
        {
            await _examRepository.Delete(new Course()
            {
                ExamId = schedulingCrudCourseRequestDto.Id,
                Name = schedulingCrudCourseRequestDto.Name
            });
        }
    }
}