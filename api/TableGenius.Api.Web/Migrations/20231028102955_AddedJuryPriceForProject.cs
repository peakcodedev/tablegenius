using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableGenius.Api.Web.Migrations
{
    public partial class AddedJuryPriceForProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PriceJury",
                table: "Project",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceJury",
                table: "Project");
        }
    }
}
