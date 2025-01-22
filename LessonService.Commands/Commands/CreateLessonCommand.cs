using LessonService.Application.Models.Lesson;
using LessonService.Application.Models.System;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.ValueObjects;
using MediatR;

namespace LessonService.Commands;

public record CreateLessonCommand(
    string Name,
    string Description,
    DateTime DateFrom,
    int Duration,
    TrainingLevel TrainingLevel, 
    LessonType LessonType, 
    int MaxStudents) : IRequest<ApiResponse<LessonResponse>>;
