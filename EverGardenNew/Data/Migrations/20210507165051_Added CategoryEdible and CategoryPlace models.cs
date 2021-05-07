using Microsoft.EntityFrameworkCore.Migrations;

namespace EverGardenNew.Data.Migrations
{
    public partial class AddedCategoryEdibleandCategoryPlacemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryEdible",
                table: "Plant");

            migrationBuilder.DropColumn(
                name: "CategoryPlace",
                table: "Plant");

            migrationBuilder.AddColumn<int>(
                name: "CategoryEdibleID",
                table: "Plant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryPlaceID",
                table: "Plant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryEdible",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEdible", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryPlace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPlace", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plant_CategoryEdibleID",
                table: "Plant",
                column: "CategoryEdibleID");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_CategoryPlaceID",
                table: "Plant",
                column: "CategoryPlaceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Plant_CategoryEdible_CategoryEdibleID",
                table: "Plant",
                column: "CategoryEdibleID",
                principalTable: "CategoryEdible",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plant_CategoryPlace_CategoryPlaceID",
                table: "Plant",
                column: "CategoryPlaceID",
                principalTable: "CategoryPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plant_CategoryEdible_CategoryEdibleID",
                table: "Plant");

            migrationBuilder.DropForeignKey(
                name: "FK_Plant_CategoryPlace_CategoryPlaceID",
                table: "Plant");

            migrationBuilder.DropTable(
                name: "CategoryEdible");

            migrationBuilder.DropTable(
                name: "CategoryPlace");

            migrationBuilder.DropIndex(
                name: "IX_Plant_CategoryEdibleID",
                table: "Plant");

            migrationBuilder.DropIndex(
                name: "IX_Plant_CategoryPlaceID",
                table: "Plant");

            migrationBuilder.DropColumn(
                name: "CategoryEdibleID",
                table: "Plant");

            migrationBuilder.DropColumn(
                name: "CategoryPlaceID",
                table: "Plant");

            migrationBuilder.AddColumn<string>(
                name: "CategoryEdible",
                table: "Plant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryPlace",
                table: "Plant",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
