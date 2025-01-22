using LessonService.Application.Models.Lesson;
using LessonService.Application.Models.System;
using MediatR;

namespace LessonService.Commands;

public record RescheduleLessonCommand(Guid LessonId, DateTime DateFrom, int Duration):  IRequest<ApiResponse<LessonResponse>>;