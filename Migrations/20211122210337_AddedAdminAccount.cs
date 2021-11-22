using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coursework.Migrations
{
    public partial class AddedAdminAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserRoles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("7a08f647-1c30-4453-b46b-a9ad1a79c168"), "5b2101ec-f391-4762-95f4-b59f9b498b92", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarUrl", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("252352f7-e127-4c9d-ad06-dd6b859043d8"), 0, "/Files/no_avatar.jpg", "cb884e20-6cbb-4f93-b6e0-f8c6082695a6", null, false, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEB4tO/cXACoYPEp7UcbkNTxtCnkjG2CsZp3QY/JBt/uvEAGwp4fdwoa47Z+G3NMvbg==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId", "Discriminator" },
                values: new object[] { new Guid("252352f7-e127-4c9d-ad06-dd6b859043d8"), new Guid("7a08f647-1c30-4453-b46b-a9ad1a79c168"), "ApplicationUserRole" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("252352f7-e127-4c9d-ad06-dd6b859043d8"), new Guid("7a08f647-1c30-4453-b46b-a9ad1a79c168") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7a08f647-1c30-4453-b46b-a9ad1a79c168"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("252352f7-e127-4c9d-ad06-dd6b859043d8"));

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserRoles");
        }
    }
}
