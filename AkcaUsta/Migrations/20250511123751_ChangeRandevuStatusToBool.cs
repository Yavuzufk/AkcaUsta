using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AkcaUsta.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRandevuStatusToBool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "RandevuStatus",
                table: "ServiceRandevus",
                type: "bit",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "RandevuStatus",
                table: "ServiceRandevus",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
