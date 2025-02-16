using LessonService.Domain.Entities.Base;

namespace LessonService.Domain.Entities.EventMessages;

public class Notification 
{
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }

    public Notification(string message, DateTime createdAt)
    {
        Message = message;
        CreatedAt = createdAt;
    }

    public Notification() : this( "Empty", DateTime.Now)
    {
    }
    public List<Guid> Recipients { get; set; } = new List<Guid>();
}