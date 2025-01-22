using LessonService.Domain.Entities.Base;
using LessonService.Domain.Entities.Interfaces;
using LessonService.Domain.ValueObjects;

namespace LessonService.Domain.Entities;

public class Trainer(Guid id, PersonName name) : Person(id, name), ITrainer
{
    public Trainer() : this(Guid.Empty, new PersonName("Empty"))
    {
    }
    public ICollection<Lesson>? Lessons { get; set; }    
}