using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMovieStore.Data.Migrations
{
    public partial class Seeding_DB4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "e201512e-6dcb-41d7-9808-fe30d09a8eef", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "e201512e-6dcb-41d7-9808-fe30d09a8eef", "88404066-1d8f-4079-bdcd-2345d879d9e2" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Balance", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "712f9231-8b95-48fd-8f7f-f7960d24557b", 0, 0.0, "86cb3cb7-8430-49fd-ba25-0f89ea0cd666", "vksn@mail.com", true, false, null, "VKS@MAIL.COM", "VKSADMIN", "AQAAAAEAACcQAAAAEFUzOFf+zqkcHGmfWivQiCxW24o8lAWLm16UsMM59C8m0+mcpEd8fDSD96BvvjrumA==", "+55555", true, "b7991a82-e0be-4327-9d28-86ff4a3c1f71", false, "VksAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "712f9231-8b95-48fd-8f7f-f7960d24557b", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "e201512e-6dcb-41d7-9808-fe30d09a8eef", 0, 0.0, "88404066-1d8f-4079-bdcd-2345d879d9e2", "admin@mail.com", true, false, null, "ADMIN@MAIL.COM", "ADMINMAIN", "AQAAAAEAACcQAAAAEGmuVtDtRin7FfJIlE0Fy4kniGV2UMt1wTEDuDfvBtCz31x4zk1Z5xywqHksGvsmqg==", "+111111111", true, "5cb7ec09-e31b-4c66-a1d3-3fe6aa7a62b5", false, "VksAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "e201512e-6dcb-41d7-9808-fe30d09a8eef", "1" });
        }
    }
}
