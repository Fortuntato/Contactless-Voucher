using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactlessLoyalty.Migrations
{
    public partial class UpdateDashboardPropertiesWithRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboard_AspNetUsers_AccountContactlessLoyaltyUserId",
                table: "Dashboard");

            migrationBuilder.DropIndex(
                name: "IX_Dashboard_AccountContactlessLoyaltyUserId",
                table: "Dashboard");

            migrationBuilder.DropColumn(
                name: "AccountContactlessLoyaltyUserId",
                table: "Dashboard");

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "Dashboard",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dashboard_UserIdId",
                table: "Dashboard",
                column: "UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboard_AspNetUsers_UserIdId",
                table: "Dashboard",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboard_AspNetUsers_UserIdId",
                table: "Dashboard");

            migrationBuilder.DropIndex(
                name: "IX_Dashboard_UserIdId",
                table: "Dashboard");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "Dashboard");

            migrationBuilder.AddColumn<string>(
                name: "AccountContactlessLoyaltyUserId",
                table: "Dashboard",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dashboard_AccountContactlessLoyaltyUserId",
                table: "Dashboard",
                column: "AccountContactlessLoyaltyUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboard_AspNetUsers_AccountContactlessLoyaltyUserId",
                table: "Dashboard",
                column: "AccountContactlessLoyaltyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
