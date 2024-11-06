using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class ingredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAndIngridient_ingridient_ProductIngredientsIngredient~",
                table: "ProductAndIngridient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ingridient",
                table: "ingridient");

            migrationBuilder.RenameTable(
                name: "ingridient",
                newName: "ingredient");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPercentage",
                table: "promotions",
                type: "decimal(5)",
                precision: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,30)",
                oldPrecision: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "products",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingredient",
                table: "ingredient",
                column: "IngredientID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAndIngridient_ingredient_ProductIngredientsIngredient~",
                table: "ProductAndIngridient",
                column: "ProductIngredientsIngredientID",
                principalTable: "ingredient",
                principalColumn: "IngredientID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAndIngridient_ingredient_ProductIngredientsIngredient~",
                table: "ProductAndIngridient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ingredient",
                table: "ingredient");

            migrationBuilder.RenameTable(
                name: "ingredient",
                newName: "ingridient");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPercentage",
                table: "promotions",
                type: "decimal(5,30)",
                precision: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5)",
                oldPrecision: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "products",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingridient",
                table: "ingridient",
                column: "IngredientID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAndIngridient_ingridient_ProductIngredientsIngredient~",
                table: "ProductAndIngridient",
                column: "ProductIngredientsIngredientID",
                principalTable: "ingridient",
                principalColumn: "IngredientID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
