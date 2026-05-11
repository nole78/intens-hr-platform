using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Skills",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Candidates",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Candidates",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ContactNumber",
                table: "Candidates",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "ContactNumber", "DateOfBirth", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "1234567890", new DateOnly(1990, 1, 1), "jd@gmail.com", "John Doe" },
                    { 2, "0987654321", new DateOnly(1992, 2, 2), "js@gmail.com", "John Smith" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "C# programming" },
                    { 2, "Java programming" },
                    { 3, "Database design" },
                    { 4, "English" },
                    { 5, "Russian" },
                    { 6, "German language" }
                });

            migrationBuilder.InsertData(
                table: "CandidateSkills",
                columns: new[] { "CandidateId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 2 },
                    { 2, 5 },
                    { 2, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CandidateSkills",
                keyColumns: new[] { "CandidateId", "SkillId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CandidateSkills",
                keyColumns: new[] { "CandidateId", "SkillId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "CandidateSkills",
                keyColumns: new[] { "CandidateId", "SkillId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "CandidateSkills",
                keyColumns: new[] { "CandidateId", "SkillId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "CandidateSkills",
                keyColumns: new[] { "CandidateId", "SkillId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "CandidateSkills",
                keyColumns: new[] { "CandidateId", "SkillId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Skills",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Candidates",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Candidates",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ContactNumber",
                table: "Candidates",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);
        }
    }
}
