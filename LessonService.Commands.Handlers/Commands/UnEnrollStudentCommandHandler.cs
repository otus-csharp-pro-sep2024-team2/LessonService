using AutoMapper;
using LessonService.Application.Services.MessageProducer;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base.Exceptions;
using LessonService.Domain.Entities.EventMessages;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Commands;

public class UnEnrollStudentCommandHandler(
    AppDbContext context,
    ILessonServiceApp lessonServiceApp,
    IMessageProducer messageSender,
    IMapper mapper,
    ILogger<UnEnrollStudentCommand> logger) : IRequestHandler<UnEnrollStudentCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(UnEnrollStudentCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await lessonServiceApp.FindLesson(command.LessonId, cancellationToken);
            var student = await context.Students.FindAsync(command.StudentId);
            if (student == null)
            {
                throw new StudentIsNotFoundException(command.StudentId);
            }
            var group = lesson.LessonGroups.FirstOrDefault(p => p.StudentId == command.StudentId);
            if (group == null)
            {
                throw new StudentNotEnrolledException(student);
            }
            context.LessonGroups.Remove(group);
            lesson.UnEnrollStudent(student);
            await context.SaveChangesAsync(cancellationToken);

            var response = new ApiResponse<LessonModel>
            {
                Message = $"Student '{student.Name}' was unenrolled.",
                Data = mapper.Map<LessonModel>(lesson)
            };
            SendNotification(lesson, student);
            logger.LogInformation(response.Message);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw;
        }
    }

    private void SendNotification(Lesson lesson, Student student)
    {
        var notification = new Notification(
            $@"Dear Student {student.Name},
You have been removed from the lesson {lesson.Name}.
The lesson date is {lesson.DateFrom:dd.MM.yyyy HH:mm}.", DateTime.Now);
        notification.Recipients.Add(student.Id);
        messageSender.Send(notification);
    }
}