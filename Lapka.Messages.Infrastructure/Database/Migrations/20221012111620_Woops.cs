using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapka.Messages.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Woops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AppUsers_SenderId",
                schema: "messages",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SenderId",
                schema: "messages",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderName",
                schema: "messages",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                schema: "messages",
                table: "Messages",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                schema: "messages",
                table: "Messages",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "messages",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoom",
                schema: "messages",
                columns: table => new
                {
                    AppUsersUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomsRoomId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoom", x => new { x.AppUsersUserId, x.RoomsRoomId });
                    table.ForeignKey(
                        name: "FK_AppUserRoom_AppUsers_AppUsersUserId",
                        column: x => x.AppUsersUserId,
                        principalSchema: "messages",
                        principalTable: "AppUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserRoom_Rooms_RoomsRoomId",
                        column: x => x.RoomsRoomId,
                        principalSchema: "messages",
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RoomId",
                schema: "messages",
                table: "Messages",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoom_RoomsRoomId",
                schema: "messages",
                table: "AppUserRoom",
                column: "RoomsRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Rooms_RoomId",
                schema: "messages",
                table: "Messages",
                column: "RoomId",
                principalSchema: "messages",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Rooms_RoomId",
                schema: "messages",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "AppUserRoom",
                schema: "messages");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RoomId",
                schema: "messages",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                schema: "messages",
                table: "Messages",
                newName: "ReceiverId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "messages",
                table: "Messages",
                newName: "MessageId");

            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                schema: "messages",
                table: "Messages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                schema: "messages",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AppUsers_SenderId",
                schema: "messages",
                table: "Messages",
                column: "SenderId",
                principalSchema: "messages",
                principalTable: "AppUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
