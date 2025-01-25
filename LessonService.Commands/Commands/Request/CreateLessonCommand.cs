using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using MediatR;

namespace LessonService.Commands.Commands.Request;

public record CreateLessonCommand(
    string Name,
    string Description,
    DateTime DateFrom,
    int Duration,
    TrainingLevel TrainingLevel, 
    LessonType LessonType, 
    int MaxStudents) : IRequest<ApiResponse<LessonModel>>;
