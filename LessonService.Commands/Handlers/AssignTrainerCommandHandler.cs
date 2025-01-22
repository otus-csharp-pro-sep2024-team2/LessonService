using AutoMapper;
using LessonService.Application.Models.Lesson;
using LessonService.Application.Models.System;
using LessonService.Domain.Entities.Base;
using LessonService.Domain.ValueObjects;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands;


public class AssignTrainerCommandHandler(
    AppDbContext context,
    ILessonServiceApp lessonServiceApp,
    ILogger<AssignTrainerCommandHandler> logger,
    IMapper mapper) : IRequestHandler<AssignTrainerCommand, ApiResponse<LessonResponse>>
{
    public async Task<ApiResponse<LessonResponse>> Handle(AssignTrainerCommand command, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<LessonResponse>();
        try
        {
            var lesson = await lessonServiceApp.FindLesson(command.LessonId, cancellationToken);
            var trainer = await context.Trainers.FindAsync(command.TrainerId);
            if (trainer == null)
            {
                trainer = new(command.TrainerId, new PersonName(command.TrainerName));
                context.Trainers.Add(trainer);
            }
            else
            {
                trainer.Name = new(command.TrainerName);
            }
            lesson.AssignTrainer(trainer);
            await context.SaveChangesAsync(cancellationToken);
            response.Message = "Trainer assigned successfully.";
            response.Data = mapper.Map<LessonResponse>(lesson);
            logger.LogInformation(response.Message);
            return response;    
        }
        catch (Exception ex)
        {
            logger.LogError($"Error getting lesson: {ex.Message}");
            throw;
        }
    }
}