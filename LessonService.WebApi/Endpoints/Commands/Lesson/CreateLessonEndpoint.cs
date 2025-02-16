using LessonService.Commands.Requests.Commands;
using MediatR;

namespace LessonService.WebApi.Endpoints.Commands.Lesson;

public static class CreateLessonEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        HelperEndpoint.ConfigureEndpoint(app.MapPost(HelperEndpoint.baseUrl,
                async (CreateLessonCommand command, IMediator mediator) =>
                {
                    var result = await mediator.Send(command);
                    return result.Data is not null? Results.Created($"{HelperEndpoint.baseUrl}/{result.Data.Id}", result) : Results.NotFound();
                }), "Create a lesson", "Endpoint to create lesson")
            .WithName("CreateLesson");
    }    
}