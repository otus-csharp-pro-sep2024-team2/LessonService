using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using MediatR;

namespace LessonService.Commands.Commands.Request;

public record UpdateLessonCommand
(
    Guid LessonId,
    string? Name,
    string? Description,
    int? MaxStudents,
    int? LessonType,
    int? TrainingLevel
) : IRequest<ApiResponse<LessonModel>>;