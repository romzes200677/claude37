using System;
using System.Collections.Generic;

namespace Testing.Domain.Entities
{
    public class TestTag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Навигационные свойства
        public ICollection<TestTemplate> TestTemplates { get; set; }
        public ICollection<TestTemplateTag> TestTemplateTags { get; set; }
    }
}