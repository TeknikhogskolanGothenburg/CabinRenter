using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CabinRenter.Data.Migrations
{
    public partial class personaddressrentalobjectrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "RentalObjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RentalObjectId",
                table: "Addresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RentalObjects_PersonId",
                table: "RentalObjects",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonId",
                table: "Addresses",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RentalObjectId",
                table: "Addresses",
                column: "RentalObjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Persons_PersonId",
                table: "Addresses",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_RentalObjects_RentalObjectId",
                table: "Addresses",
                column: "RentalObjectId",
                principalTable: "RentalObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalObjects_Persons_PersonId",
                table: "RentalObjects",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Persons_PersonId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_RentalObjects_RentalObjectId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalObjects_Persons_PersonId",
                table: "RentalObjects");

            migrationBuilder.DropIndex(
                name: "IX_RentalObjects_PersonId",
                table: "RentalObjects");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_PersonId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_RentalObjectId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "RentalObjects");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "RentalObjectId",
                table: "Addresses");
        }
    }
}
