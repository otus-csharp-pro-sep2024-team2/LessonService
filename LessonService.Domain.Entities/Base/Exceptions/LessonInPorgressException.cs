namespace LessonService.Domain.Entities.Base.Exceptions;

public class LessonInPorgressException(Lesson lesson) : Exception($"Lesson '{lesson.Name}' is completed");
