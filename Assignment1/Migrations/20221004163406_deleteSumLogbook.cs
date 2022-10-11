using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuckOf.Migrations
{
    public partial class deleteSumLogbook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SumLogbook",
                table: "Drives");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SumLogbook",
                table: "Drives",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
