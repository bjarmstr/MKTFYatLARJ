using Microsoft.EntityFrameworkCore.Migrations;

namespace MKTFY.Repositories.Migrations
{
    public partial class RegionInListing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Listings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "Listings");
        }
    }
}
