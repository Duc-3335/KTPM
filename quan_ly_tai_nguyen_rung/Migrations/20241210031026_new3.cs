using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quan_ly_tai_nguyen_rung.Migrations
{
    /// <inheritdoc />
    public partial class new3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ANIMAL_ANIMAL_FACILITY_Animal_ID_ANIMAL",
                table: "ANIMAL_ANIMAL_FACILITY");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animal",
                table: "Animal");

            migrationBuilder.RenameTable(
                name: "Animal",
                newName: "ANIMAL");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ANIMAL",
                table: "ANIMAL",
                column: "ID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ANIMAL_ANIMAL_FACILITY_ANIMAL_ID_ANIMAL",
                table: "ANIMAL_ANIMAL_FACILITY",
                column: "ID_ANIMAL",
                principalTable: "ANIMAL",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ANIMAL_ANIMAL_FACILITY_AnimalFacilityId",
                table: "ANIMAL");

            migrationBuilder.DropForeignKey(
                name: "FK_ANIMAL_ANIMAL_FACILITY_ANIMAL_ID_ANIMAL",
                table: "ANIMAL_ANIMAL_FACILITY");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ANIMAL",
                table: "ANIMAL");

            migrationBuilder.DropIndex(
                name: "IX_ANIMAL_AnimalFacilityId",
                table: "ANIMAL");

            migrationBuilder.RenameTable(
                name: "ANIMAL",
                newName: "Animal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animal",
                table: "Animal",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ANIMAL_ANIMAL_FACILITY_Animal_ID_ANIMAL",
                table: "ANIMAL_ANIMAL_FACILITY",
                column: "ID_ANIMAL",
                principalTable: "Animal",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
