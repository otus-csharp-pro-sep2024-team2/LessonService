using AutoMapper;
using LessonService.Commands.Commands.Request;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Domain.ValueObjects;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Commands.Handlers;


public class AssignTrainerCommandHandler(
    AppDbContext context,
    ILessonServiceApp lessonServiceApp,
    ILogger<AssignTrainerCommandHandler> logger,
    IMapper mapper) : IRequestHandler<AssignTrainerCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(AssignTrainerCommand command, CancellationToken cancellationToken)
    {
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

            var response = new ApiResponse<LessonModel>
            {
                Message = "Trainer was assigned.",
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