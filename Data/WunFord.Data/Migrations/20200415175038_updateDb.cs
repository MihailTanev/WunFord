using Microsoft.EntityFrameworkCore.Migrations;

namespace WunFord.Data.Migrations
{
    public partial class updateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecondCheck",
                table: "Tickets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondCheck",
                table: "Tickets");
        }
    }
}
