using AutoMapper;
using LessonService.Commands.Commands.Request;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Commands.Handlers;


public class ChangeLessonStatusCommandHandler(
    AppDbContext context,
    ILessonServiceApp lessonServiceApp,
    ILogger<ChangeLessonStatusCommandHandler> logger,
    IMapper mapper) : IRequestHandler<ChangeLessonStatusCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(ChangeLessonStatusCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await lessonServiceApp.FindLesson(command.LessonId, cancellationToken);
            lesson.SetStatus((LessonStatus)command.LessonStatus);
            await context.SaveChangesAsync(cancellationToken);

            var response = new ApiResponse<LessonModel>
            {
                Message = "Lesson's status was changed.",
                Data = mapper.Map<LessonModel>(lesson)
            };
            logger.LogInformation(response.Message);
            return response;    
        }
        catch (Exception ex)
        {
            logger.LogError($"Error changing lesson's status: {ex.Message}");
            throw;
        }
    }
}