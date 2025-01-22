using LessonService.Application.Models.System;
using MediatR;

namespace LessonService.Commands;

public record UnEnrollStudentCommand(Guid LessonId, Guid StudentId):  IRequest<ApiResponse<bool>>;