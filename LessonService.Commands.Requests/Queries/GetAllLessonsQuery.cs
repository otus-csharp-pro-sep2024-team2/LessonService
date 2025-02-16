using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using MediatR;

namespace LessonService.Commands.Requests.Queries;

public record GetAllLessonsQuery : IRequest<ApiResponse<IEnumerable<LessonModel>>>;