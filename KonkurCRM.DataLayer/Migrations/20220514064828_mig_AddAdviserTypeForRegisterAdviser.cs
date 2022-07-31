using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonkurCRM.DataLayer.Migrations
{
    public partial class mig_AddAdviserTypeForRegisterAdviser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdviserTypeId",
                table: "RegisterSAdvisers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AdviserType",
                columns: table => new
                {
                    AdviserTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdviserType", x => x.AdviserTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegisterSAdvisers_AdviserTypeId",
                table: "RegisterSAdvisers",
                column: "AdviserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterSAdvisers_AdviserType_AdviserTypeId",
                table: "RegisterSAdvisers",
                column: "AdviserTypeId",
                principalTable: "AdviserType",
                principalColumn: "AdviserTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterSAdvisers_AdviserType_AdviserTypeId",
                table: "RegisterSAdvisers");

            migrationBuilder.DropTable(
                name: "AdviserType");

            migrationBuilder.DropIndex(
                name: "IX_RegisterSAdvisers_AdviserTypeId",
                table: "RegisterSAdvisers");

            migrationBuilder.DropColumn(
                name: "AdviserTypeId",
                table: "RegisterSAdvisers");
        }
    }
}
