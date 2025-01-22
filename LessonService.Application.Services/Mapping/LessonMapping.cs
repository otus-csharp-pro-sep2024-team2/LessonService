using AutoMapper;
using LessonService.Application.Models.Lesson;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base;

namespace LessonService.Application.Services.Mapping;

public class LessonMapping: Profile
{
    public LessonMapping()
    {
        CreateMap<Lesson, LessonResponse>();
    }
}