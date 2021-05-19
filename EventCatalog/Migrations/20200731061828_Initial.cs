using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCatalog.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "Event_Audience_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Event_Category_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Event_Format_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Event_Item_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Event_Kind_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Event_Language_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Event_Location_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Event_Zipcode_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "EventAudiences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Event_AgeGroup = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAudiences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Event_Category = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventFormats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Event_Format = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventFormats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventKinds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Event_Kind = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventKinds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Event_Language = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLanguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Event_Location = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventZipcodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Event_Zipcode = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventZipcodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Event_Name = table.Column<string>(maxLength: 200, nullable: false),
                    Event_Desc = table.Column<string>(maxLength: 500, nullable: false),
                    Event_Start_Time = table.Column<DateTime>(nullable: false),
                    Event_End_Time = table.Column<DateTime>(nullable: false),
                    Event_Pictureurl = table.Column<string>(nullable: true),
                    Event_Price = table.Column<decimal>(type: "Decimal", nullable: false),
                    Event_LocationId = table.Column<int>(nullable: false),
                    Event_CategoryId = table.Column<int>(nullable: false),
                    Event_LanguageId = table.Column<int>(nullable: false),
                    Event_KindId = table.Column<int>(nullable: false),
                    Event_ZipCodeId = table.Column<int>(nullable: false),
                    Event_Organiser = table.Column<string>(nullable: false),
                    Event_AudienceId = table.Column<int>(nullable: false),
                    Event_FormatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventItems_EventAudiences_Event_AudienceId",
                        column: x => x.Event_AudienceId,
                        principalTable: "EventAudiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventItems_EventCategories_Event_CategoryId",
                        column: x => x.Event_CategoryId,
                        principalTable: "EventCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventItems_EventFormats_Event_FormatId",
                        column: x => x.Event_FormatId,
                        principalTable: "EventFormats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventItems_EventKinds_Event_KindId",
                        column: x => x.Event_KindId,
                        principalTable: "EventKinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventItems_EventLanguages_Event_LanguageId",
                        column: x => x.Event_LanguageId,
                        principalTable: "EventLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventItems_EventLocations_Event_LocationId",
                        column: x => x.Event_LocationId,
                        principalTable: "EventLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventItems_EventZipcodes_Event_ZipCodeId",
                        column: x => x.Event_ZipCodeId,
                        principalTable: "EventZipcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventItems_Event_AudienceId",
                table: "EventItems",
                column: "Event_AudienceId");

            migrationBuilder.CreateIndex(
                name: "IX_EventItems_Event_CategoryId",
                table: "EventItems",
                column: "Event_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EventItems_Event_FormatId",
                table: "EventItems",
                column: "Event_FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_EventItems_Event_KindId",
                table: "EventItems",
                column: "Event_KindId");

            migrationBuilder.CreateIndex(
                name: "IX_EventItems_Event_LanguageId",
                table: "EventItems",
                column: "Event_LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EventItems_Event_LocationId",
                table: "EventItems",
                column: "Event_LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EventItems_Event_ZipCodeId",
                table: "EventItems",
                column: "Event_ZipCodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventItems");

            migrationBuilder.DropTable(
                name: "EventAudiences");

            migrationBuilder.DropTable(
                name: "EventCategories");

            migrationBuilder.DropTable(
                name: "EventFormats");

            migrationBuilder.DropTable(
                name: "EventKinds");

            migrationBuilder.DropTable(
                name: "EventLanguages");

            migrationBuilder.DropTable(
                name: "EventLocations");

            migrationBuilder.DropTable(
                name: "EventZipcodes");

            migrationBuilder.DropSequence(
                name: "Event_Audience_hilo");

            migrationBuilder.DropSequence(
                name: "Event_Category_hilo");

            migrationBuilder.DropSequence(
                name: "Event_Format_hilo");

            migrationBuilder.DropSequence(
                name: "Event_Item_hilo");

            migrationBuilder.DropSequence(
                name: "Event_Kind_hilo");

            migrationBuilder.DropSequence(
                name: "Event_Language_hilo");

            migrationBuilder.DropSequence(
                name: "Event_Location_hilo");

            migrationBuilder.DropSequence(
                name: "Event_Zipcode_hilo");
        }
    }
}
