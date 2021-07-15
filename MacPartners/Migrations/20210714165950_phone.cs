using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MacPartners.Migrations
{
    public partial class phone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PhoneId",
                table: "Person",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AreaCode = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_PhoneId",
                table: "Person",
                column: "PhoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Phone_PhoneId",
                table: "Person",
                column: "PhoneId",
                principalTable: "Phone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Phone_PhoneId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropIndex(
                name: "IX_Person_PhoneId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PhoneId",
                table: "Person");
        }
    }
}
