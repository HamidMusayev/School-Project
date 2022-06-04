using Microsoft.EntityFrameworkCore;
using SchoolProject.Models.Classes;

namespace SchoolProject.Models.Contexts
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Class>().ToTable("Class");
            modelBuilder.Entity<Lesson>().ToTable("Lesson");
            modelBuilder.Entity<Exam>().ToTable("Exam");
            modelBuilder.Entity<Result>().ToTable("Result");

            modelBuilder.Entity<Exam>().HasOne(e => e.Teacher).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Exam>().HasOne(e => e.Class).WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lesson>().HasOne(l => l.Class).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Lesson>().HasOne(l => l.Teacher).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
