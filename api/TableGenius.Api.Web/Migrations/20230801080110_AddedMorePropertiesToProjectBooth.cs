using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableGenius.Api.Web.Migrations
{
    public partial class AddedMorePropertiesToProjectBooth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GridsLarge",
                table: "ProjectBooth");

            migrationBuilder.DropColumn(
                name: "GridsMedium",
                table: "ProjectBooth");

            migrationBuilder.RenameColumn(
                name: "GridsSmall",
                table: "ProjectBooth",
                newName: "Type");

            migrationBuilder.AddColumn<bool>(
                name: "Electrcity230",
                table: "ProjectBooth",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Electrcity400",
                table: "ProjectBooth",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProjectBoothCustomLayout",
                table: "ProjectBooth",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Electrcity230",
                table: "ProjectBooth");

            migrationBuilder.DropColumn(
                name: "Electrcity400",
                table: "ProjectBooth");

            migrationBuilder.DropColumn(
                name: "ProjectBoothCustomLayout",
                table: "ProjectBooth");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ProjectBooth",
                newName: "GridsSmall");

            migrationBuilder.AddColumn<int>(
                name: "GridsLarge",
                table: "ProjectBooth",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GridsMedium",
                table: "ProjectBooth",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
