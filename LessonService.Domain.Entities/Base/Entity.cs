namespace LessonService.Domain.Entities.Base
{
    public abstract class Entity<TId>(TId id)
    {
        public TId Id { get; set; } = id;

    }
}
