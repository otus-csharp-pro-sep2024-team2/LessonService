using AutoMapper;
using LessonService.Commands.Requests.Queries;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Queries.Handlers;

public class GetLessonByIdQueryHandler(
    IMapper mapper,
    ILessonServiceApp lessonServiceApp,
    ILogger<GetLessonByIdQuery> logger) : IRequestHandler<GetLessonByIdQuery, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(GetLessonByIdQuery query, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<LessonModel>();
        try
        {
            var lesson = await lessonServiceApp.FindLesson(query.LessonId, new CancellationToken());
            response.Data = mapper.Map<LessonModel>(lesson);
            response.Message = "Lesson has been loaded successfully";
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error getting lesson: {ex.Message}");
            throw;
        }        
    }
}