using LessonService.Commands.Requests.Commands;
using MediatR;

namespace LessonService.WebApi.Endpoints.Commands.Lesson
{
    public static class RescheduleLessonEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            // Endpoint to update data of the lesson
            HelperEndpoint.ConfigureEndpoint(app.MapPost("/lessons/reschedule",
                    async (RescheduleLessonCommand command, IMediator mediator) =>
                    {
                        var result = await mediator.Send(command);
                        return result.Data is not null? Results.Created($"{HelperEndpoint.baseUrl}/{result.Data.Id}", result) : Results.NotFound();
                    }), "Reschedule the lesson", "Endpoint to reschedule the lesson")
                .WithName("RescheduleLesson");
        }
    }
}