using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMovieStore.Data.Migrations
{
    public partial class Validations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "712f9231-8b95-48fd-8f7f-f7960d24557b", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "712f9231-8b95-48fd-8f7f-f7960d24557b", "86cb3cb7-8430-49fd-ba25-0f89ea0cd666" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Balance", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "cfddf7d7-31ca-4484-a95b-dfef7a536acc", 0, 0.0, "d3238381-f5ae-4759-b2fa-28e308342ba4", "vksn@mail.com", true, false, null, "VKS@MAIL.COM", "VKSADMIN", "AQAAAAEAACcQAAAAEPbNqYVlZ7QCAZoqtOqlXKkiTreivACAtwxkdWIRlmgbDqMlBD0MNaSXHMiceMAr2A==", "+55555", true, "49879aa4-01e7-4819-aa27-c7a4103f9983", false, "VksAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "cfddf7d7-31ca-4484-a95b-dfef7a536acc", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "cfddf7d7-31ca-4484-a95b-dfef7a536acc", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "cfddf7d7-31ca-4484-a95b-dfef7a536acc", "d3238381-f5ae-4759-b2fa-28e308342ba4" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Balance", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "712f9231-8b95-48fd-8f7f-f7960d24557b", 0, 0.0, "86cb3cb7-8430-49fd-ba25-0f89ea0cd666", "vksn@mail.com", true, false, null, "VKS@MAIL.COM", "VKSADMIN", "AQAAAAEAACcQAAAAEFUzOFf+zqkcHGmfWivQiCxW24o8lAWLm16UsMM59C8m0+mcpEd8fDSD96BvvjrumA==", "+55555", true, "b7991a82-e0be-4327-9d28-86ff4a3c1f71", false, "VksAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "712f9231-8b95-48fd-8f7f-f7960d24557b", "1" });
        }
    }
}
