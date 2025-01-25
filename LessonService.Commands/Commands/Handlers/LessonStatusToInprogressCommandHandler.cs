﻿using LessonService.Commands.Commands.Request;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Interfaces;
using MediatR;

namespace LessonService.Commands.Commands.Handlers;


public class LessonStatusToInprogressCommandHandler(ILessonServiceApp lessonServiceApp)
    : IRequestHandler<LessonStatusToInprogressCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(LessonStatusToInprogressCommand command, CancellationToken cancellationToken)
    {
        return await lessonServiceApp.SetLessonStatus(command.LessonId, LessonStatus.InProgress, cancellationToken);
    }
}