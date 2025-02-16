using LessonService.Commands.Requests.Commands;
using MediatR;

namespace LessonService.WebApi.Endpoints.Commands.Lesson;

public static class LessonStatusToCompletedEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to set the status of the lesson to Completed
        const string description = "set the status of the lesson to Completed";
        HelperEndpoint.ConfigureEndpoint(app.MapPost($"{HelperEndpoint.baseUrl}/status/completed",
                async (LessonStatusToCompletedCommand command, IMediator mediator) =>
                {
                    var result = await mediator.Send(command);
                    return result.Data is not null? Results.Created($"{HelperEndpoint.baseUrl}/{result.Data.Id}", result) : Results.NotFound();
                }), description, description)
            .WithName("SetStatusCompleted");
    }
}