namespace LessonService.Application.Models.Lesson;

public record CreateLessonRequest
(
    string Name,
    string Description,
    DateTime DateFrom,
    int Duration,
    int MaxStudents,
    int LessonType,
    int TrainingLevel,
    Guid TrainerId,
    string TrainerName
);