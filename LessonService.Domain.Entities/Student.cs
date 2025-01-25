using LessonService.Domain.Entities.Base;
using LessonService.Domain.Entities.Interfaces;
using LessonService.Domain.ValueObjects;

namespace LessonService.Domain.Entities;

public class Student(Guid id, PersonName name) : Person(id, name), IStudent
{
    public Student() : this(Guid.Empty, new PersonName("Empty"))
    {
    }
    public IEnumerable<LessonGroup>? LessonGroups { get; set; }
    
}