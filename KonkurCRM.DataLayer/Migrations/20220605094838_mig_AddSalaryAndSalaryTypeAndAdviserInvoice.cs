using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonkurCRM.DataLayer.Migrations
{
    public partial class mig_AddSalaryAndSalaryTypeAndAdviserInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdviserInvoices",
                columns: table => new
                {
                    AdviserInvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdviserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdviserInvoices", x => x.AdviserInvoiceId);
                    table.ForeignKey(
                        name: "FK_AdviserInvoices_Advisers_AdviserId",
                        column: x => x.AdviserId,
                        principalTable: "Advisers",
                        principalColumn: "AdviserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalaryTypes",
                columns: table => new
                {
                    SalaryTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryTypes", x => x.SalaryTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    SalaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaryTypeId = table.Column<int>(type: "int", nullable: false),
                    AdviserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    PaySalaryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.SalaryId);
                    table.ForeignKey(
                        name: "FK_Salaries_Advisers_AdviserId",
                        column: x => x.AdviserId,
                        principalTable: "Advisers",
                        principalColumn: "AdviserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Salaries_SalaryTypes_SalaryTypeId",
                        column: x => x.SalaryTypeId,
                        principalTable: "SalaryTypes",
                        principalColumn: "SalaryTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdviserInvoices_AdviserId",
                table: "AdviserInvoices",
                column: "AdviserId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_AdviserId",
                table: "Salaries",
                column: "AdviserId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_SalaryTypeId",
                table: "Salaries",
                column: "SalaryTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdviserInvoices");

            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.DropTable(
                name: "SalaryTypes");
        }
    }
}
