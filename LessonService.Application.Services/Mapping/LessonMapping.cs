using AutoMapper;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;

namespace LessonService.Application.Services.Mapping;

public class LessonMapping: Profile
{
    public LessonMapping()
    {
        CreateMap<Lesson, LessonModel>()
            .ConstructUsing(src => new LessonModel(
                src.Id,
                src.Name,
                src.Description,
                src.DateFrom,
                src.Duration,
                src.MaxStudents,
                new LessonTypeModel((int)src.LessonType, src.LessonType.ToString()),
                new TrainingLevelModel((int)src.TrainingLevel, src.TrainingLevel.ToString()),
                new LessonStatusModel((int)src.LessonStatus, src.LessonStatus.ToString()),
                src.Trainer != null ? new TrainerModel(src.Trainer.Id, src.Trainer.Name.ToString()) : null,
                src.LessonGroups.Select(lg => new StudentModel(lg.StudentId, lg.Student.Name.ToString())).ToList()
            ));

        CreateMap<Trainer, TrainerModel>()
            .ConstructUsing(src => new TrainerModel(src.Id, src.Name.ToString()));

        CreateMap<Student, StudentModel>()
            .ConstructUsing(src => new StudentModel(src.Id, src.Name.ToString()));
        
        CreateMap<LessonStatus, LessonStatusModel>()
            .ConstructUsing(src => new LessonStatusModel((int)src, src.ToString()));        

        CreateMap<LessonType, LessonTypeModel>()
            .ConstructUsing(src => new LessonTypeModel((int)src, src.ToString()));        
        CreateMap<TrainingLevel, TrainingLevelModel>()
            .ConstructUsing(src => new TrainingLevelModel((int)src, src.ToString()));        
    }
}