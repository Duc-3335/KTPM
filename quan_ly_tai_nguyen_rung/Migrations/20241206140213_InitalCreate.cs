using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quan_ly_tai_nguyen_rung.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ANIMAL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GENERIC = table.Column<int>(type: "int", nullable: false),
                    DATE_FOUND = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    STATUS = table.Column<int>(type: "int", nullable: false),
                    FLUCTUATION = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANIMAL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DISTRICT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DISTRICT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ROLES = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLES_GROUP",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ROLES = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES_GROUP", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "COMMUNE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ID_DISTRICT = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMMUNE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_COMMUNE_DISTRICT_ID_DISTRICT",
                        column: x => x.ID_DISTRICT,
                        principalTable: "DISTRICT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GROUPS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    STATUS = table.Column<bool>(type: "bit", nullable: false),
                    ID_ROLES_GROUP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GROUPS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GROUPS_ROLES_GROUP_ID_ROLES_GROUP",
                        column: x => x.ID_ROLES_GROUP,
                        principalTable: "ROLES_GROUP",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ANIMAL_FACILITY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ADDRESS = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CONTACT_FACE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CONTACT_MAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CONTACT_PHONE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LABOR = table.Column<int>(type: "int", nullable: false),
                    ACREAGE = table.Column<float>(type: "real", nullable: false),
                    IMAGE_ANIMAL_STORAGE = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ID_COMMUNE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANIMAL_FACILITY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ANIMAL_FACILITY_COMMUNE_ID_COMMUNE",
                        column: x => x.ID_COMMUNE,
                        principalTable: "COMMUNE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PLANT_FACILITY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    STATUS = table.Column<bool>(type: "bit", nullable: false),
                    ADDRESS = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CONTACT_FACE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CONTACT_MAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CONTACT_PHONE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ACREAGE = table.Column<float>(type: "real", nullable: false),
                    SEEDLINGS_YIELD = table.Column<float>(type: "real", nullable: false),
                    LABOR = table.Column<int>(type: "int", nullable: false),
                    IMAGE_LANT_BREEDING_FACILITY = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ID_COMMUNE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLANT_FACILITY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PLANT_FACILITY_COMMUNE_ID_COMMUNE",
                        column: x => x.ID_COMMUNE,
                        principalTable: "COMMUNE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    STATUS = table.Column<bool>(type: "bit", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PHONE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DISPLAY_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    USER_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ID_COMMUNE = table.Column<int>(type: "int", nullable: false),
                    ID_ROLES = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_COMMUNE_ID_COMMUNE",
                        column: x => x.ID_COMMUNE,
                        principalTable: "COMMUNE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_ROLES_ID_ROLES",
                        column: x => x.ID_ROLES,
                        principalTable: "ROLES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WOOD_PROCESSING_FACILITY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    STATUS = table.Column<bool>(type: "bit", nullable: false),
                    ADDRESS = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CONTACT_FACE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CONTACT_MAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CONTACT_PHONE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LABOR = table.Column<int>(type: "int", nullable: false),
                    ACREAGE = table.Column<float>(type: "real", nullable: false),
                    Yield = table.Column<float>(type: "real", nullable: true),
                    WOOD_SPECIES_PROVIDED = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PRODUCT = table.Column<int>(type: "int", maxLength: 300, nullable: false),
                    PRODUCTION_TYPE = table.Column<int>(type: "int", maxLength: 300, nullable: false),
                    ACTIVITY_FORM = table.Column<int>(type: "int", maxLength: 300, nullable: false),
                    IMAGE_WOOD_PROCESSING_FACILITY = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ID_COMMUNE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOOD_PROCESSING_FACILITY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WOOD_PROCESSING_FACILITY_COMMUNE_ID_COMMUNE",
                        column: x => x.ID_COMMUNE,
                        principalTable: "COMMUNE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ANIMAL_ANIMAL_FACILITY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_ANIMAL = table.Column<int>(type: "int", nullable: false),
                    ID_ANIMAL_FACILITY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANIMAL_ANIMAL_FACILITY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ANIMAL_ANIMAL_FACILITY_ANIMAL_FACILITY_ID_ANIMAL_FACILITY",
                        column: x => x.ID_ANIMAL_FACILITY,
                        principalTable: "ANIMAL_FACILITY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ANIMAL_ANIMAL_FACILITY_ANIMAL_ID_ANIMAL",
                        column: x => x.ID_ANIMAL,
                        principalTable: "ANIMAL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PLANT_TYPE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TYPE = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<int>(type: "int", nullable: false),
                    HEIGHT = table.Column<int>(type: "int", nullable: false),
                    PlantFacilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLANT_TYPE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PLANT_TYPE_PLANT_FACILITY_PlantFacilityId",
                        column: x => x.PlantFacilityId,
                        principalTable: "PLANT_FACILITY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ACCESS_HISTORY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TYPE = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    TIME = table.Column<TimeSpan>(type: "time", nullable: false),
                    DAY = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_USER = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCESS_HISTORY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ACCESS_HISTORY_USER_ID_USER",
                        column: x => x.ID_USER,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IMPACT_HISTORY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TYPE = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    TIME = table.Column<TimeSpan>(type: "time", nullable: false),
                    DAY = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_USER = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMPACT_HISTORY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IMPACT_HISTORY_USER_ID_USER",
                        column: x => x.ID_USER,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_GROUP",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdGroup = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_GROUP", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_GROUP_GROUPS_IdGroup",
                        column: x => x.IdGroup,
                        principalTable: "GROUPS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_GROUP_USER_IdUser",
                        column: x => x.IdUser,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ACCESS_HISTORY_ID_USER",
                table: "ACCESS_HISTORY",
                column: "ID_USER");

            migrationBuilder.CreateIndex(
                name: "IX_ANIMAL_ANIMAL_FACILITY_ID_ANIMAL",
                table: "ANIMAL_ANIMAL_FACILITY",
                column: "ID_ANIMAL");

            migrationBuilder.CreateIndex(
                name: "IX_ANIMAL_ANIMAL_FACILITY_ID_ANIMAL_FACILITY",
                table: "ANIMAL_ANIMAL_FACILITY",
                column: "ID_ANIMAL_FACILITY");

            migrationBuilder.CreateIndex(
                name: "IX_ANIMAL_FACILITY_ID_COMMUNE",
                table: "ANIMAL_FACILITY",
                column: "ID_COMMUNE");

            migrationBuilder.CreateIndex(
                name: "IX_COMMUNE_ID_DISTRICT",
                table: "COMMUNE",
                column: "ID_DISTRICT");

            migrationBuilder.CreateIndex(
                name: "IX_GROUPS_ID_ROLES_GROUP",
                table: "GROUPS",
                column: "ID_ROLES_GROUP");

            migrationBuilder.CreateIndex(
                name: "IX_IMPACT_HISTORY_ID_USER",
                table: "IMPACT_HISTORY",
                column: "ID_USER");

            migrationBuilder.CreateIndex(
                name: "IX_PLANT_FACILITY_ID_COMMUNE",
                table: "PLANT_FACILITY",
                column: "ID_COMMUNE");

            migrationBuilder.CreateIndex(
                name: "IX_PLANT_TYPE_PlantFacilityId",
                table: "PLANT_TYPE",
                column: "PlantFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ID_COMMUNE",
                table: "USER",
                column: "ID_COMMUNE");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ID_ROLES",
                table: "USER",
                column: "ID_ROLES");

            migrationBuilder.CreateIndex(
                name: "IX_USER_GROUP_IdGroup",
                table: "USER_GROUP",
                column: "IdGroup");

            migrationBuilder.CreateIndex(
                name: "IX_USER_GROUP_IdUser",
                table: "USER_GROUP",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_WOOD_PROCESSING_FACILITY_ID_COMMUNE",
                table: "WOOD_PROCESSING_FACILITY",
                column: "ID_COMMUNE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCESS_HISTORY");

            migrationBuilder.DropTable(
                name: "ANIMAL_ANIMAL_FACILITY");

            migrationBuilder.DropTable(
                name: "IMPACT_HISTORY");

            migrationBuilder.DropTable(
                name: "PLANT_TYPE");

            migrationBuilder.DropTable(
                name: "USER_GROUP");

            migrationBuilder.DropTable(
                name: "WOOD_PROCESSING_FACILITY");

            migrationBuilder.DropTable(
                name: "ANIMAL_FACILITY");

            migrationBuilder.DropTable(
                name: "ANIMAL");

            migrationBuilder.DropTable(
                name: "PLANT_FACILITY");

            migrationBuilder.DropTable(
                name: "GROUPS");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "ROLES_GROUP");

            migrationBuilder.DropTable(
                name: "COMMUNE");

            migrationBuilder.DropTable(
                name: "ROLES");

            migrationBuilder.DropTable(
                name: "DISTRICT");
        }
    }
}
