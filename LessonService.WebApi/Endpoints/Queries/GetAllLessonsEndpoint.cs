using LessonService.Commands.Requests.Queries;
using MediatR;

namespace LessonService.WebApi.Endpoints.Queries;

public static class GetAllLessonsEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to get all lessons
        HelperEndpoint.ConfigureEndpoint(app.MapGet(HelperEndpoint.baseUrl, async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllLessonsQuery());
                return Results.Ok(result);
            }), "Get all lessons", "Endpoint to get all lessons") 
            .WithName("GetAllLessons");
    }
}