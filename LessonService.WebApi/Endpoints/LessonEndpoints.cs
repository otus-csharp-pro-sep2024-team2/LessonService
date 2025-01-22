using LessonService.WebApi.Endpoints.Commands;
using LessonService.WebApi.Endpoints.LessonService;

namespace LessonService.WebApi.Endpoints;

public static class LessonEndpoints
{
    /// <summary>
    /// LessonService Endpoints
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapLessonEndPoint(this IEndpointRouteBuilder app)
    {
        CreateLessonEndpoint.Map(app);
        GetAllLessonsEndpoint.Map(app);
        DeleteLessonEndpoint.Map(app);
        GetLessonByIdEndpoint.Map(app);
        AssignTrainerEndpoint.Map(app);
        RemoveTrainerEndpoint.Map(app);
        EnrollStudentEndpoint.Map(app);
        UnEnrollStudentEndpoint.Map(app);
        GetAllStudentsOfLessonEndpoint.Map(app);
        UpdateLessonEndpoint.Map(app);
        RescheduleEndpoint.Map(app);
        return app;
    }

}