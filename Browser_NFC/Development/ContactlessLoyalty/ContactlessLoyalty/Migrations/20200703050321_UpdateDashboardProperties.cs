using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ContactlessLoyalty.Migrations
{
    public partial class UpdateDashboardProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "LoyaltyCards");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "LoyaltyCards");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "LoyaltyCards");

            migrationBuilder.DropColumn(
                name: "MobilePhoneNumber",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AccountContactlessLoyaltyUserId",
                table: "LoyaltyCards",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastStampDateTime",
                table: "LoyaltyCards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfStamps",
                table: "LoyaltyCards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NumberOfVouchers",
                table: "LoyaltyCards",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "LastStampDateTime",
                table: "LoyaltyCards");

            migrationBuilder.DropColumn(
                name: "NumberOfStamps",
                table: "LoyaltyCards");

            migrationBuilder.DropColumn(
                name: "NumberOfVouchers",
                table: "LoyaltyCards");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "LoyaltyCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "LoyaltyCards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "LoyaltyCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobilePhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                nullable: true);
        }
    }
}
