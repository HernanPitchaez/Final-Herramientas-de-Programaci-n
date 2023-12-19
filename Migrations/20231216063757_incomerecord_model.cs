using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_ClubDeportes.Migrations
{
    /// <inheritdoc />
    public partial class incomerecord_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncomeRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReceiptNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentType = table.Column<int>(type: "INTEGER", nullable: false),
                    MembershipFee = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    PartnerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeRecord_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecordSport",
                columns: table => new
                {
                    IncomeRecordsId = table.Column<int>(type: "INTEGER", nullable: false),
                    SportsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordSport", x => new { x.IncomeRecordsId, x.SportsId });
                    table.ForeignKey(
                        name: "FK_RecordSport_IncomeRecord_IncomeRecordsId",
                        column: x => x.IncomeRecordsId,
                        principalTable: "IncomeRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordSport_Sport_SportsId",
                        column: x => x.SportsId,
                        principalTable: "Sport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IncomeRecord_PartnerId",
                table: "IncomeRecord",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordSport_SportsId",
                table: "RecordSport",
                column: "SportsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordSport");

            migrationBuilder.DropTable(
                name: "IncomeRecord");
        }
    }
}
