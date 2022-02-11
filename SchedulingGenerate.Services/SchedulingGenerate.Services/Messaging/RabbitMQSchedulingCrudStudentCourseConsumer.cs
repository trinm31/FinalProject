using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SchedulingGenerate.Services.Messages;
using SchedulingGenerate.Services.Models;
using SchedulingGenerate.Services.Services.IRepository;

namespace SchedulingGenerate.Services.Messaging;

public class RabbitMQSchedulingCrudStudentCourseConsumer: Microsoft.Extensions.Hosting.BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private readonly IUnitOfWork _unitOfWork;

    public RabbitMQSchedulingCrudStudentCourseConsumer(IUnitOfWork unitOfWork)
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
            await _unitOfWork.StudentExam.AddAsync(new StudentCourse()
            { 
                StudentId= schedulingCrudStudentCourseRequestDto.StudentId,
                CourseId = schedulingCrudStudentCourseRequestDto.CourseId
            });
        }
        
        if (schedulingCrudStudentCourseRequestDto.MethodType == "update")
        {
            var studentCourseIndb = await _unitOfWork.StudentExam.GetFirstOrDefaultAsync(e => e.Id == schedulingCrudStudentCourseRequestDto.Id);
            studentCourseIndb.StudentId = schedulingCrudStudentCourseRequestDto.StudentId;
            studentCourseIndb.CourseId = schedulingCrudStudentCourseRequestDto.CourseId;
            
            await _unitOfWork.StudentExam.Update(studentCourseIndb);
        }
        
        if (schedulingCrudStudentCourseRequestDto.MethodType == "delete")
        {
            await _unitOfWork.StudentExam.RemoveAsync(schedulingCrudStudentCourseRequestDto.Id);
        }
        
        _unitOfWork.Save();
    }
}