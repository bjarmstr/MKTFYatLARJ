using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MKTFY.Repositories.Migrations
{
    public partial class NotificationContainsMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Messages_MessageId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_MessageId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Notifications",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Notifications");

            migrationBuilder.AddColumn<Guid>(
                name: "MessageId",
                table: "Notifications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_MessageId",
                table: "Notifications",
                column: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Messages_MessageId",
                table: "Notifications",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
