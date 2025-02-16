using LessonService.Commands.Requests.Queries;
using MediatR;

namespace LessonService.WebApi.Endpoints.Queries;

public static class GetAllStudentsOfLessonEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to get all students are enrolled to the lesson
        HelperEndpoint.ConfigureEndpoint(app.MapGet($"{HelperEndpoint.baseUrl}/student/{{lessonId:guid}}",
                    async (Guid lessonId, IMediator mediator) =>
                    {
                        var result = await mediator.Send(new GetAllStudentsOfLessonQuery(lessonId));
                        return Results.Ok(result);
                    }), "Get all students are enrolled to the lesson",
                "Endpoint to get all students are enrolled to the lesson")
            .WithName("GetAllStudentsOfLesson");
    }
}