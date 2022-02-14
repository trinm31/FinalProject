using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SchedulingGenerate.Services.Messages;
using SchedulingGenerate.Services.Models;
using SchedulingGenerate.Services.Services.IRepository;
using SchedulingGenerate.Services.Services.Repository;

namespace SchedulingGenerate.Services.Messaging;

public class RabbitMQSchedulingCreateStudentCourseConsumer: Microsoft.Extensions.Hosting.BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private readonly StudentExamRepository _studentExamRepository;

    public RabbitMQSchedulingCreateStudentCourseConsumer(StudentExamRepository studentExamRepository)
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
        _channel.QueueDeclare(queue: "schedulingcreatestudentcoursemessagequeue", false, false, false, arguments: null);
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            SchedulingCreateStudentCourseRequestDto schedulingCrudStudentCourseRequestDto = JsonConvert.DeserializeObject<SchedulingCreateStudentCourseRequestDto>(content);
            HandleCrudStudentCourseMessage(schedulingCrudStudentCourseRequestDto).GetAwaiter().GetResult();

            _channel.BasicAck(ea.DeliveryTag, false);
        };
        _channel.BasicConsume("schedulingcreatestudentcoursemessagequeue", false, consumer);

        return Task.CompletedTask;
    }

    private async Task HandleCrudStudentCourseMessage(SchedulingCreateStudentCourseRequestDto schedulingCrudStudentCourseRequestDto)
    {
        foreach (var studentCourse in schedulingCrudStudentCourseRequestDto.StudentCourses)
        {
            await _studentExamRepository.Create(new StudentCourse()
            {
                StudentId = studentCourse.StudentId,
                CourseId = studentCourse.CourseId
            });
        }
       
    }
}