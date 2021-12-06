using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

namespace Coursework.Migrations
{
    public partial class AddedFTSToComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "Comment",
                type: "tsvector",
                nullable: true)
                .Annotation("Npgsql:TsVectorConfig", "russian")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Text" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2d622da1-8c6a-4c64-b5f7-6ba7aeed37af");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "33d171b4-c027-45ed-aa49-f13791144427", "AQAAAAEAACcQAAAAEJDnvxFkuFPFFF4pf39fdG1R5SwwoLm0NfTWRqalnqgPF6ebUTAhLWWov1TMVHZ2iw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_SearchVector",
                table: "Comment",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comment_SearchVector",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "Comment");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "46e998a9-3caf-4e11-b564-011238b84f55");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "53539be3-48fb-4f51-90f1-ff19b491fd6f", "AQAAAAEAACcQAAAAEAaSq+IkoyUafykM0I0cUxIy7/15Id37JkGw7SCxYUxWTbO+JEeuT5r6mIH8jeC00A==" });
        }
    }
}
