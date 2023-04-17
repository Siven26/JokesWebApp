using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesWebApp.Data.Migrations
{
    public partial class InitialRatingUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Ratings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "JokeID",
                table: "Ratings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_JokeID_UserID",
                table: "Ratings",
                columns: new[] { "JokeID", "UserID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ratings_JokeID_UserID",
                table: "Ratings");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Ratings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "JokeID",
                table: "Ratings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
