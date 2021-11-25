using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coursework.Migrations
{
    public partial class EditedUserRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserRaterId",
                table: "UserRating",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7a08f647-1c30-4453-b46b-a9ad1a79c168"),
                column: "ConcurrencyStamp",
                value: "28e86af8-fb18-4859-b819-de904d9561ee");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("252352f7-e127-4c9d-ad06-dd6b859043d8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9b38f1fc-8950-4a80-b1bb-67db84f76bb4", "AQAAAAEAACcQAAAAEFco1ZpXSQ9ht2CubZi0ZbbYyYeNPyKp+zQrsvP5wXd3LK7On2FRbcY1dmqho1qNkA==" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7a08f647-1c30-4453-b46b-a9ad1a79c168"),
                column: "ConcurrencyStamp",
                value: "805d000f-bb1a-470f-8c78-e2bc9c01b4c1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("252352f7-e127-4c9d-ad06-dd6b859043d8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e17c0dc3-fa32-4af3-8caf-ddbc689fb8fb", "AQAAAAEAACcQAAAAECnzJQtNrIvQb1wKKyarQPw0eYG9MFlrJKGsGmzeMdOPEPbvzFB3P2BtW1drSV+lSA==" });
        }
    }
}
