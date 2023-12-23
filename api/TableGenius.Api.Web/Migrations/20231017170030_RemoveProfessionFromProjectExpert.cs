using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableGenius.Api.Web.Migrations
{
    public partial class RemoveProfessionFromProjectExpert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profession",
                table: "ProjectExpert");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "ProjectRating",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "ProjectRating");

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "ProjectExpert",
                type: "text",
                nullable: true);
        }
    }
}
