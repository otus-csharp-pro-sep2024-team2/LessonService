using LessonService.Commands.Queries.Request;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Queries.Handlers;

public class GetLessonStudentsQueryHandler(
    AppDbContext context,
    ILessonServiceApp lessonServiceApp,
    ILogger<GetAllStudentsOfLessonQuery> logger) : IRequestHandler<GetAllStudentsOfLessonQuery, ApiResponse<List<StudentModel>>>
{
    public async Task<ApiResponse<List<StudentModel>>> Handle(GetAllStudentsOfLessonQuery query, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<List<StudentModel>>();
        try
        {
            var lesson = await lessonServiceApp.FindLesson(query.LessonId, new CancellationToken());
            response.Data = await context.LessonGroups
                .Where(lg => lg.LessonId == query.LessonId)
                .Select(p => new StudentModel(p.StudentId, p.Student.Name.Name))
                .ToListAsync(cancellationToken: cancellationToken);
            response.Message = "Students list has been loaded.";
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error getting lesson: {ex.Message}.");
            throw;
        }        
    }
}