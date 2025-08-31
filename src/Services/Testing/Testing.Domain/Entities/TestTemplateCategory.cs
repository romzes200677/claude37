using System;

namespace Testing.Domain.Entities
{
    public class TestTemplateCategory
    {
        public Guid TestTemplateId { get; set; }
        public Guid TestCategoryId { get; set; }

        // Навигационные свойства
        public TestTemplate TestTemplate { get; set; }
        public TestCategory TestCategory { get; set; }
    }
}