using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerceStarterCode.Migrations
{
    public partial class addingUserRolesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "291b4388-34b6-4ba9-aa9a-61aff9a90127", "96273819-46b8-474c-b736-6e76c7a43dbc", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "57092b51-d9cd-4f23-b5c4-f35282579dc8", "81cca9c9-fb51-4387-942c-17a8d75f197b", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "291b4388-34b6-4ba9-aa9a-61aff9a90127");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57092b51-d9cd-4f23-b5c4-f35282579dc8");
        }
    }
}
