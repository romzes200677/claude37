using System;

namespace Testing.Domain.Entities
{
    public class TestQuestionOptionResponse
    {
        public Guid Id { get; set; }
        public Guid QuestionResponseId { get; set; }
        public Guid OptionId { get; set; }
        public bool IsSelected { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Навигационные свойства
        public TestQuestionResponse QuestionResponse { get; set; }
        public TestQuestionOption Option { get; set; }
    }
}