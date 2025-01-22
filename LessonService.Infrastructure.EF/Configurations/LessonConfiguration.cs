using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LessonService.Infrastructure.EF.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("Lessons");
        builder.HasKey(e => e.Id);
//        builder.HasOne(e => e.Trainer).WithOne().HasForeignKey<Trainer>(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.TrainerId).IsRequired(false);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(500).IsRequired(false);
        builder.Property(e => e.DateFrom).IsRequired();
        builder.Property(e => e.Duration).IsRequired().HasDefaultValue(60);
        builder.Property(e => e.TrainingLevel).IsRequired().HasDefaultValue(TrainingLevel.Beginner).HasSentinel(TrainingLevel.Beginner);
        builder.Property(e => e.LessonType).IsRequired().HasDefaultValue(LessonType.None).HasSentinel(LessonType.None);
        builder.Property(e => e.MaxStudents).IsRequired().HasDefaultValue(1);
        builder.Property(e => e.LessonStatus).IsRequired().HasDefaultValue(LessonStatus.Scheduled).HasSentinel(LessonStatus.Scheduled);
        builder.HasOne(l => l.Trainer)
            .WithMany(t => t.Lessons)
            .HasForeignKey(l => l.TrainerId)
            .IsRequired(false); 
        builder.HasMany(l => l.LessonGroups)
            .WithOne(lg => lg.Lesson)
            .HasForeignKey(lg => lg.LessonId);
        
            // builder.HasData(
            //     new Lesson("Lesson 1", "Description 1") { Trainer = { } },
            //     new Lesson("Lesson 2", "Description 2")
            // );
                
    }
}