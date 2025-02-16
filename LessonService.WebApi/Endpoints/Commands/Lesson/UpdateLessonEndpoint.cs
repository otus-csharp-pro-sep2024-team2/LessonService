using LessonService.Commands.Requests.Commands;
using MediatR;

namespace LessonService.WebApi.Endpoints.Commands.Lesson;

public static class UpdateLessonEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to update data of the lesson
        HelperEndpoint.ConfigureEndpoint(app.MapPatch("/lessons",
                async (UpdateLessonCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result;
            }), "Update the lesson's data", "Update the lesson's data")
            .WithName("UpdateLesson");
    }
}