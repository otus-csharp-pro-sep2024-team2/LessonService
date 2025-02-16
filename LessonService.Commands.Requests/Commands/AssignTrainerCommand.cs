using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using MediatR;

namespace LessonService.Commands.Requests.Commands;

public record AssignTrainerCommand(Guid LessonId, Guid TrainerId, string TrainerName):  IRequest<ApiResponse<LessonModel>>;