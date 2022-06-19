using Microsoft.EntityFrameworkCore;
using SchoolProject.Models.Classes;

namespace SchoolProject.Models.Contexts;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Class> Classes { get; set; }
    public virtual DbSet<Lesson> Lessons { get; set; }
    public virtual DbSet<Exam> Exams { get; set; }
    public virtual DbSet<Result> Results { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<Class>().ToTable("Class");
        modelBuilder.Entity<Lesson>().ToTable("Lesson");
        modelBuilder.Entity<Exam>().ToTable("Exam");
        modelBuilder.Entity<Result>().ToTable("Result");

        modelBuilder.Entity<User>().HasOne(e => e.Class).WithMany().OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Exam>().HasOne(e => e.Lesson).WithMany().OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Lesson>().HasOne(l => l.Class).WithMany().OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Lesson>().HasOne(l => l.Teacher).WithMany().OnDelete(DeleteBehavior.Restrict);
    }
}