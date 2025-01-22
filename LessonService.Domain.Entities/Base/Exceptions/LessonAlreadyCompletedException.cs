namespace LessonService.Domain.Entities.Base.Exceptions;

public class LessonAlreadyCompletedException(Lesson lesson) : Exception($"Lesson {lesson.Id} has been completed");