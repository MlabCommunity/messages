using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapka.Messages.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorkers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "messages",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "messages",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "messages",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                schema: "messages",
                table: "AppUsers");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                schema: "messages",
                table: "AppUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Workers",
                schema: "messages",
                columns: table => new
                {
                    WorkerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShelterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.WorkerId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Workers",
                schema: "messages");

            migrationBuilder.DropColumn(
                name: "Role",
                schema: "messages",
                table: "AppUsers");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "messages",
                table: "AppUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "messages",
                table: "AppUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "messages",
                table: "AppUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoto",
                schema: "messages",
                table: "AppUsers",
                type: "text",
                nullable: true);
        }
    }
}
