using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CabinRenter.Data.Migrations
{
    public partial class bookingweekrentalobjrelationpersonbookingrel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "RentalObjectWeek",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Bookings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RentalObjectWeek_BookingId",
                table: "RentalObjectWeek",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PersonId",
                table: "Bookings",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Persons_PersonId",
                table: "Bookings",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalObjectWeek_Bookings_BookingId",
                table: "RentalObjectWeek",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Persons_PersonId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalObjectWeek_Bookings_BookingId",
                table: "RentalObjectWeek");

            migrationBuilder.DropIndex(
                name: "IX_RentalObjectWeek_BookingId",
                table: "RentalObjectWeek");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_PersonId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "RentalObjectWeek");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Bookings");
        }
    }
}
