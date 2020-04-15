using Microsoft.EntityFrameworkCore.Migrations;

namespace WunFord.Data.Migrations
{
    public partial class FirstCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckInOne",
                table: "Tickets",
                newName: "FirstCheck");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstCheck",
                table: "Tickets",
                newName: "CheckInOne");
        }
    }
}
