using System;
using System.Collections.Generic;

namespace Testing.Domain.Entities
{
    public class TestQuestionResponse
    {
        public Guid Id { get; set; }
        public Guid TestAttemptId { get; set; }
        public Guid QuestionId { get; set; }
        public string ResponseText { get; set; }
        public int? PointsEarned { get; set; }
        public bool? IsCorrect { get; set; }
        public string AiEvaluation { get; set; }
        public string ReviewerFeedback { get; set; }
        public bool IsReviewed { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Навигационные свойства
        public TestAttempt TestAttempt { get; set; }
        public TestQuestion Question { get; set; }
        public ICollection<TestQuestionOptionResponse> OptionResponses { get; set; }
    }
}