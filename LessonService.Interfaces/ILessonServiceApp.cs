using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;

namespace LessonService.Interfaces;

public interface ILessonServiceApp
{
    Task<Lesson> FindLesson(Guid lessonId, CancellationToken cancellationToken);
    Task<LessonGroup?> FindGroup(Guid lessonId, Guid studentId);
    Task<ApiResponse<LessonModel>> SetLessonStatus(Guid lessonId, LessonStatus lessonStatus, CancellationToken cancellationToken);
}
