using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = TaskManager.DeveloperEvaluation.Domain.Entities.Task;

namespace TaskManager.DeveloperEvaluation.ORM.Mapping;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(t => t.Id);
                
        builder.Property(t => t.Id)
               .HasColumnType("uniqueidentifier")
               .HasDefaultValueSql("NEWID()");

        builder.Property(t => t.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(t => t.Description)
               .HasMaxLength(1000);

        builder.Property(t => t.DueDate)
               .IsRequired();

        builder.Property(t => t.Status)
               .HasConversion<string>()
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(t => t.Priority)
               .HasConversion<string>()
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(t => t.ProjectId)
               .HasColumnType("uniqueidentifier")
               .IsRequired();

        builder.Property(t => t.UserId)
               .HasColumnType("uniqueidentifier")
               .IsRequired();

        builder.Property(t => t.CreatedAt)
               .IsRequired();

        builder.Property(t => t.UpdatedAt);
                
        builder.HasOne(t => t.Project)
               .WithMany(p => p.Tasks)
               .HasForeignKey(t => t.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}