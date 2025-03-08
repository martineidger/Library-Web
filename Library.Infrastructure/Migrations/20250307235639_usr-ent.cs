using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class usrent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "Books",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserEntityId",
                table: "Books",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_UserEntityId",
                table: "Books",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_UserEntityId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_UserEntityId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Books");
        }
    }
}
