using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_Seminarski_rad.Data.Migrations
{
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cd4c5e7b-f1c3-4fc5-a053-91f98f92d230", "d05a0377-1354-496e-813a-9f48117021a6", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd4c5e7b-f1c3-4fc5-a053-91f98f92d230");
        }
    }
}
