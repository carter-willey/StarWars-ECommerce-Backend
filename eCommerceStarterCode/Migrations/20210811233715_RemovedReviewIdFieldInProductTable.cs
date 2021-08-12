using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerceStarterCode.Migrations
{
    public partial class RemovedReviewIdFieldInProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Reviews_ReviewId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ReviewId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01b534ff-1f8f-4303-9415-2d9277bebb40");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "158f38d0-b565-4da0-b75c-3045ada0889e");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "01b534ff-1f8f-4303-9415-2d9277bebb40", "e279bb34-b55f-4b11-ae69-9f3225e087b0", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "158f38d0-b565-4da0-b75c-3045ada0889e", "50125e87-3969-4f41-b46c-bfc768a39062", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ReviewId",
                table: "Products",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Reviews_ReviewId",
                table: "Products",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
