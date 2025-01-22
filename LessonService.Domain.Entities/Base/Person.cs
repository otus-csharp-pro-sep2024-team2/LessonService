using LessonService.Domain.ValueObjects;

namespace LessonService.Domain.Entities.Base;

public class Person(Guid id, PersonName name) : Entity<Guid>(id)
{
    public PersonName Name { get; set; } = name;
}
