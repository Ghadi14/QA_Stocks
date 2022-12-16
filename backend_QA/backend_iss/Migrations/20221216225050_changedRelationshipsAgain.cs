using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_iss.Migrations
{
    public partial class changedRelationshipsAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "StockUser",
                columns: table => new
                {
                    MemberOfStocksId = table.Column<int>(type: "int", nullable: false),
                    MembersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockUser", x => new { x.MemberOfStocksId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_StockUser_Stocks_MemberOfStocksId",
                        column: x => x.MemberOfStocksId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StockUser_Users_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockUser_MembersId",
                table: "StockUser",
                column: "MembersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockUser");

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
    }
}
