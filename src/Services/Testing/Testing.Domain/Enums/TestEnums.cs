namespace Testing.Domain.Enums
{
    public enum QuestionType
    {
        MultipleChoice,
        SingleChoice,
        Text,
        Code
    }

    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }

    public enum TestAttemptStatus
    {
        InProgress,
        Completed,
        Abandoned
    }

    public enum ReviewStatus
    {
        NotReviewed,
        InReview,
        Reviewed
    }
}