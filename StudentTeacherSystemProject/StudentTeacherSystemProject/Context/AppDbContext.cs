using Microsoft.EntityFrameworkCore;
using StudentSystem.Server.Model;
using StudentTeacherSystemProject.Model;

namespace StudentSystem.Server.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Veritabanı tabloları için DbSet'ler tanımlayın
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<StudentCoursesSelections> StudentCoursesSelections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Instructor-Course Foreign Key
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.Instructor_ID)
                .OnDelete(DeleteBehavior.Cascade);

            // Course-StudentCourseSelection Foreign Key
            modelBuilder.Entity<StudentCoursesSelections>()
               .HasOne(c => c.Student)
               .WithMany(i => i.StudentCoursesSelection)
               .HasForeignKey(c => c.Student_ID)
               .OnDelete(DeleteBehavior.Cascade);

            // Student-StudentCourseSelection Foreign Key
            modelBuilder.Entity<StudentCoursesSelections>()
               .HasOne(c => c.Course)
               .WithMany(i => i.StudentCoursesSelection)
               .HasForeignKey(c => c.Course_ID)
               .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

        }
    }
}


