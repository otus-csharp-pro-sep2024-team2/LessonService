namespace LessonService.Domain.Entities.Base.Exceptions;

public class LessonHasNoErolledStudent(Lesson lesson) : Exception($"Lesson '{lesson.Name}' has no enrolled students");
