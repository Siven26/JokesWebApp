using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesWebApp.Data.Migrations
{
    public partial class JokeRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ratings_JokeID",
                table: "Ratings");

            migrationBuilder.AddColumn<DateTime>(
                name: "RatingDateAdded",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Ratings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_JokeID",
                table: "Ratings",
                column: "JokeID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserID",
                table: "Ratings",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_UserID",
                table: "Ratings",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Jokes_JokeID",
                table: "Ratings",
                column: "JokeID",
                principalTable: "Jokes",
                principalColumn: "JokeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_UserID",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Jokes_JokeID",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_JokeID",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_UserID",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "RatingDateAdded",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Ratings");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_JokeID",
                table: "Ratings",
                column: "JokeID",
                unique: true);
        }
    }
}
