namespace LessonService.Domain.Entities.Base.Exceptions;

public class LessonCompletedException(Lesson lesson) : Exception($"Lesson '{lesson.Name}' is completed");
