using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_iss.Migrations
{
    public partial class changedRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_StockId",
                table: "Users",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Stocks_StockId",
                table: "Users",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Stocks_StockId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_StockId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "Users");
        }
    }
}
