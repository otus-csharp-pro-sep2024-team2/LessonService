using AutoMapper;
using LessonService.Application.Services.MessageProducer;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base.Exceptions;
using LessonService.Domain.Entities.EventMessages;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Domain.ValueObjects;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Commands;

public class EnrollStudentCommandHandler(
    AppDbContext context,
    IMapper mapper,
    IMessageProducer messageSender,
    ILessonServiceApp lessonServiceApp,
    ILogger<EnrollStudentCommand> logger) : IRequestHandler<EnrollStudentCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(EnrollStudentCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await lessonServiceApp.FindLesson(command.LessonId, cancellationToken);
            var student = await context.Students.FindAsync(command.StudentId);
            if (student == null)
            {
                student = new(command.StudentId, new PersonName(command.StudentName));
                context.Students.Add(student);
            }
            else
            {
                student.Name = new(command.StudentName);
            }
            var group = lesson.LessonGroups.FirstOrDefault(p => p.StudentId == command.StudentId);
            if (group != null)
            {
                throw new StudentIsEnrolledAlreadyException(lesson, student);
            }            
            
            lesson.EnrollStudent(student);
            await context.SaveChangesAsync(cancellationToken);
     
            var response = new ApiResponse<LessonModel>
            {
                Message = $"Student '{student.Name}' was enrolled.",
                Data = mapper.Map<LessonModel>(lesson)
            };
            logger.LogInformation(response.Message);
            SendNotification(lesson, student);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error enrolling student: {ex.Message}");
            throw;
        }
        
    }

    private void SendNotification(Lesson lesson, Student student)
    {
        var notification = new Notification(
            $@"Dear Student {student.Name},
You are enrolled in the lesson {lesson.Name}.
The lesson will start at {lesson.DateFrom:dd.MM.yyyy HH:mm}.
The duration of the lesson is {lesson.Duration} minutes.", DateTime.Now);
        notification.Recipients.Add(student.Id);
        messageSender.Send(notification);
    }
}