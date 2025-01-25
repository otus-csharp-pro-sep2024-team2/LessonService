using AutoMapper;
using LessonService.Commands.Queries.Request;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Queries.Handlers;

public class GetLessonsQueryHandler(
    AppDbContext context,
    IMapper mapper,
    ILogger<GetAllLessonsQuery> logger) : IRequestHandler<GetAllLessonsQuery, ApiResponse<IEnumerable<LessonModel>>>
{
    public async Task<ApiResponse<IEnumerable<LessonModel>>> Handle(GetAllLessonsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var response = new ApiResponse<IEnumerable<LessonModel>>();

            var lessons = await context.Lessons
                .Where(lesson => true)
                .Include(lesson => lesson.Trainer)
                .Include(lesson => lesson.LessonGroups)
                .ThenInclude(lg => lg.Student)
                .ToListAsync(cancellationToken: cancellationToken);
            response.Data = mapper.Map<IEnumerable<LessonModel>>(lessons);
            return response;
            
        }
        catch (Exception e)
        {
            logger.LogError($"Error getting list of lessons: {e.Message}");
            throw;
        }        
    }
}