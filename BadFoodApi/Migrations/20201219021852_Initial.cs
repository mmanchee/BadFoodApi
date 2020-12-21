using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BadFoodApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    FoodId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    SubCat = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FDCId = table.Column<int>(nullable: false),
                    Caffeine = table.Column<int>(nullable: false),
                    Egg = table.Column<int>(nullable: false),
                    Fish = table.Column<int>(nullable: false),
                    FODMAP = table.Column<int>(nullable: false),
                    Fructose = table.Column<int>(nullable: false),
                    Gluten = table.Column<int>(nullable: false),
                    Histamine = table.Column<int>(nullable: false),
                    Lactose = table.Column<int>(nullable: false),
                    Lectin = table.Column<int>(nullable: false),
                    Legume = table.Column<int>(nullable: false),
                    Nut = table.Column<int>(nullable: false),
                    Oxalte = table.Column<int>(nullable: false),
                    Salicylates = table.Column<int>(nullable: false),
                    Shellfish = table.Column<int>(nullable: false),
                    Soy = table.Column<int>(nullable: false),
                    Sulfites = table.Column<int>(nullable: false),
                    Tryamine = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.FoodId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foods");
        }
    }
}
