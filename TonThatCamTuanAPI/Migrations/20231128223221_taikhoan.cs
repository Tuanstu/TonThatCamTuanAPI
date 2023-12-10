using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TonThatCamTuanAPI.Migrations
{
    /// <inheritdoc />
    public partial class taikhoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "TaiKhoan");
        }
    }
}
