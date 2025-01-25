namespace LessonService.Domain.Entities.Base.Exceptions;

public class CompletedAlreadyComletedException(Lesson lesson) : Exception($"Cannot complete the lesson '{lesson.Name}'. Lesson is completed already.");