namespace LessonService.Domain.Entities.Base.Exceptions;

public class LessonCannotBeScheduledException(Lesson lesson) : Exception($"Lesson '{lesson.Name}' cannot be scheduled");
