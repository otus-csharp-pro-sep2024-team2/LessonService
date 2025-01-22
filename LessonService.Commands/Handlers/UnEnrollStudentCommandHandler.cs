using LessonService.Application.Models.System;
using LessonService.Domain.Entities.Base.Exceptions;
using LessonService.Domain.ValueObjects;
using LessonService.Infrastructure.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands;

public class UnEnrollStudentCommandHandler(
    AppDbContext context,
    ILogger<UnEnrollStudentCommand> logger) : IRequestHandler<UnEnrollStudentCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(UnEnrollStudentCommand command,
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
                throw new LessonIsNotFoundException(command.LessonId); 
                return response;
            }
            var student = await context.Students.FindAsync(command.StudentId);
            if (student == null)
            {
                throw new StudentIsNotFoundException(command.StudentId);
            }
            var group = lesson.LessonGroups.FirstOrDefault(p => p.StudentId == command.StudentId);
            if (group == null)
            {
                throw new StudentNotEnrolledException(command.StudentId);
                return response;
            }
            context.LessonGroups.Remove(group);
            //lesson.UnEnrollStudent(student);
            await context.SaveChangesAsync(cancellationToken);
            response.Message = "Student UnEnrolled successfully.";
            response.Data = true;
            logger.LogInformation(response.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw;
        }
        return response;
    }
}