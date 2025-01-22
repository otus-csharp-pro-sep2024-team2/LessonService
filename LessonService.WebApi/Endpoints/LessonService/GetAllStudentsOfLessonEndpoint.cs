using LessonService.Interfaces;

namespace LessonService.WebApi.Endpoints;

public static class GetAllStudentsOfLessonEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to get all students are enrolled to the lesson
        HelperEndpoint.ConfigureEndpoint(app.MapGet($"{HelperEndpoint.baseUrl}/student/{{lessonId:guid}}",
                    async (Guid lessonId, ILessonServiceApp lessonServiceApp) =>
                    {
                        var result = await lessonServiceApp.GetAllStudentsOfLessonAsync(lessonId);
                        return Results.Ok(result);
                    }), "Get all students are enrolled to the lesson",
                "Endpoint to get all students are enrolled to the lesson")
            .WithName("GetAllStudentsOfLesson");
        
    }
}