using AutoMapper;
using LessonService.Application.Models.Lesson;
using LessonService.Application.Models.System;
using LessonService.Domain.Entities.Base.Exceptions;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands;

public class RescheduleLessonCommandHandler(
    AppDbContext context, IMapper mapper, ILessonServiceApp lessonServiceApp,
    ILogger<RescheduleLessonCommand> logger) : IRequestHandler<RescheduleLessonCommand, ApiResponse<LessonResponse>>
{
    public async Task<ApiResponse<LessonResponse>> Handle(RescheduleLessonCommand command,
        CancellationToken cancellationToken)
    {
        var response = new ApiResponse<LessonResponse>();
        try
        {
            var lesson = await lessonServiceApp.FindLesson(command.LessonId, cancellationToken);
            lesson.Reschedule(command.DateFrom, command.Duration);
            await context.SaveChangesAsync(cancellationToken);
            response.Message = "Lesson is rescheduled  successfully.";
            response.Data = mapper.Map<LessonResponse>(lesson);
            logger.LogInformation(response.Message);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw;
        }
    }
}