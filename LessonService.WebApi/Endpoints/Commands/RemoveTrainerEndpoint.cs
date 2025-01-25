using LessonService.Commands.Commands.Request;
using MediatR;

namespace LessonService.WebApi.Endpoints.Commands;

public static class RemoveTrainerEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to remove a trainer from the lesson
        HelperEndpoint.ConfigureEndpoint(app.MapDelete($"{HelperEndpoint.baseUrl}/trainer/{{lessonId:guid}}",
                async (Guid lessonId, IMediator mediator) =>
                {
                    var response = await mediator.Send(new RemoveTrainerCommand(lessonId));
                    return response;
                }), 
                "Remove a trainer from the lesson", "Endpoint to remove a trainer from the lesson")
            .WithName("RemoveTrainer");
    }
}