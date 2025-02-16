using LessonService.Commands.Requests.Commands;
using MediatR;

namespace LessonService.WebApi.Endpoints.Commands.Student;

public static class UnEnrollStudentEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to un-enroll a student from the lesson
        HelperEndpoint.ConfigureEndpoint(app.MapPatch($"{HelperEndpoint.baseUrl}/student",
                async (UnEnrollStudentCommand command, IMediator mediator) =>
                {
                    var result = await mediator.Send(command);
                    return result;
                }), "Un-enroll a student from the lesson", "Endpoint to un-enroll a student from the lesson")
            .WithName("UnEnrollStudent");
    }
}