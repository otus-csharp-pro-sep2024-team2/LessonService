using LessonService.Domain.Entities;
using LessonService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LessonService.Infrastructure.EF.Configurations;

public class StudentConfigaration  : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");
        builder.HasKey(e => new { e.Id });
        builder.Property(x => x.Name)
            .HasConversion(name => name.Name, name => new PersonName(name))
            .IsRequired();

    }
}