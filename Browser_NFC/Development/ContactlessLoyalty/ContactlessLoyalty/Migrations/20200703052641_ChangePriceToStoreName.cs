using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactlessLoyalty.Migrations
{
    public partial class ChangePriceToStoreName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboard_AspNetUsers_UserIdId",
                table: "LoyaltyCards");

            migrationBuilder.DropIndex(
                name: "IX_Dashboard_UserIdId",
                table: "LoyaltyCards");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "LoyaltyCards");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "LoyaltyCards");

            migrationBuilder.AddColumn<string>(
                name: "StoreName",
                table: "LoyaltyCards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LoyaltyCards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dashboard_UserId",
                table: "LoyaltyCards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboard_AspNetUsers_UserId",
                table: "LoyaltyCards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboard_AspNetUsers_UserId",
                table: "LoyaltyCards");

            migrationBuilder.DropIndex(
                name: "IX_Dashboard_UserId",
                table: "LoyaltyCards");

            migrationBuilder.DropColumn(
                name: "StoreName",
                table: "LoyaltyCards");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LoyaltyCards");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "LoyaltyCards",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "LoyaltyCards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dashboard_UserIdId",
                table: "LoyaltyCards",
                column: "UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboard_AspNetUsers_UserIdId",
                table: "LoyaltyCards",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
