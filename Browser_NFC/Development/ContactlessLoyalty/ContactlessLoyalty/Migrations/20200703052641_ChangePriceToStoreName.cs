using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactlessLoyalty.Migrations
{
    public partial class ChangePriceToStoreName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboard_AspNetUsers_UserIdId",
                table: "Dashboard");

            migrationBuilder.DropIndex(
                name: "IX_Dashboard_UserIdId",
                table: "Dashboard");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Dashboard");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "Dashboard");

            migrationBuilder.AddColumn<string>(
                name: "StoreName",
                table: "Dashboard",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Dashboard",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dashboard_UserId",
                table: "Dashboard",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboard_AspNetUsers_UserId",
                table: "Dashboard",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboard_AspNetUsers_UserId",
                table: "Dashboard");

            migrationBuilder.DropIndex(
                name: "IX_Dashboard_UserId",
                table: "Dashboard");

            migrationBuilder.DropColumn(
                name: "StoreName",
                table: "Dashboard");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Dashboard");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Dashboard",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "Dashboard",
                type: "nvarchar(450)",
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
    }
}
