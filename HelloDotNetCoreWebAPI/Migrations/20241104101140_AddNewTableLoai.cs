using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace HelloDotNetCoreWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTableLoai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "loaiId",
                table: "hang_hoa",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "loai_id",
                table: "hang_hoa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "loai",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loai", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_hang_hoa_loai_id",
                table: "hang_hoa",
                column: "loai_id");

            migrationBuilder.AddForeignKey(
                name: "FK_hang_hoa_loai_loai_id",
                table: "hang_hoa",
                column: "loai_id",
                principalTable: "loai",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hang_hoa_loai_loai_id",
                table: "hang_hoa");

            migrationBuilder.DropTable(
                name: "loai");

            migrationBuilder.DropIndex(
                name: "IX_hang_hoa_loai_id",
                table: "hang_hoa");

            migrationBuilder.DropColumn(
                name: "loaiId",
                table: "hang_hoa");

            migrationBuilder.DropColumn(
                name: "loai_id",
                table: "hang_hoa");
        }
    }
}
