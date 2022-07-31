using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonkurCRM.DataLayer.Migrations
{
    public partial class mig_RemoveSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Salaries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    SalaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdviserId = table.Column<int>(type: "int", nullable: false),
                    SalaryTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaySalaryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "IX_Salaries_AdviserId",
                table: "Salaries",
                column: "AdviserId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_SalaryTypeId",
                table: "Salaries",
                column: "SalaryTypeId");
        }
    }
}
