using Microsoft.EntityFrameworkCore;
using SimpleTaskManager.Domain.Entities;

namespace SimpleTaskManager.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
            
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.Priority)
                .IsRequired();

            entity.Property(e => e.TaskStatus)
                .IsRequired();
        });
    }
}