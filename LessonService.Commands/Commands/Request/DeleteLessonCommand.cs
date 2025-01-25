using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using MediatR;

namespace LessonService.Commands.Commands.Request;

public record DeleteLessonCommand(Guid LessonId): IRequest<ApiResponse<LessonModel>>;