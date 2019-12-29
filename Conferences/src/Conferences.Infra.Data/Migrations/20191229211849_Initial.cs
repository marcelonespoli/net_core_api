    using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Conferences.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    DocumentId = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false),
                    ShortDescription = table.Column<string>(type: "varchar(150)", nullable: true),
                    LongDescription = table.Column<string>(type: "varchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Free = table.Column<bool>(nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    Online = table.Column<bool>(nullable: false),
                    CompanyName = table.Column<string>(type: "varchar(150)", nullable: false),
                    Excluded = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: true),
                    AddressId = table.Column<Guid>(nullable: true),
                    OrganizerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conferences_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conferences_Organizers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "Organizers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address1 = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Address2 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Address3 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Postcode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    County = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ConferenceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ConferenceId",
                table: "Addresses",
                column: "ConferenceId",
                unique: true,
                filter: "[ConferenceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Conferences_CategoryId",
                table: "Conferences",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Conferences_OrganizerId",
                table: "Conferences",
                column: "OrganizerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Conferences");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Organizers");
        }
    }
}
