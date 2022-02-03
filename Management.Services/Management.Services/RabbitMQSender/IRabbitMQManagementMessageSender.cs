using Management.Services.MessageBus;

namespace Management.Services.RabbitMQSender;

public interface IRabbitMQManagementMessageSender
{
    void SendMessage(BaseMessage baseMessage, String queueName);
}