using Microsoft.EntityFrameworkCore.Migrations;

namespace MKTFY.Repositories.Migrations
{
    public partial class CategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Listings",
                newName: "CategoryName");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionStatus",
                table: "Listings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                column: "Name",
                values: new object[]
                {
                    "Electronics",
                    "RealEstate"
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CategoryName",
                table: "Listings",
                column: "CategoryName");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Categories_CategoryName",
                table: "Listings",
                column: "CategoryName",
                principalTable: "Categories",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Categories_CategoryName",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Listings_CategoryName",
                table: "Listings");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Listings",
                newName: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionStatus",
                table: "Listings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
