using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linquiztic.Migrations
{
    /// <inheritdoc />
    public partial class AddFirebaseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirebaseId",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirebaseId",
                table: "Users");
        }
    }
}
