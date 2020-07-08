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
                table: "Dashboard");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Dashboard");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Dashboard");

            migrationBuilder.DropColumn(
                name: "MobilePhoneNumber",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AccountContactlessLoyaltyUserId",
                table: "Dashboard",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastStampDateTime",
                table: "Dashboard",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfStamps",
                table: "Dashboard",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NumberOfVouchers",
                table: "Dashboard",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "LastStampDateTime",
                table: "Dashboard");

            migrationBuilder.DropColumn(
                name: "NumberOfStamps",
                table: "Dashboard");

            migrationBuilder.DropColumn(
                name: "NumberOfVouchers",
                table: "Dashboard");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Dashboard",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Dashboard",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Dashboard",
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
