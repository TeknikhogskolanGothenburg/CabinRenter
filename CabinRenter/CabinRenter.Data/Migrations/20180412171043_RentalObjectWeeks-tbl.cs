using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CabinRenter.Data.Migrations
{
    public partial class RentalObjectWeekstbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalObjectWeek_Bookings_BookingId",
                table: "RentalObjectWeek");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalObjectWeek_RentalObjects_RentalObjectId",
                table: "RentalObjectWeek");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalObjectWeek_Weeks_WeekId",
                table: "RentalObjectWeek");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentalObjectWeek",
                table: "RentalObjectWeek");

            migrationBuilder.RenameTable(
                name: "RentalObjectWeek",
                newName: "RentalObjectWeeks");

            migrationBuilder.RenameIndex(
                name: "IX_RentalObjectWeek_WeekId",
                table: "RentalObjectWeeks",
                newName: "IX_RentalObjectWeeks_WeekId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalObjectWeek_BookingId",
                table: "RentalObjectWeeks",
                newName: "IX_RentalObjectWeeks_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentalObjectWeeks",
                table: "RentalObjectWeeks",
                columns: new[] { "RentalObjectId", "WeekId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RentalObjectWeeks_Bookings_BookingId",
                table: "RentalObjectWeeks",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalObjectWeeks_RentalObjects_RentalObjectId",
                table: "RentalObjectWeeks",
                column: "RentalObjectId",
                principalTable: "RentalObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalObjectWeeks_Weeks_WeekId",
                table: "RentalObjectWeeks",
                column: "WeekId",
                principalTable: "Weeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalObjectWeeks_Bookings_BookingId",
                table: "RentalObjectWeeks");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalObjectWeeks_RentalObjects_RentalObjectId",
                table: "RentalObjectWeeks");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalObjectWeeks_Weeks_WeekId",
                table: "RentalObjectWeeks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentalObjectWeeks",
                table: "RentalObjectWeeks");

            migrationBuilder.RenameTable(
                name: "RentalObjectWeeks",
                newName: "RentalObjectWeek");

            migrationBuilder.RenameIndex(
                name: "IX_RentalObjectWeeks_WeekId",
                table: "RentalObjectWeek",
                newName: "IX_RentalObjectWeek_WeekId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalObjectWeeks_BookingId",
                table: "RentalObjectWeek",
                newName: "IX_RentalObjectWeek_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentalObjectWeek",
                table: "RentalObjectWeek",
                columns: new[] { "RentalObjectId", "WeekId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RentalObjectWeek_Bookings_BookingId",
                table: "RentalObjectWeek",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalObjectWeek_RentalObjects_RentalObjectId",
                table: "RentalObjectWeek",
                column: "RentalObjectId",
                principalTable: "RentalObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalObjectWeek_Weeks_WeekId",
                table: "RentalObjectWeek",
                column: "WeekId",
                principalTable: "Weeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
