using LessonService.Domain.Entities;
using LessonService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LessonService.Infrastructure.EF.Configurations;

public class TrainerConfigaration  : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.ToTable("Trainers");
        builder.HasKey(e => new { e.Id });
        builder.Property(x => x.Name)
            .HasConversion(name => name.Name, name => new PersonName(name))
            .IsRequired();

    }
}