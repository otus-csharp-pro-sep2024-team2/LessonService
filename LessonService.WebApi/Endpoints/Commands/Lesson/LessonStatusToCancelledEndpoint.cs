using LessonService.Commands.Requests.Commands;
using MediatR;

namespace LessonService.WebApi.Endpoints.Commands.Lesson;

public static class LessonStatusToCancelledEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to set the status of the lesson to Cancelled
        const string description = "set the status of the lesson to Cancelled";
        HelperEndpoint.ConfigureEndpoint(app.MapPost($"{HelperEndpoint.baseUrl}/status/cancelled",
                async (LessonStatusToCancelledCommand command, IMediator mediator) =>
                {
                    var result = await mediator.Send(command);
                    return result.Data is not null? Results.Created($"{HelperEndpoint.baseUrl}/{result.Data.Id}", result) : Results.NotFound();
                }), description, description)
            .WithName("SetStatusInCancelled");
    }
}