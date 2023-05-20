using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesWebApp.Data.Migrations
{
    public partial class JokeCreatorDisplayed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_JokeID_UserID",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "UserJokeComments");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Ratings");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Jokes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_JokeID",
                table: "Ratings",
                column: "JokeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jokes_UserID",
                table: "Jokes",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Jokes_AspNetUsers_UserID",
                table: "Jokes",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jokes_AspNetUsers_UserID",
                table: "Jokes");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_JokeID",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Jokes_UserID",
                table: "Jokes");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Jokes");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "UserJokeComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Ratings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_JokeID_UserID",
                table: "Ratings",
                columns: new[] { "JokeID", "UserID" },
                unique: true);
        }
    }
}
