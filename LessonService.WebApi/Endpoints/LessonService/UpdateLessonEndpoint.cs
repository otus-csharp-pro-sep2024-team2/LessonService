using LessonService.Application.Models.Lesson;
using LessonService.Interfaces;

namespace LessonService.WebApi.Endpoints;

public static class UpdateLessonEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to update data of the lesson
        HelperEndpoint.ConfigureEndpoint(app.MapPatch("/lessons/{id:guid}",
                async (Guid id, UpdateLessonRequest updateRequest, ILessonServiceApp lessonServiceApp) =>
            {
                var result = await lessonServiceApp.UpdateLessonAsync(id, updateRequest);
                return result is not null ? Results.Created($"{HelperEndpoint.baseUrl}/{result.Id}", result) : Results.NotFound();
            }), "Update the lesson's data", "Update the lesson's data")
            .WithName("UpdateLesson");
    }
}