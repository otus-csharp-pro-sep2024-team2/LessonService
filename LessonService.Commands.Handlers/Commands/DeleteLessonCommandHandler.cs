using AutoMapper;
using LessonService.Application.Services.MessageProducer;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.EventMessages;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Commands;

public class DeleteLessonCommandHandler(AppDbContext context,
    ILessonServiceApp lessonServiceApp,
    IMessageProducer messageSender,
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
            SendNotification(lesson);
            return response;    
        }
        catch (Exception ex)
        {
            logger.LogError($"Error deleting lesson: {ex.Message}");
            throw;
        }
    }
    private void SendNotification(Lesson lesson)
    {
        var notification = new Notification(
            $@"Dear Student,
The lesson {lesson.Name} was cancelled.", DateTime.Now);
        foreach(var student in lesson.GetStudents())
        {
            notification.Recipients.Add(student.Id);
        }
        messageSender.Send(notification);
    }
}