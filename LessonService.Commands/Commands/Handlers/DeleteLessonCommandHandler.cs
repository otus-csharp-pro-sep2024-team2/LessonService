using AutoMapper;
using LessonService.Commands.Commands.Request;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Commands.Handlers;

public class DeleteLessonCommandHandler(AppDbContext context,
    ILessonServiceApp lessonServiceApp,
    ILogger<DeleteLessonCommandHandler> logger,
    IMapper mapper): IRequestHandler<DeleteLessonCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(DeleteLessonCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await lessonServiceApp.FindLesson(command.LessonId, cancellationToken);
            lesson.ValidateLesson(); 
            context.LessonGroups.RemoveRange(lesson.LessonGroups);
            context.Lessons.Remove(lesson);
            await context.SaveChangesAsync(cancellationToken);

            var response = new ApiResponse<LessonModel>
            {
                Message = "Lesson was deleted.",
                Data = mapper.Map<LessonModel>(lesson)
            };
            logger.LogInformation(response.Message);
            return response;    
        }
        catch (Exception ex)
        {
            logger.LogError($"Error deleting lesson: {ex.Message}");
            throw;
        }
    }
}