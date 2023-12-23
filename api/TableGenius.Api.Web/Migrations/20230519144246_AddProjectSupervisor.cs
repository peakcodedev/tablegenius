using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableGenius.Api.Web.Migrations
{
    public partial class AddProjectSupervisor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupervisorMail",
                table: "Project",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupervisorName",
                table: "Project",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupervisorPhone",
                table: "Project",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupervisorMail",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "SupervisorName",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "SupervisorPhone",
                table: "Project");
        }
    }
}
