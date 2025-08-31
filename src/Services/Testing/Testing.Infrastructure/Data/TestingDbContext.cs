using Microsoft.EntityFrameworkCore;
using Testing.Domain.Entities;

namespace Testing.Infrastructure.Data
{
    public class TestingDbContext : DbContext
    {
        public TestingDbContext(DbContextOptions<TestingDbContext> options) : base(options)
        {
        }

        public DbSet<TestTemplate> TestTemplates { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<TestQuestionOption> TestQuestionOptions { get; set; }
        public DbSet<TestAttempt> TestAttempts { get; set; }
        public DbSet<TestQuestionResponse> TestQuestionResponses { get; set; }
        public DbSet<TestQuestionOptionResponse> TestQuestionOptionResponses { get; set; }
        public DbSet<TestCategory> TestCategories { get; set; }
        public DbSet<TestTag> TestTags { get; set; }
        public DbSet<TestTemplateTag> TestTemplateTags { get; set; }
        public DbSet<TestTemplateCategory> TestTemplateCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure TestTemplate entity
            modelBuilder.Entity<TestTemplate>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(2000);
                entity.Property(e => e.TimeLimit).HasDefaultValue(60); // Default 60 minutes
                entity.Property(e => e.PassingScore).HasDefaultValue(70); // Default 70%
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsPublished).HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Configure TestQuestion entity
            modelBuilder.Entity<TestQuestion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.QuestionText).IsRequired().HasMaxLength(2000);
                entity.Property(e => e.Points).HasDefaultValue(1);
                entity.Property(e => e.OrderIndex).HasDefaultValue(0);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(d => d.TestTemplate)
                      .WithMany(p => p.Questions)
                      .HasForeignKey(d => d.TestTemplateId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure TestQuestionOption entity
            modelBuilder.Entity<TestQuestionOption>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OptionText).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.IsCorrect).HasDefaultValue(false);
                entity.Property(e => e.OrderIndex).HasDefaultValue(0);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(d => d.Question)
                      .WithMany(p => p.Options)
                      .HasForeignKey(d => d.QuestionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure TestAttempt entity
            modelBuilder.Entity<TestAttempt>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Score).HasDefaultValue(0);
                entity.Property(e => e.Status).HasConversion<string>().HasMaxLength(20);
                entity.Property(e => e.ReviewStatus).HasConversion<string>().HasMaxLength(20);
                entity.Property(e => e.StartedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(d => d.TestTemplate)
                      .WithMany(p => p.Attempts)
                      .HasForeignKey(d => d.TestTemplateId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure TestQuestionResponse entity
            modelBuilder.Entity<TestQuestionResponse>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ResponseText).HasMaxLength(4000);
                entity.Property(e => e.PointsEarned).HasDefaultValue(0);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(d => d.TestAttempt)
                      .WithMany(p => p.QuestionResponses)
                      .HasForeignKey(d => d.TestAttemptId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Question)
                      .WithMany()
                      .HasForeignKey(d => d.QuestionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure TestQuestionOptionResponse entity
            modelBuilder.Entity<TestQuestionOptionResponse>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(d => d.QuestionResponse)
                      .WithMany(p => p.OptionResponses)
                      .HasForeignKey(d => d.QuestionResponseId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Option)
                      .WithMany()
                      .HasForeignKey(d => d.OptionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure TestCategory entity
            modelBuilder.Entity<TestCategory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(d => d.ParentCategory)
                      .WithMany(p => p.ChildCategories)
                      .HasForeignKey(d => d.ParentCategoryId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired(false);
            });

            // Configure TestTag entity
            modelBuilder.Entity<TestTag>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(e => e.Name).IsUnique();
            });

            // Configure TestTemplateTag entity (many-to-many relationship)
            modelBuilder.Entity<TestTemplateTag>(entity =>
            {
                entity.HasKey(e => new { e.TestTemplateId, e.TestTagId });

                entity.HasOne(d => d.TestTemplate)
                      .WithMany(p => p.TestTemplateTags)
                      .HasForeignKey(d => d.TestTemplateId);

                entity.HasOne(d => d.TestTag)
                      .WithMany(p => p.TestTemplateTags)
                      .HasForeignKey(d => d.TestTagId);
            });

            // Configure TestTemplateCategory entity (many-to-many relationship)
            modelBuilder.Entity<TestTemplateCategory>(entity =>
            {
                entity.HasKey(e => new { e.TestTemplateId, e.TestCategoryId });

                entity.HasOne(d => d.TestTemplate)
                      .WithMany(p => p.TestTemplateCategories)
                      .HasForeignKey(d => d.TestTemplateId);

                entity.HasOne(d => d.TestCategory)
                      .WithMany()
                      .HasForeignKey(d => d.TestCategoryId);
            });
        }
    }
}