using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonkurCRM.DataLayer.Migrations
{
    public partial class mig_UpdateUnregisteredCalls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "UnregisteredCalls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UnregisteredCalls_StudentId",
                table: "UnregisteredCalls",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnregisteredCalls_Students_StudentId",
                table: "UnregisteredCalls",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnregisteredCalls_Students_StudentId",
                table: "UnregisteredCalls");

            migrationBuilder.DropIndex(
                name: "IX_UnregisteredCalls_StudentId",
                table: "UnregisteredCalls");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "UnregisteredCalls");
        }
    }
}
