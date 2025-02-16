namespace LessonService.Domain.Models.System;

/// <summary>
/// Represents a response with a data payload and a message.
/// </summary>
/// <typeparam name="T">The type of the data payload.</typeparam>
public class ApiResponse<T>
{
    public T? Data { get; set; }
    public string Message { get; set; }

    public ApiResponse()
    {
        Message = "";
    }

    public ApiResponse(T data, string message)
    {
        Data = data;
        Message = message;
    }
}
