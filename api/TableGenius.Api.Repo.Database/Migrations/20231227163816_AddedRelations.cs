using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableGenius.Api.Repo.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationAssignments_Reservations_ReservationId",
                table: "ReservationAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ReservationAssignments_ReservationId",
                table: "ReservationAssignments");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationAssignments_ReservationId",
                table: "ReservationAssignments",
                column: "ReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationAssignments_Reservations_ReservationId",
                table: "ReservationAssignments",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationAssignments_Reservations_ReservationId",
                table: "ReservationAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ReservationAssignments_ReservationId",
                table: "ReservationAssignments");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationAssignments_ReservationId",
                table: "ReservationAssignments",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationAssignments_Reservations_ReservationId",
                table: "ReservationAssignments",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
