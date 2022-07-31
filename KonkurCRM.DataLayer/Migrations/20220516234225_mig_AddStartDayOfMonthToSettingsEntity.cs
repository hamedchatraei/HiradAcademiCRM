using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonkurCRM.DataLayer.Migrations
{
    public partial class mig_AddStartDayOfMonthToSettingsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StartDayOfMonths",
                columns: table => new
                {
                    SdmId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SdmValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartDayOfMonths", x => x.SdmId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StartDayOfMonths");
        }
    }
}
