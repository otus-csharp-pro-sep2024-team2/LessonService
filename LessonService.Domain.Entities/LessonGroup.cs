namespace LessonService.Domain.Entities
{
    public class LessonGroup
    {
        public Guid LessonId
        {
            get { return Lesson != null ? Lesson.Id : Guid.Empty; }
            private set => Lesson.Id = value;
        }

        public Guid StudentId
        {
            get { return Student.Id; }
            private set => Student.Id = value;
        }

        public Student Student { get; init; } = new();
        public Lesson Lesson { get; init; } = new();
    }
}
