using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookAnnotationApp.Migrations
{
    /// <inheritdoc />
    public partial class renameBooktoVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Annotations_Books_BookId",
                table: "Annotations");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Books_BookId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Notes_BookId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Annotations_BookId",
                table: "Annotations");

            migrationBuilder.AddColumn<int>(
                name: "BookVersionId",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookVersionId",
                table: "Annotations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Versions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Language = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Versions_BookDetails_BookDetailsId",
                        column: x => x.BookDetailsId,
                        principalTable: "BookDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Versions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_BookVersionId",
                table: "Notes",
                column: "BookVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Annotations_BookVersionId",
                table: "Annotations",
                column: "BookVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Versions_BookDetailsId",
                table: "Versions",
                column: "BookDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Versions_UserId",
                table: "Versions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Annotations_Versions_BookVersionId",
                table: "Annotations",
                column: "BookVersionId",
                principalTable: "Versions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Versions_BookVersionId",
                table: "Notes",
                column: "BookVersionId",
                principalTable: "Versions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Annotations_Versions_BookVersionId",
                table: "Annotations");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Versions_BookVersionId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "Versions");

            migrationBuilder.DropIndex(
                name: "IX_Notes_BookVersionId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Annotations_BookVersionId",
                table: "Annotations");

            migrationBuilder.DropColumn(
                name: "BookVersionId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "BookVersionId",
                table: "Annotations");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BookDetailsId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FileType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Language = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_BookDetails_BookDetailsId",
                        column: x => x.BookDetailsId,
                        principalTable: "BookDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_BookId",
                table: "Notes",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Annotations_BookId",
                table: "Annotations",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookDetailsId",
                table: "Books",
                column: "BookDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserId",
                table: "Books",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Annotations_Books_BookId",
                table: "Annotations",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Books_BookId",
                table: "Notes",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
