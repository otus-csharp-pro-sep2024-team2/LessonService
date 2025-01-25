using AutoMapper;
using LessonService.Commands.Commands.Request;
using LessonService.Domain.Entities.Base.Exceptions;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Domain.ValueObjects;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Commands.Handlers;

public class EnrollStudentCommandHandler(
    AppDbContext context,
    IMapper mapper,
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
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error enrolling student: {ex.Message}");
            throw;
        }
        
    }
}