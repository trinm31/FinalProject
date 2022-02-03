using SchedulingGenerate.Services.MessageBus;

namespace SchedulingGenerate.Services.RabbitMQSender;

public interface IRabbitMQSchedulingSVMessageSender
{
    void SendMessage(BaseMessage baseMessage, String queueName);
}