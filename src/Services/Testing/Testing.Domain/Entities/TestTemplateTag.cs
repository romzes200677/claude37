using System;

namespace Testing.Domain.Entities
{
    public class TestTemplateTag
    {
        public Guid TestTemplateId { get; set; }
        public Guid TestTagId { get; set; }

        // Навигационные свойства
        public TestTemplate TestTemplate { get; set; }
        public TestTag TestTag { get; set; }
    }
}