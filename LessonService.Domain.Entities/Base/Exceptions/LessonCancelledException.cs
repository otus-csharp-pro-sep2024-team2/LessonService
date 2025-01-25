namespace LessonService.Domain.Entities.Base.Exceptions;

public class LessonCancelledException(Lesson lesson) : Exception($"Lesson '{lesson.Name}' is cancelled");
