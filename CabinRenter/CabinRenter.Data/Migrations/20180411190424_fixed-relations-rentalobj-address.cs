using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CabinRenter.Data.Migrations
{
    public partial class fixedrelationsrentalobjaddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_RentalObjects_RentalObjectId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_RentalObjectId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "RentalObjectId",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "RentalObjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RentalObjects_AddressId",
                table: "RentalObjects",
                column: "AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalObjects_Addresses_AddressId",
                table: "RentalObjects",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalObjects_Addresses_AddressId",
                table: "RentalObjects");

            migrationBuilder.DropIndex(
                name: "IX_RentalObjects_AddressId",
                table: "RentalObjects");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "RentalObjects");

            migrationBuilder.AddColumn<int>(
                name: "RentalObjectId",
                table: "Addresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RentalObjectId",
                table: "Addresses",
                column: "RentalObjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_RentalObjects_RentalObjectId",
                table: "Addresses",
                column: "RentalObjectId",
                principalTable: "RentalObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
