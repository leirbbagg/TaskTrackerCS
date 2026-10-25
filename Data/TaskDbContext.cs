using Microsoft.EntityFrameworkCore;
using TaskTrackerCS.Models;

namespace TaskTrackerCS.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base(options) { }

        public DbSet<TaskToDo> Tasks => Set<TaskToDo>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskToDo>(e =>
            {
                e.ToTable("Tasks");
                e.HasKey(t => t.Id);

                e.Property(t => t.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                e.Property(t => t.Status)
                    .HasConversion<string>()
                    .HasMaxLength(20);
            });
        }
    }
}