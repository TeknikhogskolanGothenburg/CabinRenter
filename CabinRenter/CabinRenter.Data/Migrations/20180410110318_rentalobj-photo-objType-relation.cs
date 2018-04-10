using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CabinRenter.Data.Migrations
{
    public partial class rentalobjphotoobjTyperelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ObjectTypeId",
                table: "RentalObjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RentalObjectId",
                table: "Photos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RentalObjects_ObjectTypeId",
                table: "RentalObjects",
                column: "ObjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_RentalObjectId",
                table: "Photos",
                column: "RentalObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_RentalObjects_RentalObjectId",
                table: "Photos",
                column: "RentalObjectId",
                principalTable: "RentalObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalObjects_ObjectTypes_ObjectTypeId",
                table: "RentalObjects",
                column: "ObjectTypeId",
                principalTable: "ObjectTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_RentalObjects_RentalObjectId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalObjects_ObjectTypes_ObjectTypeId",
                table: "RentalObjects");

            migrationBuilder.DropIndex(
                name: "IX_RentalObjects_ObjectTypeId",
                table: "RentalObjects");

            migrationBuilder.DropIndex(
                name: "IX_Photos_RentalObjectId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ObjectTypeId",
                table: "RentalObjects");

            migrationBuilder.DropColumn(
                name: "RentalObjectId",
                table: "Photos");
        }
    }
}
