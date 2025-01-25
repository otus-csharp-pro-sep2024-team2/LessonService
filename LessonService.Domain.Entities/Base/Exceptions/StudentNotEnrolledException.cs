namespace LessonService.Domain.Entities.Base.Exceptions;

public class StudentNotEnrolledException(Student student): Exception($"Student '{student.Name}' is not enrolled in the lesson");