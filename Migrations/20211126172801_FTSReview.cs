using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

namespace Coursework.Migrations
{
    public partial class FTSReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "Reviews",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SearchVector",
                table: "Reviews",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.Sql(
                @"CREATE TRIGGER review_search_vector_update BEFORE INSERT OR UPDATE
                  ON ""Reviews"" FOR EACH ROW EXECUTE PROCEDURE
                  tsvector_update_trigger(""SearchVector"", 'pg_catalog.english', ""Title"", ""Text"");");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviews_SearchVector",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "Reviews");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7a08f647-1c30-4453-b46b-a9ad1a79c168"),
                column: "ConcurrencyStamp",
                value: "b2ca589a-27db-4cda-9b72-6ae993a28feb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("252352f7-e127-4c9d-ad06-dd6b859043d8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cb5793a5-6fae-4255-8425-8544570122ad", "AQAAAAEAACcQAAAAEKkPuVCINkOpPGk1q3LP2QQiRrtsRhR8QpDfCxnmMLDOkYmrJHd5aASh6hJYQgBsWA==" });

            migrationBuilder.Sql("DROP TRIGGER review_search_vector_update");
        }
    }
}
