using System.Text;
using Management.Services.Messages;
using Management.Services.Models;
using Management.Services.Services.Repository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Management.Services.Messaging;

public class RabbitMQSchedulingResultConsumer : Microsoft.Extensions.Hosting.BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private readonly ScheduleRepository _scheduleRepository;

    public RabbitMQSchedulingResultConsumer(ScheduleRepository scheduleRepository)
    {
        //Todo: clean here
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        _scheduleRepository = scheduleRepository;
        
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "schedulingresultmessagequeue", false, false, false, arguments: null);
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            SchedulingResultRequestDto schedulingResultRequestDto = JsonConvert.DeserializeObject<SchedulingResultRequestDto>(content);
            HandleResultMessage(schedulingResultRequestDto).GetAwaiter().GetResult();

            _channel.BasicAck(ea.DeliveryTag, false);
        };
        _channel.BasicConsume("schedulingresultmessagequeue", false, consumer);

        return Task.CompletedTask;
    }

    private async Task HandleResultMessage(SchedulingResultRequestDto schedulingResultRequestDto)
    {
        if (schedulingResultRequestDto.MethodType == "create")
        {
            List<Schedule> schedules = new List<Schedule>();
            foreach (var result in schedulingResultRequestDto.ResultList)
            {
                schedules.Add(new Schedule()
                {
                    CourseId = result.CourseId,
                    Color = result.Color,
                    Date = result.Date,
                    Slot = result.Slot
                });
            }

            await _scheduleRepository.CreateRange(schedules);
        }
        
        
        if (schedulingResultRequestDto.MethodType == "delete")
        {
            await _scheduleRepository.DeleteAll();
        }
        
    }
}