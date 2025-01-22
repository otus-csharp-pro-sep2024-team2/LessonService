using LessonService.Interfaces;

namespace LessonService.WebApi.Endpoints.LessonService;

public static class DeleteLessonEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to delete lesson by ID
        HelperEndpoint.ConfigureEndpoint(app.MapDelete($"{HelperEndpoint.baseUrl}/{{id:guid}}",
                async (Guid id, ILessonServiceApp lessonServiceApp) =>
            {
                var result = await lessonServiceApp.DeleteLessonAsync(id);
                return result ? Results.NoContent() : Results.NotFound();
            }), "Delete lesson", "Endpoint to delete lesson by Id")
            .WithName("DeleteLesson");
    }
}