using AutoMapper;
using LessonService.Application.Models.Lesson;
using LessonService.Application.Models.System;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base;
using LessonService.Domain.ValueObjects;
using LessonService.Infrastructure.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands;

public class EnrollStudentCommandHandler(
    AppDbContext context,
    ILogger<EnrollStudentCommand> logger) : IRequestHandler<EnrollStudentCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(EnrollStudentCommand command,
        CancellationToken cancellationToken)
    {
        var response = new ApiResponse<bool>
        {
            Data = false
        };
        try
        {
            var lesson = await context.Lessons
                .Include(l => l.LessonGroups)
                .FirstOrDefaultAsync(l => l.Id == command.LessonId, cancellationToken: cancellationToken);            
            if (lesson == null)
            {
                response.Message = $"Lesson with ID: {command.LessonId} not found."; 
                return response;
            }
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
                response.Message = $"Student: {student.Name.Name} is enrolled to this lesson already.";
                return response;
            }            
            
            lesson.EnrollStudent(student);
            await context.SaveChangesAsync(cancellationToken);
            response.Message = "Student enrolled successfully.";
            response.Data = true;
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