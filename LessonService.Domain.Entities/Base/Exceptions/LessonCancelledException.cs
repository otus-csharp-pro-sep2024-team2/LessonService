namespace LessonService.Domain.Entities.Base.Exceptions;

public class LessonCancelledException(Lesson lesson) : Exception($"Lesson {lesson.Id} is cancelled");
