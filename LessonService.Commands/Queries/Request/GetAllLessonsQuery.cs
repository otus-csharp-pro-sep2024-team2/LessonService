using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using MediatR;

namespace LessonService.Commands.Queries.Request;

public record GetAllLessonsQuery : IRequest<ApiResponse<IEnumerable<LessonModel>>>;