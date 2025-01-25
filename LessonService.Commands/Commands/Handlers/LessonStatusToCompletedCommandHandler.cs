﻿using LessonService.Commands.Commands.Request;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Interfaces;
using MediatR;

namespace LessonService.Commands.Commands.Handlers;


public class LessonStatusToCompletedCommandHandler(ILessonServiceApp lessonServiceApp)
    : IRequestHandler<LessonStatusToCompletedCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(LessonStatusToCompletedCommand command, CancellationToken cancellationToken)
    {
        return await lessonServiceApp.SetLessonStatus(command.LessonId, LessonStatus.Completed, cancellationToken);
    }
}