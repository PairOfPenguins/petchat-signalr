using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petchat.Migrations
{
    /// <inheritdoc />
    public partial class AssignedUserIdToMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedUserName",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedUserName",
                table: "Messages");
        }
    }
}
