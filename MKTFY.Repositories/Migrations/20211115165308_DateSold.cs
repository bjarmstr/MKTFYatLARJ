using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MKTFY.Repositories.Migrations
{
    public partial class DateSold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateSold",
                table: "Listings",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListingUploads_UploadId",
                table: "ListingUploads",
                column: "UploadId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListingUploads_Uploads_UploadId",
                table: "ListingUploads",
                column: "UploadId",
                principalTable: "Uploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListingUploads_Uploads_UploadId",
                table: "ListingUploads");

            migrationBuilder.DropIndex(
                name: "IX_ListingUploads_UploadId",
                table: "ListingUploads");

            migrationBuilder.DropColumn(
                name: "DateSold",
                table: "Listings");
        }
    }
}
