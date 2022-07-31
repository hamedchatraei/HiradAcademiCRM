using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonkurCRM.DataLayer.Migrations
{
    public partial class mig_AddIsCancelToRegisterEntityAndRemoveIsCanceleFromStudentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancel",
                table: "Students");

            migrationBuilder.AddColumn<bool>(
                name: "IsCancel",
                table: "Registers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancel",
                table: "Registers");

            migrationBuilder.AddColumn<bool>(
                name: "IsCancel",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
