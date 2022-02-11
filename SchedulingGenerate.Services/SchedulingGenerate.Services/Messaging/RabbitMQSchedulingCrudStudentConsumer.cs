using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SchedulingGenerate.Services.Messages;
using SchedulingGenerate.Services.Models;
using SchedulingGenerate.Services.Services.Repository;

namespace SchedulingGenerate.Services.Messaging;

public class RabbitMQSchedulingCrudStudentConsumer: Microsoft.Extensions.Hosting.BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private readonly StudentRepository _studentReposzitory;

    public RabbitMQSchedulingCrudStudentConsumer(StudentRepository studentRepository)
    {
        //Todo: clean here
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        _studentReposzitory = studentRepository;
        
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "schedulingcrudstudentmessagequeue", false, false, false, arguments: null);
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            SchedulingCrudStudentRequestDto schedulingCrudStudentRequestDto = JsonConvert.DeserializeObject<SchedulingCrudStudentRequestDto>(content);
            HandleCrudStudentMessage(schedulingCrudStudentRequestDto).GetAwaiter().GetResult();

            _channel.BasicAck(ea.DeliveryTag, false);
        };
        _channel.BasicConsume("schedulingcrudstudentmessagequeue", false, consumer);

        return Task.CompletedTask;
    }

    private async Task HandleCrudStudentMessage(SchedulingCrudStudentRequestDto schedulingCrudStudentRequestDto)
    {
        if (schedulingCrudStudentRequestDto.MethodType == "create")
        {
            await _studentReposzitory.Create(new Student()
            {
                StudentId = schedulingCrudStudentRequestDto.Id,
                Name = schedulingCrudStudentRequestDto.Name
            });
        }
        
        if (schedulingCrudStudentRequestDto.MethodType == "update")
        {
            await _studentReposzitory.Update(new Student()
            {
                StudentId = schedulingCrudStudentRequestDto.Id,
                Name = schedulingCrudStudentRequestDto.Name
            }, schedulingCrudStudentRequestDto.oldStudentId);
        }
        
        if (schedulingCrudStudentRequestDto.MethodType == "delete")
        {
            await _studentReposzitory.Delete(new Student()
            {
                StudentId = schedulingCrudStudentRequestDto.Id,
                Name = schedulingCrudStudentRequestDto.Name
            });
        }
    }
}