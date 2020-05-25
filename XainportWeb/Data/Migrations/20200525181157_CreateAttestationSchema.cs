using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XainportWeb.Migrations
{
    public partial class CreateAttestationSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Covid19AttestationModel",
                columns: table => new
                {
                    XainportId = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    TestedDateTime = table.Column<DateTime>(nullable: false),
                    Covid19Data = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Covid19AttestationModel", x => x.XainportId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Covid19AttestationModel");
        }
    }
}
