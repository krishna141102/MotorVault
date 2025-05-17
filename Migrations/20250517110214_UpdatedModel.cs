using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorVault.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModels_CarModels_CarModelId1",
                table: "CarModels");

            migrationBuilder.DropIndex(
                name: "IX_CarModels_CarModelId1",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "CarModelId1",
                table: "CarModels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarModelId1",
                table: "CarModels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarModels_CarModelId1",
                table: "CarModels",
                column: "CarModelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModels_CarModels_CarModelId1",
                table: "CarModels",
                column: "CarModelId1",
                principalTable: "CarModels",
                principalColumn: "CarModelId");
        }
    }
}
