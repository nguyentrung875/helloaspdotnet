using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloDotNetCoreWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    token = table.Column<string>(type: "longtext", nullable: false),
                    jwtToken = table.Column<string>(type: "longtext", nullable: false),
                    isUsed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isRevoked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    issuedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    expiredAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_nguoi_dung_userId",
                        column: x => x.userId,
                        principalTable: "nguoi_dung",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_userId",
                table: "RefreshToken",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");
        }
    }
}
