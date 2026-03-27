using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TetPee.Repository.Migrations
{
    /// <inheritdoc />
    public partial class seedataUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsDeleted", "Password", "Role", "UpdatedAt" },
                values: new object[] { new Guid("cd37964b-94cc-4027-bd27-86794cb55c6e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "admin@gmail.com", false, "PiedTeam", "Admin", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cd37964b-94cc-4027-bd27-86794cb55c6e"));
        }
    }
}
