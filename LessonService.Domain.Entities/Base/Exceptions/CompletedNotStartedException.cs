namespace LessonService.Domain.Entities.Base.Exceptions;

public class CompletedNotStartedException(Lesson lesson) : Exception($"Cannot complete the lesson '{lesson.Name}'. Lesson is not started yet.");