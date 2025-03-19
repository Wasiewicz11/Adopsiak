using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adopsiak.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Breed_BreedId",
                table: "Animal");

            migrationBuilder.DropTable(
                name: "Breed");

            migrationBuilder.DropIndex(
                name: "IX_Animal_BreedId",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "BreedId",
                table: "Animal");

            migrationBuilder.AddColumn<string>(
                name: "Breed",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Breed",
                table: "Animal");

            migrationBuilder.AddColumn<int>(
                name: "BreedId",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Breed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breed", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_BreedId",
                table: "Animal",
                column: "BreedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Breed_BreedId",
                table: "Animal",
                column: "BreedId",
                principalTable: "Breed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
