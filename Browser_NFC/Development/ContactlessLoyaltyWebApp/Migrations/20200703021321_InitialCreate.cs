using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactlessLoyaltyWebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    MobilePhone = table.Column<string>(maxLength: 15, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LoyaltyCards",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastStampReceived = table.Column<DateTime>(maxLength: 50, nullable: false),
                    VoucherReceived = table.Column<int>(maxLength: 10, nullable: false),
                    StampsCollected = table.Column<int>(maxLength: 10, nullable: false),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyCards", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LoyaltyCards_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyCards_UserID",
                table: "LoyaltyCards",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CardID",
                table: "Users",
                column: "CardID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LoyaltyCards_CardID",
                table: "Users",
                column: "CardID",
                principalTable: "LoyaltyCards",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyCards_Users_UserID",
                table: "LoyaltyCards");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LoyaltyCards");
        }
    }
}
