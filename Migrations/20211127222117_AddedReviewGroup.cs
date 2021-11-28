using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Coursework.Migrations
{
    public partial class AddedReviewGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Reviews",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReviewGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewGroups", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7a08f647-1c30-4453-b46b-a9ad1a79c168"),
                column: "ConcurrencyStamp",
                value: "bf1c547e-2a62-43d7-9353-05dabba0786d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("252352f7-e127-4c9d-ad06-dd6b859043d8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "15e68e49-3e68-4650-809d-64353608cead", "AQAAAAEAACcQAAAAEH/mFR03M+CJNj5Xq2EKKjlX4dUS2sc47v2j3cGoxGhibk8r8CyR9DEcyH1dI/rQow==" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_GroupId",
                table: "Reviews",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ReviewGroups_GroupId",
                table: "Reviews",
                column: "GroupId",
                principalTable: "ReviewGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ReviewGroups_GroupId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "ReviewGroups");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_GroupId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Reviews");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7a08f647-1c30-4453-b46b-a9ad1a79c168"),
                column: "ConcurrencyStamp",
                value: "d3d2ccfa-f531-4fd8-9be5-77a4e8c24906");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("252352f7-e127-4c9d-ad06-dd6b859043d8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "64a927c0-7121-4445-9b77-146ff4b910d4", "AQAAAAEAACcQAAAAEN0w8f9qn8cV10qGltYw5+oIVp8SyysFl1IQ2aF2nLMCWqXxYItlD4E9Ra373nudyg==" });
        }
    }
}
