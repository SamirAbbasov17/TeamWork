using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRChatApi.Migrations
{
    /// <inheritdoc />
    public partial class Initilize2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeepLoggedIn",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "KeepLoggedIn",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
