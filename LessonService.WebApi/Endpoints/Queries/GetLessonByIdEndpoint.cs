using LessonService.Commands.Requests.Queries;
using MediatR;

namespace LessonService.WebApi.Endpoints.Queries;

public static class GetLessonByIdEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to get lesson by ID
        HelperEndpoint.ConfigureEndpoint(app.MapGet($"{HelperEndpoint.baseUrl}/{{lessonId:guid}}",
                async (Guid lessonId, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetLessonByIdQuery(lessonId));
                return Results.Ok(result);
            }), "Get lesson by Id", "Endpoint to get lesson by Id")
            .WithName("GetLesson");
    }
}