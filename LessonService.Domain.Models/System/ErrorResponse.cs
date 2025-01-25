namespace LessonService.Domain.Models.System;

public record ErrorResponse(string Title, string Message, int StatusCode);
