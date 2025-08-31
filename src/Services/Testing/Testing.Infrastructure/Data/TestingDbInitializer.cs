using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Testing.Domain.Entities;
using Testing.Domain.Enums;

namespace Testing.Infrastructure.Data
{
    public class TestingDbInitializer
    {
        private readonly TestingDbContext _context;
        private readonly ILogger<TestingDbInitializer> _logger;

        public TestingDbInitializer(TestingDbContext context, ILogger<TestingDbInitializer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            try
            {
                // Apply migrations if they are not applied
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }

                // Seed data only if the database is empty
                await SeedDataAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }

        private async Task SeedDataAsync()
        {
            // Seed categories if they don't exist
            if (!await _context.TestCategories.AnyAsync())
            {
                var programmingCategory = new TestCategory
                {
                    Name = "Programming",
                    Description = "Tests related to programming skills"
                };

                var csharpCategory = new TestCategory
                {
                    Name = "C#",
                    Description = "Tests related to C# programming language"
                };

                var dotnetCategory = new TestCategory
                {
                    Name = ".NET",
                    Description = "Tests related to .NET framework and ecosystem"
                };

                await _context.TestCategories.AddRangeAsync(programmingCategory);
                await _context.SaveChangesAsync();

                // Set parent category after saving to get the ID
                csharpCategory.ParentCategoryId = programmingCategory.Id;
                dotnetCategory.ParentCategoryId = programmingCategory.Id;

                await _context.TestCategories.AddRangeAsync(csharpCategory, dotnetCategory);
                await _context.SaveChangesAsync();
            }

            // Seed tags if they don't exist
            if (!await _context.TestTags.AnyAsync())
            {
                var tags = new List<TestTag>
                {
                    new TestTag { Name = "Beginner" },
                    new TestTag { Name = "Intermediate" },
                    new TestTag { Name = "Advanced" },
                    new TestTag { Name = "OOP" },
                    new TestTag { Name = "LINQ" },
                    new TestTag { Name = "ASP.NET" },
                    new TestTag { Name = "Entity Framework" },
                    new TestTag { Name = "Algorithms" }
                };

                await _context.TestTags.AddRangeAsync(tags);
                await _context.SaveChangesAsync();
            }

            // Seed a sample test template if none exist
            if (!await _context.TestTemplates.AnyAsync())
            {
                var csharpCategory = await _context.TestCategories.FirstOrDefaultAsync(c => c.Name == "C#");
                var beginnerTag = await _context.TestTags.FirstOrDefaultAsync(t => t.Name == "Beginner");
                var oopTag = await _context.TestTags.FirstOrDefaultAsync(t => t.Name == "OOP");

                if (csharpCategory != null && beginnerTag != null && oopTag != null)
                {
                    var testTemplate = new TestTemplate
                    {
                        Title = "C# Basics Assessment",
                        Description = "A basic test to assess fundamental C# knowledge",
                        Instructions = "Answer all questions. Each question has only one correct answer unless specified otherwise.",
                        TimeLimit = 30, // 30 minutes
                        PassingScore = 70, // 70%
                        DifficultyLevel = DifficultyLevel.Easy,
                        IsActive = true,
                        IsPublished = true,
                        AuthorId = Guid.NewGuid(), // System generated test
                        Questions = new List<TestQuestion>
                        {
                            new TestQuestion
                            {
                                QuestionText = "What is a correct syntax to declare a variable in C#?",
                                QuestionType = QuestionType.SingleChoice,
                                DifficultyLevel = DifficultyLevel.Easy,
                                Points = 1,
                                OrderIndex = 1,
                                Options = new List<TestQuestionOption>
                                {
                                    new TestQuestionOption { OptionText = "var x = 5;", IsCorrect = true, OrderIndex = 1 },
                                    new TestQuestionOption { OptionText = "variable x = 5;", IsCorrect = false, OrderIndex = 2 },
                                    new TestQuestionOption { OptionText = "x := 5;", IsCorrect = false, OrderIndex = 3 },
                                    new TestQuestionOption { OptionText = "int x := 5;", IsCorrect = false, OrderIndex = 4 }
                                }
                            },
                            new TestQuestion
                            {
                                QuestionText = "Which of the following are valid C# access modifiers? (Select all that apply)",
                                QuestionType = QuestionType.MultipleChoice,
                                DifficultyLevel = DifficultyLevel.Easy,
                                Points = 2,
                                OrderIndex = 2,
                                Options = new List<TestQuestionOption>
                                {
                                    new TestQuestionOption { OptionText = "public", IsCorrect = true, OrderIndex = 1 },
                                    new TestQuestionOption { OptionText = "private", IsCorrect = true, OrderIndex = 2 },
                                    new TestQuestionOption { OptionText = "protected", IsCorrect = true, OrderIndex = 3 },
                                    new TestQuestionOption { OptionText = "internal", IsCorrect = true, OrderIndex = 4 },
                                    new TestQuestionOption { OptionText = "external", IsCorrect = false, OrderIndex = 5 },
                                    new TestQuestionOption { OptionText = "friendly", IsCorrect = false, OrderIndex = 6 }
                                }
                            },
                            new TestQuestion
                            {
                                QuestionText = "Explain the concept of inheritance in object-oriented programming and provide a simple example in C#.",
                                QuestionType = QuestionType.Text,
                                DifficultyLevel = DifficultyLevel.Medium,
                                Points = 5,
                                OrderIndex = 3
                            }
                        }
                    };

                    await _context.TestTemplates.AddAsync(testTemplate);
                    await _context.SaveChangesAsync();

                    // Add template-category relationship
                    var templateCategory = new TestTemplateCategory
                    {
                        TestTemplateId = testTemplate.Id,
                        TestCategoryId = csharpCategory.Id
                    };

                    await _context.TestTemplateCategories.AddAsync(templateCategory);

                    // Add template-tag relationships
                    var templateTags = new List<TestTemplateTag>
                    {
                        new TestTemplateTag
                        {
                            TestTemplateId = testTemplate.Id,
                            TestTagId = beginnerTag.Id
                        },
                        new TestTemplateTag
                        {
                            TestTemplateId = testTemplate.Id,
                            TestTagId = oopTag.Id
                        }
                    };

                    await _context.TestTemplateTags.AddRangeAsync(templateTags);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}