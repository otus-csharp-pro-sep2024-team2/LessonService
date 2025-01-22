namespace LessonService.Application.Models.Lesson;

public record AssignTrainerRequest(Guid LessonId, Guid TrainerId);