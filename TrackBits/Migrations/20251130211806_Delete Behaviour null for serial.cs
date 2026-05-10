using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackBits.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBehaviournullforserial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_AspNetUsers_AssignedToUserId",
                table: "SerialNumbers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SerialNumbers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SerialNumbers",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "SerialNumbers",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "SerialNumbers",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "SerialNumbers",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "SerialNumbers",
                keyColumn: "Id",
                keyValue: 5,
                column: "UserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_UserId",
                table: "SerialNumbers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SerialNumbers_AspNetUsers_AssignedToUserId",
                table: "SerialNumbers",
                column: "AssignedToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SerialNumbers_AspNetUsers_UserId",
                table: "SerialNumbers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_AspNetUsers_AssignedToUserId",
                table: "SerialNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_AspNetUsers_UserId",
                table: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_SerialNumbers_UserId",
                table: "SerialNumbers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SerialNumbers");

            migrationBuilder.AddForeignKey(
                name: "FK_SerialNumbers_AspNetUsers_AssignedToUserId",
                table: "SerialNumbers",
                column: "AssignedToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
