using Microsoft.EntityFrameworkCore.Migrations;

namespace Login.Migrations
{
    public partial class room : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GetUsers",
                table: "GetUsers");

            migrationBuilder.RenameTable(
                name: "GetUsers",
                newName: "AccLogin");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccLogin",
                table: "AccLogin",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AccLogin",
                table: "AccLogin");

            migrationBuilder.RenameTable(
                name: "AccLogin",
                newName: "GetUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetUsers",
                table: "GetUsers",
                column: "Id");
        }
    }
}
