using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.WebAPI.Migrations
{
    public partial class ConstraintsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserIdentity",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserIdentity_Email",
                table: "UserIdentity",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserIdentity_UserName",
                table: "UserIdentity",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserIdentity_Email",
                table: "UserIdentity");

            migrationBuilder.DropIndex(
                name: "IX_UserIdentity_UserName",
                table: "UserIdentity");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserIdentity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
