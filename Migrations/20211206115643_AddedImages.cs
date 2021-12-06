using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Coursework.Migrations
{
    public partial class AddedImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageUrl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true),
                    ReviewId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageUrl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageUrl_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ImageUrl_ReviewId",
                table: "ImageUrl",
                column: "ReviewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageUrl");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e9f5bbd7-db69-4226-816a-0daa0c485873");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e6e0df4a-90fe-4771-bbf4-8fca4251ecc1", "AQAAAAEAACcQAAAAEIziADaVlpd8xY7BO2XGmTGX80C2I1RLEDBt6bHKJQiBvfQB02BOS5ZG/+RxjIHGTw==" });
        }
    }
}
