namespace LessonService.Domain.Entities.Base.Exceptions;

public class LessonIsNotFoundException(Guid id) : Exception($"Lesson ID {id} is not found");
