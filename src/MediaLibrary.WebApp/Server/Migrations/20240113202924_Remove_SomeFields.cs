using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaLibrary.WebApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class Remove_SomeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtisticRole",
                table: "Contributor");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Contributor");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Contributor");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Media",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Media",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ArtisticRole",
                table: "Contributor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Contributor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Contributor",
                type: "datetime2",
                nullable: true);
        }
    }
}
