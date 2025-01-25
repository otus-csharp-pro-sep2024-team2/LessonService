using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using MediatR;

namespace LessonService.Commands.Commands.Request;

public record RescheduleLessonCommand(Guid LessonId, DateTime DateFrom, int Duration):  IRequest<ApiResponse<LessonModel>>;