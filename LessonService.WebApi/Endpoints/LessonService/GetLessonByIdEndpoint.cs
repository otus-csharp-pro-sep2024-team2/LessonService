using LessonService.Interfaces;

namespace LessonService.WebApi.Endpoints;

public static class GetLessonByIdEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to get lesson by ID
        HelperEndpoint.ConfigureEndpoint(app.MapGet($"{HelperEndpoint.baseUrl}/{{id:guid}}",
                async (Guid id, ILessonServiceApp lessonServiceApp) =>
            {
                var result = await lessonServiceApp.GetLessonByIdAsync(id);
                return Results.Ok(result);
            }), "Get lesson by Id", "Endpoint to get lesson by Id")
            .WithName("GetLesson");
    }
}