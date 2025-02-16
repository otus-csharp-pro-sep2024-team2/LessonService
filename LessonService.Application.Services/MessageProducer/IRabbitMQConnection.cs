namespace LessonService.Application.Services.MessageProducer;

public interface IRabbitMQConnection
{
    string GetHost();
    int GetPort();
    string GetQueueName();
    string GetUserName();
    string GetUserPassword();    
}