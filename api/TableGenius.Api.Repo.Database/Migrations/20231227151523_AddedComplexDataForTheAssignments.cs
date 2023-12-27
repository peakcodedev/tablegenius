using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableGenius.Api.Repo.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedComplexDataForTheAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uuid", nullable: false),
                    AreaSlotId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    ModDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationAssignments_AreaSlots_AreaSlotId",
                        column: x => x.AreaSlotId,
                        principalTable: "AreaSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationAssignments_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TableReservationAssignments",
                columns: table => new
                {
                    ReservationAssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TableId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableReservationAssignments", x => new { x.TableId, x.ReservationAssignmentId, x.TenantId });
                    table.ForeignKey(
                        name: "FK_TableReservationAssignments_ReservationAssignments_Reservat~",
                        column: x => x.ReservationAssignmentId,
                        principalTable: "ReservationAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TableReservationAssignments_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationAssignments_AreaSlotId",
                table: "ReservationAssignments",
                column: "AreaSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationAssignments_ReservationId",
                table: "ReservationAssignments",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_TableReservationAssignments_ReservationAssignmentId",
                table: "TableReservationAssignments",
                column: "ReservationAssignmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableReservationAssignments");

            migrationBuilder.DropTable(
                name: "ReservationAssignments");
        }
    }
}
