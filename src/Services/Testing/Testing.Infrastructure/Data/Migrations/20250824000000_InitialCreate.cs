using Microsoft.EntityFrameworkCore.Migrations;

namespace Testing.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create TestTemplates table
            migrationBuilder.CreateTable(
                name: "TestTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    TimeLimit = table.Column<int>(nullable: false, defaultValue: 60),
                    PassingScore = table.Column<int>(nullable: false, defaultValue: 70),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    IsPublished = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTemplates", x => x.Id);
                });

            // Create TestCategories table
            migrationBuilder.CreateTable(
                name: "TestCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    ParentCategoryId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCategories_TestCategories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "TestCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            // Create TestTags table
            migrationBuilder.CreateTable(
                name: "TestTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTags", x => x.Id);
                });

            // Create TestQuestions table
            migrationBuilder.CreateTable(
                name: "TestQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TestTemplateId = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(maxLength: 2000, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    Difficulty = table.Column<string>(maxLength: 50, nullable: false),
                    Points = table.Column<int>(nullable: false, defaultValue: 1),
                    Order = table.Column<int>(nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestQuestions_TestTemplates_TestTemplateId",
                        column: x => x.TestTemplateId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create TestAttempts table
            migrationBuilder.CreateTable(
                name: "TestAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TestTemplateId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Score = table.Column<int>(nullable: false, defaultValue: 0),
                    Status = table.Column<string>(maxLength: 20, nullable: false),
                    ReviewStatus = table.Column<string>(maxLength: 20, nullable: false),
                    StartedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CompletedAt = table.Column<DateTime>(nullable: true),
                    TimeSpentSeconds = table.Column<int>(nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestAttempts_TestTemplates_TestTemplateId",
                        column: x => x.TestTemplateId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            // Create TestQuestionOptions table
            migrationBuilder.CreateTable(
                name: "TestQuestionOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(maxLength: 1000, nullable: false),
                    IsCorrect = table.Column<bool>(nullable: false, defaultValue: false),
                    Order = table.Column<int>(nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestQuestionOptions_TestQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "TestQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create TestQuestionResponses table
            migrationBuilder.CreateTable(
                name: "TestQuestionResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TestAttemptId = table.Column<Guid>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false),
                    TextResponse = table.Column<string>(maxLength: 4000, nullable: true),
                    Score = table.Column<int>(nullable: false, defaultValue: 0),
                    IsCorrect = table.Column<bool>(nullable: false, defaultValue: false),
                    ReviewerFeedback = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestionResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestQuestionResponses_TestAttempts_TestAttemptId",
                        column: x => x.TestAttemptId,
                        principalTable: "TestAttempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestQuestionResponses_TestQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "TestQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            // Create TestQuestionOptionResponses table
            migrationBuilder.CreateTable(
                name: "TestQuestionOptionResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    QuestionResponseId = table.Column<Guid>(nullable: false),
                    OptionId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestionOptionResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestQuestionOptionResponses_TestQuestionOptions_OptionId",
                        column: x => x.OptionId,
                        principalTable: "TestQuestionOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestQuestionOptionResponses_TestQuestionResponses_QuestionResponseId",
                        column: x => x.QuestionResponseId,
                        principalTable: "TestQuestionResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create TestTemplateCategories table (many-to-many)
            migrationBuilder.CreateTable(
                name: "TestTemplateCategories",
                columns: table => new
                {
                    TestTemplateId = table.Column<Guid>(nullable: false),
                    TestCategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTemplateCategories", x => new { x.TestTemplateId, x.TestCategoryId });
                    table.ForeignKey(
                        name: "FK_TestTemplateCategories_TestCategories_TestCategoryId",
                        column: x => x.TestCategoryId,
                        principalTable: "TestCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestTemplateCategories_TestTemplates_TestTemplateId",
                        column: x => x.TestTemplateId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create TestTemplateTags table (many-to-many)
            migrationBuilder.CreateTable(
                name: "TestTemplateTags",
                columns: table => new
                {
                    TestTemplateId = table.Column<Guid>(nullable: false),
                    TestTagId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTemplateTags", x => new { x.TestTemplateId, x.TestTagId });
                    table.ForeignKey(
                        name: "FK_TestTemplateTags_TestTags_TestTagId",
                        column: x => x.TestTagId,
                        principalTable: "TestTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestTemplateTags_TestTemplates_TestTemplateId",
                        column: x => x.TestTemplateId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create indexes
            migrationBuilder.CreateIndex(
                name: "IX_TestAttempts_TestTemplateId",
                table: "TestAttempts",
                column: "TestTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAttempts_UserId",
                table: "TestAttempts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCategories_Name",
                table: "TestCategories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_TestCategories_ParentCategoryId",
                table: "TestCategories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestionOptionResponses_OptionId",
                table: "TestQuestionOptionResponses",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestionOptionResponses_QuestionResponseId",
                table: "TestQuestionOptionResponses",
                column: "QuestionResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestionOptions_QuestionId",
                table: "TestQuestionOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestionResponses_QuestionId",
                table: "TestQuestionResponses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestionResponses_TestAttemptId",
                table: "TestQuestionResponses",
                column: "TestAttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestions_TestTemplateId",
                table: "TestQuestions",
                column: "TestTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTags_Name",
                table: "TestTags",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplateCategories_TestCategoryId",
                table: "TestTemplateCategories",
                column: "TestCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplateTags_TestTagId",
                table: "TestTemplateTags",
                column: "TestTagId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplates_Title",
                table: "TestTemplates",
                column: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestQuestionOptionResponses");

            migrationBuilder.DropTable(
                name: "TestTemplateCategories");

            migrationBuilder.DropTable(
                name: "TestTemplateTags");

            migrationBuilder.DropTable(
                name: "TestQuestionOptions");

            migrationBuilder.DropTable(
                name: "TestQuestionResponses");

            migrationBuilder.DropTable(
                name: "TestCategories");

            migrationBuilder.DropTable(
                name: "TestTags");

            migrationBuilder.DropTable(
                name: "TestAttempts");

            migrationBuilder.DropTable(
                name: "TestQuestions");

            migrationBuilder.DropTable(
                name: "TestTemplates");
        }
    }
}