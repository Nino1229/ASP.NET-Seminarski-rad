using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_Seminarski_rad.Data.Migrations
{
    public partial class BillingAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2c12801-0c68-4f75-97b9-fc6258cf804b");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress",
                table: "Order",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingCity",
                table: "Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingCountry",
                table: "Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingFirstName",
                table: "Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingLastName",
                table: "Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingPhoneNumber",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingZipCode",
                table: "Order",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "88dc19fb-75cf-4a5a-ae3e-f7211fdd7f3f", "00d36d6c-5fc3-449f-be7d-a27ecdde547e", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88dc19fb-75cf-4a5a-ae3e-f7211fdd7f3f");

            migrationBuilder.DropColumn(
                name: "BillingAddress",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BillingCity",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BillingCountry",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BillingFirstName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BillingLastName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BillingPhoneNumber",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BillingZipCode",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e2c12801-0c68-4f75-97b9-fc6258cf804b", "2aaf9886-69fd-47f1-be1b-ac63c5a0ceb5", "Admin", "ADMIN" });
        }
    }
}
