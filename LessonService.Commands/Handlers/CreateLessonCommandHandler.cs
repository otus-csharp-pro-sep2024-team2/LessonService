using AutoMapper;
using LessonService.Application.Models.Lesson;
using LessonService.Application.Models.System;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base;
using LessonService.Infrastructure.EF;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands;

public class CreateLessonCommandHandler(
    AppDbContext context,
    ILogger<CreateLessonCommandHandler> logger,
    IMapper mapper) : IRequestHandler<CreateLessonCommand, ApiResponse<LessonResponse>>
{
    public async Task<ApiResponse<LessonResponse>> Handle(CreateLessonCommand lessonInfo, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<LessonResponse>();
        try
        {
            var lesson = new Lesson(
                Guid.Empty,
                lessonInfo.Name,
                lessonInfo.Description,
                DateTime.SpecifyKind(lessonInfo.DateFrom, DateTimeKind.Utc),
                lessonInfo.Duration,
                lessonInfo.TrainingLevel,
                lessonInfo.LessonType,
                lessonInfo.MaxStudents
            );
            context.Lessons.Add(lesson);
            await context.SaveChangesAsync(cancellationToken);
            response.Message = "Lesson created successfully.";
            response.Data = mapper.Map<LessonResponse>(lesson);
            logger.LogInformation(response.Message);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error creating lesson: {ex.Message}");
            throw;
        }
    }
}