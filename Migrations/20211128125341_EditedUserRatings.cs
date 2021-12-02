using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coursework.Migrations
{
    public partial class EditedUserRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_AspNetUsers_UserRaterId",
                table: "UserRating");

            migrationBuilder.DropIndex(
                name: "IX_UserRating_UserRaterId",
                table: "UserRating");

            migrationBuilder.DropColumn(
                name: "UserRaterId",
                table: "UserRating");

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "UserRating",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7a08f647-1c30-4453-b46b-a9ad1a79c168"),
                column: "ConcurrencyStamp",
                value: "77f2d437-07f8-42ac-8ad8-76ad74469d15");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("252352f7-e127-4c9d-ad06-dd6b859043d8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "UserName" },
                values: new object[] { "40c7d0fd-97ea-4880-ac54-67a8729921c6", "AQAAAAEAACcQAAAAEDXn8P0V3SwOBt3eyhct83k1Si5n5Eu7xK8iwbK+pcsv/NDbZTSGrFk9/Uns4mjEsw==", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_UserRating_ReviewId",
                table: "UserRating",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_Reviews_ReviewId",
                table: "UserRating",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_Reviews_ReviewId",
                table: "UserRating");

            migrationBuilder.DropIndex(
                name: "IX_UserRating_ReviewId",
                table: "UserRating");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "UserRating");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "AspNetUsers",
                newName: "Username");

            migrationBuilder.AddColumn<Guid>(
                name: "UserRaterId",
                table: "UserRating",
                type: "uuid",
                nullable: true);

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Username" },
                values: new object[] { "15e68e49-3e68-4650-809d-64353608cead", "AQAAAAEAACcQAAAAEH/mFR03M+CJNj5Xq2EKKjlX4dUS2sc47v2j3cGoxGhibk8r8CyR9DEcyH1dI/rQow==", null });

            migrationBuilder.CreateIndex(
                name: "IX_UserRating_UserRaterId",
                table: "UserRating",
                column: "UserRaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_AspNetUsers_UserRaterId",
                table: "UserRating",
                column: "UserRaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
