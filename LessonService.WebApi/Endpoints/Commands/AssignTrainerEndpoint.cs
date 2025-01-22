using LessonService.Application.Models.Lesson;
using LessonService.Commands;
using LessonService.Interfaces;
using MediatR;

namespace LessonService.WebApi.Endpoints;

public static class AssignTrainerEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to assign a trainer to the lesson
        HelperEndpoint.ConfigureEndpoint(app.MapPost($"{HelperEndpoint.baseUrl}/trainer",
                async (AssignTrainerCommand command, IMediator mediator) =>
                {
                    var result = await mediator.Send(command);
                    return result.Data is not null? Results.Created($"{HelperEndpoint.baseUrl}/{result.Data.Id}", result) : Results.NotFound();
                }), "Assign a trainer to the lesson", "Endpoint to assign a trainer to the lesson")
            .WithName("AssignTrainer");
    }
}