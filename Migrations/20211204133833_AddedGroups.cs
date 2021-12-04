using Microsoft.EntityFrameworkCore.Migrations;

namespace Coursework.Migrations
{
    public partial class AddedGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "ReviewGroups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Movies" },
                    { 2, "Events" },
                    { 3, "Games" },
                    { 4, "Books" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ReviewGroups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ReviewGroups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ReviewGroups",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ReviewGroups",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6b51b8ea-9392-43b5-a25e-4ce3287f7707");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2a978fa4-ad0a-4050-8ce3-7aed62da4fa9", "AQAAAAEAACcQAAAAEKBjqM5EVpBXakgu/RCI35YVVPHfJ0t61Zl+G1p+yJgw5DQVfA36I99IL7LHrBBBdw==" });
        }
    }
}
