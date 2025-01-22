using LessonService.Interfaces;

namespace LessonService.WebApi.Endpoints;

public static class RemoveTrainerEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to remove a trainer from the lesson
        HelperEndpoint.ConfigureEndpoint(app.MapDelete($"{HelperEndpoint.baseUrl}/trainer/{{lessonId:guid}}",
                async (Guid lessonId, ILessonServiceApp lessonServiceApp) =>
                {
                    var result = await lessonServiceApp.RemoveTrainerAsync(lessonId);
                    return result ? Results.NoContent() : Results.NotFound();
                }), "Remove a trainer from the lesson", "Endpoint to remove a trainer from the lesson")
            .WithName("RemoveTrainer");
    }
}