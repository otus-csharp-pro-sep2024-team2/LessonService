namespace LessonService.Domain.Entities.Base.Exceptions;

public class InvalidLessonStatusValueException(int lessonStatus) : Exception($"Invalid lesson's status: {lessonStatus}");