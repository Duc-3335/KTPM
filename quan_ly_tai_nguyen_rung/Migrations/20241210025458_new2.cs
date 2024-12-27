using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quan_ly_tai_nguyen_rung.Migrations
{
    /// <inheritdoc />
    public partial class new2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QUANTITY",
                table: "ANIMAL",
                newName: "PREVIOUS_QUANTITY");

            migrationBuilder.AlterColumn<byte[]>(
                name: "IMAGE_ANIMAL_STORAGE",
                table: "ANIMAL_FACILITY",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<double>(
                name: "ACREAGE",
                table: "ANIMAL_FACILITY",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "AnimalFacilityId",
                table: "ANIMAL",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CURRENT_QUANTITY",
                table: "ANIMAL",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATE",
                table: "ANIMAL",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IS_ACTIVE",
                table: "ANIMAL",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LOCATION",
                table: "ANIMAL",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "REASON",
                table: "ANIMAL",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ANIMAL_AnimalFacilityId",
                table: "ANIMAL",
                column: "AnimalFacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ANIMAL_ANIMAL_FACILITY_AnimalFacilityId",
                table: "ANIMAL",
                column: "AnimalFacilityId",
                principalTable: "ANIMAL_FACILITY",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ANIMAL_ANIMAL_FACILITY_AnimalFacilityId",
                table: "ANIMAL");

            migrationBuilder.DropIndex(
                name: "IX_ANIMAL_AnimalFacilityId",
                table: "ANIMAL");

            migrationBuilder.DropColumn(
                name: "AnimalFacilityId",
                table: "ANIMAL");

            migrationBuilder.DropColumn(
                name: "CURRENT_QUANTITY",
                table: "ANIMAL");

            migrationBuilder.DropColumn(
                name: "DATE",
                table: "ANIMAL");

            migrationBuilder.DropColumn(
                name: "IS_ACTIVE",
                table: "ANIMAL");

            migrationBuilder.DropColumn(
                name: "LOCATION",
                table: "ANIMAL");

            migrationBuilder.DropColumn(
                name: "REASON",
                table: "ANIMAL");

            migrationBuilder.RenameColumn(
                name: "PREVIOUS_QUANTITY",
                table: "ANIMAL",
                newName: "QUANTITY");

            migrationBuilder.AlterColumn<byte[]>(
                name: "IMAGE_ANIMAL_STORAGE",
                table: "ANIMAL_FACILITY",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ACREAGE",
                table: "ANIMAL_FACILITY",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
