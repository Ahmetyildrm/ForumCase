using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumCase.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Discriminator", "Email", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), "Admin", "admin@hotmail.com", "A", "A", "adminadmin", "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Discriminator", "Email", "FirstName", "LastName", "Password" },
                values: new object[] { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "User", "ahmetyildirim@hotmail.com", "Ahmet", "Yildirim", "1234567890" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Discriminator", "Email", "FirstName", "LastName", "Password" },
                values: new object[] { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "User", "user@hotmail.com", "A", "User", "1234567890" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "Content", "OparatedBy", "Star", "Status", "Title", "UserId" },
                values: new object[] { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Deneme Content", "NoOne", 0, "Pending", "Deneme Title", new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
        }
    }
}
