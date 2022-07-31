﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonkurCRM.DataLayer.Migrations
{
    public partial class mig_AddUserIdToAdviser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Advisers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Advisers");
        }
    }
}
