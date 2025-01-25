namespace LessonService.Domain.Entities.Base.Exceptions;

public class LessonInProgressException(Lesson lesson) : Exception($"Lesson '{lesson.Name}' is in progress");