using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenNote.StudentInfo.Migrations
{
    /// <inheritdoc />
    public partial class addClassInfoDatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewRatio = table.Column<int>(type: "int", nullable: false),
                    ClassContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassInfos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassInfos");
        }
    }
}
