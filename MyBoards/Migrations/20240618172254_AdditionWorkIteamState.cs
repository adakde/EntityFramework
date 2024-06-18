using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoards.Migrations
{
    /// <inheritdoc />
    public partial class AdditionWorkIteamState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table: "States", column: "Value", value: "On Hold");
            migrationBuilder.InsertData(table: "States", column: "Value", value: "Rejected");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "States", keyColumn: "Value", keyValue: "On Hold");
            migrationBuilder.DeleteData(table: "States", keyColumn: "Value", keyValue: "Rejected");
        }
    }
}
