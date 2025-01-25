namespace LessonService.Domain.Entities.Base.Exceptions;

public class RescheduleLessonDateTimeException(Lesson lesson)
    : Exception($"Failed reschedule lesson '{lesson.Name}'. The current start date/time is equals the new one.");