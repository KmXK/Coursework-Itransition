using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coursework.Migrations
{
    public partial class AddedReviewId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRating_Reviews_ReviewId",
                table: "ReviewRating");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "ReviewRating",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRating_Reviews_ReviewId",
                table: "ReviewRating",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewRating_Reviews_ReviewId",
                table: "ReviewRating");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "ReviewRating",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7a08f647-1c30-4453-b46b-a9ad1a79c168"),
                column: "ConcurrencyStamp",
                value: "23b73206-806f-441b-8c5e-f28bdc1ed0bf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("252352f7-e127-4c9d-ad06-dd6b859043d8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "527fc978-e4cf-438f-b504-9efda8cfb6e5", "AQAAAAEAACcQAAAAEHwPxrpWuwFywscADKVmq9MWxt7gAOsD8V003tKSDAKg9C8c2n3HkliaiGsVetWM6Q==" });

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewRating_Reviews_ReviewId",
                table: "ReviewRating",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
