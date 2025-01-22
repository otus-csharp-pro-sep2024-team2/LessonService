using LessonService.Interfaces;

namespace LessonService.WebApi.Endpoints;

public static class GetAllLessonsEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to get all lessons
        HelperEndpoint.ConfigureEndpoint(app.MapGet(HelperEndpoint.baseUrl, async (ILessonServiceApp lessonServiceApp) =>
            {
                var result = await lessonServiceApp.GetAllLessonsAsync();
                return Results.Ok(result);
            }), "Get all lessons", "Endpoint to get all lessons") 
            .WithName("GetAllLessons");
        
    }
}