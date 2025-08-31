using System;
using System.Collections.Generic;

namespace Testing.Domain.Entities
{
    public class TestCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Навигационные свойства
        public TestCategory ParentCategory { get; set; }
        public ICollection<TestCategory> ChildCategories { get; set; }
        public ICollection<TestTemplate> TestTemplates { get; set; }
    }
}