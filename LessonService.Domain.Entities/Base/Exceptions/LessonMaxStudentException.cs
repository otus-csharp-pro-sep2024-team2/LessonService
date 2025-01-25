namespace LessonService.Domain.Entities.Base.Exceptions;

public class LessonMaxStudentException(Lesson lesson) : Exception($"Lesson '{lesson.Name}' has max students assigned already");