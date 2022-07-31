using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonkurCRM.DataLayer.Migrations
{
    public partial class mig_AddUnregisteeredCallsAndUnregisteredFollowUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnregisteredCalls",
                columns: table => new
                {
                    UnregisteredCallsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdviserId = table.Column<int>(type: "int", nullable: false),
                    CallDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CallTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsCall = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnregisteredCalls", x => x.UnregisteredCallsId);
                    table.ForeignKey(
                        name: "FK_UnregisteredCalls_Advisers_AdviserId",
                        column: x => x.AdviserId,
                        principalTable: "Advisers",
                        principalColumn: "AdviserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnregisteredFollowUps",
                columns: table => new
                {
                    UnregisteredFollowUpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdviserId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    FollowUpDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsFollowedUp = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnregisteredFollowUps", x => x.UnregisteredFollowUpId);
                    table.ForeignKey(
                        name: "FK_UnregisteredFollowUps_Advisers_AdviserId",
                        column: x => x.AdviserId,
                        principalTable: "Advisers",
                        principalColumn: "AdviserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnregisteredFollowUps_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnregisteredCalls_AdviserId",
                table: "UnregisteredCalls",
                column: "AdviserId");

            migrationBuilder.CreateIndex(
                name: "IX_UnregisteredFollowUps_AdviserId",
                table: "UnregisteredFollowUps",
                column: "AdviserId");

            migrationBuilder.CreateIndex(
                name: "IX_UnregisteredFollowUps_StudentId",
                table: "UnregisteredFollowUps",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnregisteredCalls");

            migrationBuilder.DropTable(
                name: "UnregisteredFollowUps");
        }
    }
}
