using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactlessLoyalty.Migrations
{
    public partial class UpdateDashboardPropertiesWithRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboard_AspNetUsers_AccountContactlessLoyaltyUserId",
                table: "LoyaltyCards");

            migrationBuilder.DropIndex(
                name: "IX_Dashboard_AccountContactlessLoyaltyUserId",
                table: "LoyaltyCards");

            migrationBuilder.DropColumn(
                name: "AccountContactlessLoyaltyUserId",
                table: "LoyaltyCards");

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "LoyaltyCards",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboard_AspNetUsers_UserIdId",
                table: "LoyaltyCards");

            migrationBuilder.DropIndex(
                name: "IX_Dashboard_UserIdId",
                table: "LoyaltyCards");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "LoyaltyCards");

            migrationBuilder.AddColumn<string>(
                name: "AccountContactlessLoyaltyUserId",
                table: "LoyaltyCards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dashboard_AccountContactlessLoyaltyUserId",
                table: "LoyaltyCards",
                column: "AccountContactlessLoyaltyUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboard_AspNetUsers_AccountContactlessLoyaltyUserId",
                table: "LoyaltyCards",
                column: "AccountContactlessLoyaltyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
