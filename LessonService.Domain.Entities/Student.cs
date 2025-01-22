using LessonService.Domain.Entities.Base;
using LessonService.Domain.Entities.Interfaces;
using LessonService.Domain.ValueObjects;

namespace LessonService.Domain.Entities;

public class Student(Guid id, PersonName name) : Person(id, name), IStudent
{
    private PersonName _name = name;

    public Student() : this(Guid.NewGuid(), new PersonName("Empty"))
    {
    }

    //public PersonName Name => this.Name;

    public IEnumerable<LessonGroup>? LessonGroups { get; set; }
    
}