namespace LessonService.Domain.Entities.Base.Exceptions;

public class StudentNotEnrolledException(Guid id): Exception($"Student ID {id} is not enrolled to the lesson");