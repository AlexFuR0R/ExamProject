using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualificationCoursesExam.Models {
    public class AppDbContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Course)
                .WithMany(c => c.Registrations)
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Student)
                .WithMany(u => u.RegistrationsAsStudent)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.CoursesAsTeacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Course>()
            .Property(c => c.Title)
            .IsRequired();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RSI5KJL;Database=QualificationCourses;Trusted_Connection=True;TrustServerCertificate=True;");
        }

    }
}