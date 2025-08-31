using Courses.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.Data
{
    /// <summary>
    /// Контекст базы данных для микросервиса Courses
    /// </summary>
    public class CoursesDbContext : DbContext
    {
        public CoursesDbContext(DbContextOptions<CoursesDbContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonMaterial> LessonMaterials { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<LessonProgress> LessonProgresses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<FeedbackComment> FeedbackComments { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyQuestionOption> SurveyQuestionOptions { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }
        public DbSet<SurveyQuestionResponse> SurveyQuestionResponses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationSettings> NotificationSettings { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<CourseView> CourseViews { get; set; }
        public DbSet<UserInterest> UserInterests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация Course
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(2000);
                entity.Property(e => e.DifficultyLevel).HasMaxLength(50);
                entity.Property(e => e.ImageUrl).HasMaxLength(500);
            });

            // Конфигурация Module
            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.HasOne(e => e.Course)
                    .WithMany(c => c.Modules)
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация Lesson
            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.LessonType).HasMaxLength(50);

                entity.HasOne(e => e.Module)
                    .WithMany(m => m.Lessons)
                    .HasForeignKey(e => e.ModuleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация LessonMaterial
            modelBuilder.Entity<LessonMaterial>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.MaterialType).HasMaxLength(50);
                entity.Property(e => e.Url).HasMaxLength(500);

                entity.HasOne(e => e.Lesson)
                    .WithMany(l => l.Materials)
                    .HasForeignKey(e => e.LessonId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация Enrollment
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(e => e.Course)
                    .WithMany(c => c.Enrollments)
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация LessonProgress
            modelBuilder.Entity<LessonProgress>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(e => e.Lesson)
                    .WithMany(l => l.LessonProgresses)
                    .HasForeignKey(e => e.LessonId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация CourseView
            modelBuilder.Entity<CourseView>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IpAddress).HasMaxLength(50);
                entity.Property(e => e.ReferralSource).HasMaxLength(500);
                entity.Property(e => e.Device).HasMaxLength(100);

                entity.HasOne(e => e.Course)
                    .WithMany()
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация UserInterest
            modelBuilder.Entity<UserInterest>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Interest).IsRequired().HasMaxLength(100);

                // Создаем уникальный индекс для комбинации UserId и Interest
                entity.HasIndex(e => new { e.UserId, e.Interest }).IsUnique();
            });
        }
    }
}