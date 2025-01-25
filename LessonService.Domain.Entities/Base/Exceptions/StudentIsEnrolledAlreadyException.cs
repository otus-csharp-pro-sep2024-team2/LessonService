namespace LessonService.Domain.Entities.Base.Exceptions;

public class StudentIsEnrolledAlreadyException(Lesson lesson, Student student)
    : Exception($"Student '{student.Name}' is already enrolled in the lesson '{lesson.Name}'.");