using LessonService.Application.Models.Lesson;
using LessonService.Domain.Entities;

namespace LessonService.Interfaces;

public interface ILessonServiceApp
{
    Task<IEnumerable<LessonResponse>> GetAllLessonsAsync();
    Task<LessonResponse?> GetLessonByIdAsync(Guid id);
    Task<LessonResponse?> UpdateLessonAsync(Guid id, UpdateLessonRequest lessonInfo);
    Task<bool> DeleteLessonAsync(Guid id);
    Task<bool> RemoveTrainerAsync(Guid lessonId);
    Task<bool> RemoveStudentAsync(Guid lessonId, Guid studentId);
    Task<List<string>> GetAllStudentsOfLessonAsync(Guid lessonId);
    Task<LessonResponse?> RescheduleAsync(Guid id, RescheduleRequest updateRequest);
    Task<Lesson?> FindLesson(Guid lessonId, CancellationToken cancellationToken);
    Task<LessonGroup?> FindGroup(Guid lessonId, Guid studentId);
}
