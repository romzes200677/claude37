using System;
using System.Collections.Generic;
using Testing.Domain.Enums;

namespace Testing.Domain.Entities
{
    public class TestQuestion
    {
        public Guid Id { get; set; }
        public Guid TestTemplateId { get; set; }
        public string QuestionText { get; set; }
        public QuestionType QuestionType { get; set; }
        public int OrderIndex { get; set; }
        public int Points { get; set; }
        public bool IsRequired { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public string Explanation { get; set; }
        public string Settings { get; set; }
        public string MediaUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Навигационные свойства
        public TestTemplate TestTemplate { get; set; }
        public ICollection<TestQuestionOption> Options { get; set; }
        public ICollection<TestQuestionResponse> Responses { get; set; }
    }
}