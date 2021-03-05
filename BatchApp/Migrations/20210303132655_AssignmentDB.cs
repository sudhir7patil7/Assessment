using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BatchAPI.Migrations
{
    public partial class AssignmentDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "batches",
                columns: table => new
                {
                    batchID = table.Column<Guid>(nullable: false),
                    businessUnit = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    batchPublishDate = table.Column<DateTime>(nullable: false),
                    requiredDate = table.Column<DateTime>(nullable: false),
                    fileName = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_batches", x => x.batchID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "batches");
        }
    }
}
