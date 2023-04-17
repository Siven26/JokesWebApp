using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesWebApp.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentDateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                });

            migrationBuilder.CreateTable(
                name: "Jokes",
                columns: table => new
                {
                    JokeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JokeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JokeCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JokeText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JokeDateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jokes", x => x.JokeID);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JokeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RatingValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingID);
                });

            migrationBuilder.CreateTable(
                name: "UserJokeComments",
                columns: table => new
                {
                    UserJokeCommentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JokeID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserJokeComments", x => x.UserJokeCommentID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Jokes");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "UserJokeComments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
