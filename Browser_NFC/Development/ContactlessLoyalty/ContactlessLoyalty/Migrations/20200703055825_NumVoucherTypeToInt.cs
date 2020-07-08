using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactlessLoyalty.Migrations
{
    public partial class NumVoucherTypeToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NumberOfVouchers",
                table: "LoyaltyCards",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NumberOfVouchers",
                table: "LoyaltyCards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
