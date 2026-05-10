using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackBits.Migrations
{
    /// <inheritdoc />
    public partial class addBussinessName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessUserName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessUserName",
                table: "AspNetUsers");
        }
    }
}
