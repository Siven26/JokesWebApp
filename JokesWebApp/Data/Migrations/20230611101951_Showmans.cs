using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesWebApp.Data.Migrations
{
    public partial class Showmans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Showmans",
                columns: table => new
                {
                    ShowmanID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShowmanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowmanImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowmanDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showmans", x => x.ShowmanID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Showmans");
        }
    }
}
