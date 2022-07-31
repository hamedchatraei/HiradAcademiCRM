using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonkurCRM.DataLayer.Migrations
{
    public partial class mig_AddAdviserIdToFollowUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdviserId",
                table: "FollowUps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdviserId",
                table: "FollowUps");
        }
    }
}
