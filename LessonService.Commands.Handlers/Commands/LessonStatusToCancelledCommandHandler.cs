using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Interfaces;
using MediatR;

namespace LessonService.Commands.Commands;


public class LessonStatusToCancelledCommandHandler(ILessonServiceApp lessonServiceApp)
    : IRequestHandler<LessonStatusToCancelledCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(LessonStatusToCancelledCommand command, CancellationToken cancellationToken)
    {
        return await lessonServiceApp.SetLessonStatus(command.LessonId, LessonStatus.Cancelled, cancellationToken);
    }
}