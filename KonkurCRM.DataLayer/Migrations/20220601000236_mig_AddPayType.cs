using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonkurCRM.DataLayer.Migrations
{
    public partial class mig_AddPayType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PayTypeId",
                table: "Registers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PayTypes",
                columns: table => new
                {
                    PayTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayTypeTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayTypes", x => x.PayTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registers_PayTypeId",
                table: "Registers",
                column: "PayTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_PayTypes_PayTypeId",
                table: "Registers",
                column: "PayTypeId",
                principalTable: "PayTypes",
                principalColumn: "PayTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registers_PayTypes_PayTypeId",
                table: "Registers");

            migrationBuilder.DropTable(
                name: "PayTypes");

            migrationBuilder.DropIndex(
                name: "IX_Registers_PayTypeId",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "PayTypeId",
                table: "Registers");
        }
    }
}
