using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaLibrary.WebApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class Remove_MediaTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_MediaType_MediaTypeId",
                table: "Media");

            migrationBuilder.DropTable(
                name: "MediaType");

            migrationBuilder.DropIndex(
                name: "IX_Media_MediaTypeId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "AdditionalInformation",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Media");

            migrationBuilder.RenameColumn(
                name: "UpdateUser",
                table: "Media",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "Media",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "MediaTypeId",
                table: "Media",
                newName: "MediaType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MediaType",
                table: "Media",
                newName: "MediaTypeId");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Media",
                newName: "UpdateUser");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Media",
                newName: "Summary");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInformation",
                table: "Media",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "Media",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Media",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Media",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Media",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Media",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MediaType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Media_MediaTypeId",
                table: "Media",
                column: "MediaTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_MediaType_MediaTypeId",
                table: "Media",
                column: "MediaTypeId",
                principalTable: "MediaType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
