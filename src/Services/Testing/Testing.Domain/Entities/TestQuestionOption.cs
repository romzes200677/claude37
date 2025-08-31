using System;
using System.Collections.Generic;

namespace Testing.Domain.Entities
{
    public class TestQuestionOption
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public int OrderIndex { get; set; }
        public string Explanation { get; set; }
        public string MediaUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Навигационные свойства
        public TestQuestion Question { get; set; }
        public ICollection<TestQuestionOptionResponse> OptionResponses { get; set; }
    }
}