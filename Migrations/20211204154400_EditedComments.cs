using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coursework.Migrations
{
    public partial class EditedComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Comment");

            migrationBuilder.AddColumn<DateTime>(
                name: "PostTime",
                table: "Comment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4ed68c3a-1482-4e93-a84f-c9262fb24f10");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c8e59697-5954-428f-9aa9-b9d99c063016", "AQAAAAEAACcQAAAAENb3/AZfJmFEJdf61wUjeGaYRar9UdZMTSGFsA1wm2VYihCH63TiBPKr2i67vVtgIQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostTime",
                table: "Comment");

            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "Comment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Comment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "27e6318a-9b3d-4e53-84d3-8c5ee3a30374");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f36eae06-c0f7-4495-9731-b5a0fa0f3e60", "AQAAAAEAACcQAAAAEO2y5vsmMwegsRwQV5cBmF8BW68StUskUE0rgh/xSHb85Ael+TB+lTpk+vVzD5/Z/A==" });
        }
    }
}
