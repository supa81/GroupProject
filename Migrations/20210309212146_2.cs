using Microsoft.EntityFrameworkCore.Migrations;

namespace PawMates.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39db63f3-d78c-4218-9483-4bab8923b647");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Dogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Dogs",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "86bc5f60-529e-4c3b-b452-bdf2d811bed2", "0643d62c-a06b-4f5b-bdcb-a334e947116f", "Owner", "OWNER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86bc5f60-529e-4c3b-b452-bdf2d811bed2");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Dogs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "39db63f3-d78c-4218-9483-4bab8923b647", "a6dc554c-26f6-4152-b1a4-c83e7b04f029", "Owner", "OWNER" });
        }
    }
}
