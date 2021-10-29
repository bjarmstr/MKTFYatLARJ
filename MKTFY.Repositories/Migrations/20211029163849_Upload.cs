using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MKTFY.Repositories.Migrations
{
    public partial class Upload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListingUploads",
                columns: table => new
                {
                    ListingId = table.Column<Guid>(type: "uuid", nullable: false),
                    UploadId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingUploads", x => new { x.ListingId, x.UploadId });
                    table.ForeignKey(
                        name: "FK_ListingUploads_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingUploads");
        }
    }
}
