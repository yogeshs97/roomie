using Microsoft.EntityFrameworkCore.Migrations;

namespace List.Migrations
{
    public partial class roomlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    SpaceCount = table.Column<int>(nullable: false),
                    Rent = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ContactNo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomList", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomList");
        }
    }
}
