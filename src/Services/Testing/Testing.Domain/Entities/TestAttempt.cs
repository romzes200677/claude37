using System;
using System.Collections.Generic;
using Testing.Domain.Enums;

namespace Testing.Domain.Entities
{
    public class TestAttempt
    {
        public Guid Id { get; set; }
        public Guid TestTemplateId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int Score { get; set; }
        public TestAttemptStatus Status { get; set; }
        public int TimeSpentSeconds { get; set; }
        public bool IsPassed { get; set; }
        public string Feedback { get; set; }
        public ReviewStatus ReviewStatus { get; set; }
        public Guid? ReviewerId { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public string ReviewNotes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Навигационные свойства
        public TestTemplate TestTemplate { get; set; }
        public ICollection<TestQuestionResponse> QuestionResponses { get; set; }
    }
}