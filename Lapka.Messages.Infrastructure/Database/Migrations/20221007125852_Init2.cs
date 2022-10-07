using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapka.Messages.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderId",
                schema: "messages",
                table: "Messages",
                newName: "ReceiverId");

            migrationBuilder.AddColumn<int>(
                name: "OnlineCount",
                schema: "messages",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsUnread",
                schema: "messages",
                table: "Messages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                schema: "messages",
                table: "AppUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnlineCount",
                schema: "messages",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "IsUnread",
                schema: "messages",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                schema: "messages",
                table: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                schema: "messages",
                table: "Messages",
                newName: "SenderId");
        }
    }
}
