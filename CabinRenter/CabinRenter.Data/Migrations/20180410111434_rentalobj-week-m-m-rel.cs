using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CabinRenter.Data.Migrations
{
    public partial class rentalobjweekmmrel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RentalObjectWeek",
                columns: table => new
                {
                    RentalObjectId = table.Column<int>(nullable: false),
                    WeekId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalObjectWeek", x => new { x.RentalObjectId, x.WeekId });
                    table.ForeignKey(
                        name: "FK_RentalObjectWeek_RentalObjects_RentalObjectId",
                        column: x => x.RentalObjectId,
                        principalTable: "RentalObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalObjectWeek_Weeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Weeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentalObjectWeek_WeekId",
                table: "RentalObjectWeek",
                column: "WeekId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalObjectWeek");
        }
    }
}
