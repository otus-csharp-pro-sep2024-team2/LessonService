namespace LessonService.Domain.Entities.Base.Exceptions;

public class TrainerIsNotAssignedException(Lesson lesson) : Exception($"The lesson '{lesson.Name}' does not have any trainer assigned.");
