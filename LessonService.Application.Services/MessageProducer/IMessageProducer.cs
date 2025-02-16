namespace LessonService.Application.Services.MessageProducer;

public interface IMessageProducer
{
    void Send<T>(T message);
}