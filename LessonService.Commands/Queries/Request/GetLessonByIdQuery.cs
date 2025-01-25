using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using MediatR;

namespace LessonService.Commands.Queries.Request;

public record GetLessonByIdQuery(Guid LessonId) : IRequest<ApiResponse<LessonModel>>;