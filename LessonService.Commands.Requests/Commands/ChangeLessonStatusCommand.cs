using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using MediatR;

namespace LessonService.Commands.Requests.Commands;

public record ChangeLessonStatusCommand(Guid LessonId, int LessonStatus):  IRequest<ApiResponse<LessonModel>>;