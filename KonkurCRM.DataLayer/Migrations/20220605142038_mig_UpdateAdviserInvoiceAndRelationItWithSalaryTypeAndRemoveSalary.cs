using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonkurCRM.DataLayer.Migrations
{
    public partial class mig_UpdateAdviserInvoiceAndRelationItWithSalaryTypeAndRemoveSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "AdviserInvoices",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaySalaryDate",
                table: "AdviserInvoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SalaryTypeId",
                table: "AdviserInvoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdviserInvoices_SalaryTypeId",
                table: "AdviserInvoices",
                column: "SalaryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdviserInvoices_SalaryTypes_SalaryTypeId",
                table: "AdviserInvoices",
                column: "SalaryTypeId",
                principalTable: "SalaryTypes",
                principalColumn: "SalaryTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdviserInvoices_SalaryTypes_SalaryTypeId",
                table: "AdviserInvoices");

            migrationBuilder.DropIndex(
                name: "IX_AdviserInvoices_SalaryTypeId",
                table: "AdviserInvoices");

            migrationBuilder.DropColumn(
                name: "PaySalaryDate",
                table: "AdviserInvoices");

            migrationBuilder.DropColumn(
                name: "SalaryTypeId",
                table: "AdviserInvoices");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "AdviserInvoices",
                newName: "Title");
        }
    }
}
