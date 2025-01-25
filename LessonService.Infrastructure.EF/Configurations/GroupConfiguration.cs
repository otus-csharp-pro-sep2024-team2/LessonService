using LessonService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LessonService.Infrastructure.EF.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<LessonGroup>
{
    public void Configure(EntityTypeBuilder<LessonGroup> builder)
    {
        // Установка первичного ключа
        builder.HasKey(lg => new { lg.LessonId, lg.StudentId });

        // Настройка связи с сущностью Lesson
        builder.HasOne(lg => lg.Lesson)
            .WithMany(l => l.LessonGroups)
            .HasForeignKey(lg => lg.LessonId);

        // Настройка связи с сущностью Student
        builder.HasOne(lg => lg.Student)
            .WithMany(s => s.LessonGroups)
            .HasForeignKey(s => s.StudentId);
        //.UsingEntity(j => j.ToTable("LessonGroupStudents"));
    }
}