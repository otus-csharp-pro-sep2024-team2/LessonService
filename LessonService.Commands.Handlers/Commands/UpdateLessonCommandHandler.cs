using AutoMapper;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Commands;


public class UpdateLessonCommandHandler(
    AppDbContext context,
    ILessonServiceApp lessonServiceApp,
    ILogger<UpdateLessonCommandHandler> logger,
    IMapper mapper) : IRequestHandler<UpdateLessonCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(UpdateLessonCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await lessonServiceApp.FindLesson(command.LessonId, new CancellationToken());
            if (command.MaxStudents.HasValue) lesson.SetMaxStudents(command.MaxStudents.Value);
            if (command.Name is not null) lesson.SetName(command.Name);
            if (command.Description is not null) lesson.SetDescription(command.Description);
            if (command.LessonType.HasValue) lesson.SetLessonType((LessonType)command.LessonType);
            if (command.TrainingLevel.HasValue) lesson.SetTrainingLevel((TrainingLevel)command.TrainingLevel);

            await context.SaveChangesAsync(cancellationToken);

            var response = new ApiResponse<LessonModel>
            {
                Message = "Lesson was updated.",
                Data = mapper.Map<LessonModel>(lesson)
            };
            logger.LogInformation(response.Message);
            return response;    
        }
        catch (Exception ex)
        {
            logger.LogError($"Error updating the lesson: {ex.Message}");
            throw;
        }
    }
}