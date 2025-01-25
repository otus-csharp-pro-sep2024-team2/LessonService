namespace LessonService.Domain.Entities.Base.Exceptions;

public class RescheduleLessonDurationException(Lesson lesson)
    : Exception($"Failed reschedule lesson '{lesson.Name}'. The duration value must be grater than 1 min");