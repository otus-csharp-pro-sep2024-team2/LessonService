using LessonService.Application.Models.System;
using MediatR;

namespace LessonService.Commands;

public record EnrollStudentCommand(Guid LessonId, Guid StudentId, string StudentName):  IRequest<ApiResponse<bool>>;
