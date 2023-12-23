using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableGenius.Api.Web.Migrations
{
    public partial class OneToManyForProjectCollaborators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectCollaboration_ProjectId",
                table: "ProjectCollaboration");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCollaboration_ProjectId",
                table: "ProjectCollaboration",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectCollaboration_ProjectId",
                table: "ProjectCollaboration");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCollaboration_ProjectId",
                table: "ProjectCollaboration",
                column: "ProjectId",
                unique: true);
        }
    }
}
