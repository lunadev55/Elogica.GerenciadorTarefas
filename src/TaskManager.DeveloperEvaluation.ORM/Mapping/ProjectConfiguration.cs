using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.DeveloperEvaluation.Domain.Entities;

namespace TaskManager.DeveloperEvaluation.ORM.Mapping;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
               .HasColumnType("uniqueidentifier")
               .HasDefaultValueSql("NEWID()");

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.Description)
               .HasMaxLength(1000);

        builder.Property(p => p.StartDate)
               .IsRequired();

        builder.Property(p => p.EndDate);

        builder.Property(p => p.Status)
               .HasConversion<string>()
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(p => p.UserId)
               .HasColumnType("uniqueidentifier")
               .IsRequired();

        builder.Property(p => p.CreatedAt)
               .IsRequired();

        builder.Property(p => p.UpdatedAt);
        
        builder.HasMany(p => p.Tasks)
               .WithOne(t => t.Project)
               .HasForeignKey(t => t.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}