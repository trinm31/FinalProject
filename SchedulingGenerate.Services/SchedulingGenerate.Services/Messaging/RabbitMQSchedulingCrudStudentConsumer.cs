using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SchedulingGenerate.Services.Messages;
using SchedulingGenerate.Services.Models;
using SchedulingGenerate.Services.Services.IRepository;

namespace SchedulingGenerate.Services.Messaging;

public class RabbitMQSchedulingCrudStudentConsumer: Microsoft.Extensions.Hosting.BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private readonly IUnitOfWork _unitOfWork;

    public RabbitMQSchedulingCrudStudentConsumer(IUnitOfWork unitOfWork)
    {
        //Todo: clean here
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        _unitOfWork = unitOfWork;

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
            await _unitOfWork.Student.AddAsync(new Student()
            {
                Id = schedulingCrudStudentRequestDto.Id,
                Name = schedulingCrudStudentRequestDto.Name
            });
        }
        
        if (schedulingCrudStudentRequestDto.MethodType == "update")
        {
            var studentIndb = await _unitOfWork.Student.GetFirstOrDefaultAsync(e => e.Id == schedulingCrudStudentRequestDto.Id);
            studentIndb.Id = schedulingCrudStudentRequestDto.Id;
            studentIndb.Name = schedulingCrudStudentRequestDto.Name;
            
            _unitOfWork.Student.Update(studentIndb);
        }
        
        if (schedulingCrudStudentRequestDto.MethodType == "delete")
        {
            await _unitOfWork.Student.RemoveAsync(schedulingCrudStudentRequestDto.Id);
        }
        
        _unitOfWork.Save();
    }
}