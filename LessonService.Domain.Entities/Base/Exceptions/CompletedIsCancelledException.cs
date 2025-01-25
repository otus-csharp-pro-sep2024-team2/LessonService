namespace LessonService.Domain.Entities.Base.Exceptions;

public class CompletedIsCancelledException(Lesson lesson) : Exception($"Cannot complete the lesson '{lesson.Name}'. Lesson is canceled");