using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesWebApp.Data.Migrations
{
    public partial class Comedians : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comedians",
                columns: table => new
                {
                    ComedianID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ComedianName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComedianImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComedianDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comedians", x => x.ComedianID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comedians");
        }
    }
}
