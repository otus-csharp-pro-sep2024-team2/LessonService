namespace LessonService.Application.Models.Lesson;

public record UpdateLessonRequest
(
    string? Name,
    string? Description,
    int? MaxStudents,
    int? LessonType,
    int? TrainingLevel
);