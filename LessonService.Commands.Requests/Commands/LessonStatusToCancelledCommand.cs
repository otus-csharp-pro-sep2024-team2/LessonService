using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using MediatR;

namespace LessonService.Commands.Requests.Commands;

public record LessonStatusToCancelledCommand(Guid LessonId):  IRequest<ApiResponse<LessonModel>>;