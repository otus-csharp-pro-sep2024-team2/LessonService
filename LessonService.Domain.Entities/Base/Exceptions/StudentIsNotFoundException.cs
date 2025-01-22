namespace LessonService.Domain.Entities.Base.Exceptions;

public class StudentIsNotFoundException(Guid id) : Exception($"Student ID: {id} not found.");