using LessonService.Application.Models.Lesson;
using LessonService.Commands;
using LessonService.Interfaces;
using MediatR;

namespace LessonService.WebApi.Endpoints;

public static class EnrollStudentEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to enroll a student to the lesson
        HelperEndpoint.ConfigureEndpoint(app.MapPost($"{HelperEndpoint.baseUrl}/student",
                async (EnrollStudentCommand command, IMediator mediator) =>
                {
                    var result = await mediator.Send(command);
                    return result;
                }), "Enroll a student to the lesson", "Endpoint to enroll a student to the lesson")
            .WithName("EnrollStudent");
    }
}