using System;
using System.Collections.Generic;
using Testing.Domain.Enums;

namespace Testing.Domain.Entities
{
    public class TestTemplate
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TimeLimit { get; set; }
        public int PassingScore { get; set; }
        public int MaxAttempts { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public Guid AuthorId { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? ModuleId { get; set; }
        public Guid? LessonId { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Instructions { get; set; }
        public string Settings { get; set; }

        // Навигационные свойства
        public ICollection<TestQuestion> Questions { get; set; }
        public ICollection<TestAttempt> Attempts { get; set; }
        public ICollection<TestCategory> Categories { get; set; }
        public ICollection<TestTag> Tags { get; set; }
        public ICollection<TestTemplateTag> TestTemplateTags { get; set; }
        public ICollection<TestTemplateCategory> TestTemplateCategories { get; set; }
    }
}