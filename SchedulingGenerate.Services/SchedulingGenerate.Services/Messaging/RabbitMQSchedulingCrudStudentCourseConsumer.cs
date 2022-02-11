using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SchedulingGenerate.Services.Messages;
using SchedulingGenerate.Services.Models;
using SchedulingGenerate.Services.Services.IRepository;
using SchedulingGenerate.Services.Services.Repository;

namespace SchedulingGenerate.Services.Messaging;

public class RabbitMQSchedulingCrudStudentCourseConsumer: Microsoft.Extensions.Hosting.BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private readonly StudentExamRepository _studentExamRepository;

    public RabbitMQSchedulingCrudStudentCourseConsumer(StudentExamRepository studentExamRepository)
    {
        //Todo: clean here
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        _studentExamRepository = studentExamRepository;
        
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "schedulingcrudstudentcoursemessagequeue", false, false, false, arguments: null);
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            SchedulingCrudStudentCourseRequestDto schedulingCrudStudentCourseRequestDto = JsonConvert.DeserializeObject<SchedulingCrudStudentCourseRequestDto>(content);
            HandleCrudStudentCourseMessage(schedulingCrudStudentCourseRequestDto).GetAwaiter().GetResult();

            _channel.BasicAck(ea.DeliveryTag, false);
        };
        _channel.BasicConsume("schedulingcrudstudentcoursemessagequeue", false, consumer);

        return Task.CompletedTask;
    }

    private async Task HandleCrudStudentCourseMessage(SchedulingCrudStudentCourseRequestDto schedulingCrudStudentCourseRequestDto)
    {
        if (schedulingCrudStudentCourseRequestDto.MethodType == "create")
        {
            await _studentExamRepository.Create(new StudentCourse()
            { 
                StudentId= schedulingCrudStudentCourseRequestDto.StudentId,
                CourseId = schedulingCrudStudentCourseRequestDto.CourseId
            });
        }
        
        if (schedulingCrudStudentCourseRequestDto.MethodType == "update")
        {
            await _studentExamRepository.Update(new StudentCourse()
            { 
                StudentId= schedulingCrudStudentCourseRequestDto.StudentId,
                CourseId = schedulingCrudStudentCourseRequestDto.CourseId
            },schedulingCrudStudentCourseRequestDto.oldStudentId, 
                schedulingCrudStudentCourseRequestDto.oldCourseId);
        }
        
        if (schedulingCrudStudentCourseRequestDto.MethodType == "delete")
        {
            await _studentExamRepository.Delete(new StudentCourse()
            { 
                StudentId= schedulingCrudStudentCourseRequestDto.StudentId,
                CourseId = schedulingCrudStudentCourseRequestDto.CourseId
            });
        }
    }
}