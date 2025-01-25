using AutoMapper;
using LessonService.Commands.Commands.Request;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Commands.Handlers;

public class RescheduleLessonCommandHandler(
    AppDbContext context, IMapper mapper, ILessonServiceApp lessonServiceApp,
    ILogger<RescheduleLessonCommand> logger) : IRequestHandler<RescheduleLessonCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(RescheduleLessonCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await lessonServiceApp.FindLesson(command.LessonId, cancellationToken);
            lesson.Reschedule(DateTime.SpecifyKind(command.DateFrom, DateTimeKind.Utc), command.Duration);
            await context.SaveChangesAsync(cancellationToken);

            var response = new ApiResponse<LessonModel>
            {
                Message = "Lesson was rescheduled.",
                Data = mapper.Map<LessonModel>(lesson)
            };
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