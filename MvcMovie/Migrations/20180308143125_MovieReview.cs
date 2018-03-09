using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MvcMovie.Migrations
{
    public partial class MovieReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movie_MvcMovie.Models.Reviews_MovieID",
                table: "Reviews");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Movie_TempId_TempId1",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "MvcMovie.Models.Reviews",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "TempId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "TempId1",
                table: "Movie");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movie_MovieID",
                table: "Reviews",
                column: "MovieID",
                principalTable: "Movie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movie_MovieID",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "MvcMovie.Models.Reviews",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "Movie",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TempId1",
                table: "Movie",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Movie_TempId_TempId1",
                table: "Movie",
                columns: new[] { "TempId", "TempId1" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movie_MvcMovie.Models.Reviews_MovieID",
                table: "Reviews",
                columns: new[] { "MvcMovie.Models.Reviews", "MovieID" },
                principalTable: "Movie",
                principalColumns: new[] { "TempId", "TempId1" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
