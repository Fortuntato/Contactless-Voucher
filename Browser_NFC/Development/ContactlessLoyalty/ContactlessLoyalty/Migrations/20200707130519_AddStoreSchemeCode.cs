using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactlessLoyalty.Migrations
{
    public partial class AddStoreSchemeCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StoreName",
                table: "LoyaltyCards",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreSchemeCode",
                table: "LoyaltyCards",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreSchemeCode",
                table: "LoyaltyCards");

            migrationBuilder.AlterColumn<string>(
                name: "StoreName",
                table: "LoyaltyCards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
