using LessonService.Application.Models.System;
using LessonService.Commands;
using MediatR;

namespace LessonService.WebApi.Endpoints;

public static class HelperEndpoint
{
    public const string baseUrl = "/lessons";
    public const string lessonTag = "LessonService";

    public static RouteHandlerBuilder ConfigureEndpoint(RouteHandlerBuilder builder, string summary,
        string description)
    {
        return builder.WithTags("LessonService").WithSummary(summary).WithDescription(description);
    }
   
}