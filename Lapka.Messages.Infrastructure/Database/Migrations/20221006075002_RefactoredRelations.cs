using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lapka.Messages.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AppUsers_AppUserUserId",
                schema: "messages",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_AppUserUserId",
                schema: "messages",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "AppUserUserId",
                schema: "messages",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                schema: "messages",
                table: "Messages",
                type: "text",
                nullable: false,
                defaultValue: "");

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
                name: "IX_AppUserRoom_RoomsRoomId",
                schema: "messages",
                table: "AppUserRoom",
                column: "RoomsRoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserRoom",
                schema: "messages");

            migrationBuilder.DropColumn(
                name: "SenderName",
                schema: "messages",
                table: "Messages");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserUserId",
                schema: "messages",
                table: "Rooms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AppUserUserId",
                schema: "messages",
                table: "Rooms",
                column: "AppUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AppUsers_AppUserUserId",
                schema: "messages",
                table: "Rooms",
                column: "AppUserUserId",
                principalSchema: "messages",
                principalTable: "AppUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
