using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerceStarterCode.Migrations
{
    public partial class UserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "368b17fc-5f8d-4e42-be4c-2487cb4c55a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e98eee23-4b18-4e5a-95b9-d042828edda7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1bbe737a-bf9a-4ced-ac5d-80a9c6991667", "5ce771f4-ee5f-4466-9d7f-3e43e4bc4b77", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cb09bf3c-2051-446b-86fd-b7d960d52ec8", "d265337e-883e-4b12-89f3-48046d682c0f", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bbe737a-bf9a-4ced-ac5d-80a9c6991667");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb09bf3c-2051-446b-86fd-b7d960d52ec8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "368b17fc-5f8d-4e42-be4c-2487cb4c55a3", "6832f70a-a4fb-4cca-850c-9b93a934e64f", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e98eee23-4b18-4e5a-95b9-d042828edda7", "961ca5a2-8b78-4f90-85b8-78f7d4022692", "Admin", "ADMIN" });
        }
    }
}
