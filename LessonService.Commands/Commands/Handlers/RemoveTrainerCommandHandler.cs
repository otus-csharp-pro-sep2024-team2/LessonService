using AutoMapper;
using LessonService.Commands.Commands.Request;
using LessonService.Domain.Entities.Base.Exceptions;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Commands.Handlers;

public class RemoveTrainerCommandHandler(AppDbContext context,
    ILessonServiceApp lessonServiceApp,
    ILogger<RemoveTrainerCommandHandler> logger,
    IMapper mapper): IRequestHandler<RemoveTrainerCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(RemoveTrainerCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await lessonServiceApp.FindLesson(command.LessonId, cancellationToken);
            lesson.RemoveTrainer();
            await context.SaveChangesAsync(cancellationToken);

            var response = new ApiResponse<LessonModel>
            {
                Message = "Trainer was removed.",
                Data = mapper.Map<LessonModel>(lesson)
            };
            logger.LogInformation(response.Message);
            return response;    
        }
        catch (Exception ex)
        {
            logger.LogError($"Error assigning trainer: {ex.Message}");
            throw;
        }
    }
}