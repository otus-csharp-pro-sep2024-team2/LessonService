using LessonService.Commands.Requests.Commands;
using MediatR;

namespace LessonService.WebApi.Endpoints.Commands.Lesson;

public static class DeleteLessonEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to delete lesson by ID
        HelperEndpoint.ConfigureEndpoint(app.MapDelete($"{HelperEndpoint.baseUrl}/{{lessonId:guid}}",
                    async (Guid lessonId, IMediator mediator) =>
                    {
                        var response = await mediator.Send(new DeleteLessonCommand(lessonId));
                        return response;
                    }),  
                "Delete lesson", "Endpoint to delete lesson by Id")
            .WithName("DeleteLesson");
    }
}