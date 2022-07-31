using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonkurCRM.DataLayer.Migrations
{
    public partial class mig_AddNotCalledEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotCalleds",
                columns: table => new
                {
                    NotCalledId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdviserId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotCalleds", x => x.NotCalledId);
                    table.ForeignKey(
                        name: "FK_NotCalleds_Advisers_AdviserId",
                        column: x => x.AdviserId,
                        principalTable: "Advisers",
                        principalColumn: "AdviserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotCalleds_AdviserId",
                table: "NotCalleds",
                column: "AdviserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotCalleds");
        }
    }
}
