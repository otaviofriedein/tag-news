using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tag_news.Migrations
{
    /// <inheritdoc />
    public partial class MakeUsuarioOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Noticias_Usuarios_UsuarioId",
                table: "Noticias");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Noticias",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Texto",
                table: "Noticias",
                type: "character varying(800)",
                maxLength: 800,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Noticias_Usuarios_UsuarioId",
                table: "Noticias",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Noticias_Usuarios_UsuarioId",
                table: "Noticias");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Noticias",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Texto",
                table: "Noticias",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(800)",
                oldMaxLength: 800);

            migrationBuilder.AddForeignKey(
                name: "FK_Noticias_Usuarios_UsuarioId",
                table: "Noticias",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
