using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableGenius.Api.Repo.Database.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUselessProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "LocationAssignments");

            migrationBuilder.DropColumn(
                name: "IsSuperAdmin",
                table: "LocationAssignments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "LocationAssignments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuperAdmin",
                table: "LocationAssignments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
