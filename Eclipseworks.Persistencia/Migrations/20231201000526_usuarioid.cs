using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eclipseworks.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class usuarioid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nome_usuario",
                table: "historicos");

            migrationBuilder.CreateIndex(
                name: "IX_historicos_usuario_id",
                table: "historicos",
                column: "usuario_id");

            migrationBuilder.AddForeignKey(
                name: "FK_historicos_usuarios_usuario_id",
                table: "historicos",
                column: "usuario_id",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_historicos_usuarios_usuario_id",
                table: "historicos");

            migrationBuilder.DropIndex(
                name: "IX_historicos_usuario_id",
                table: "historicos");

            migrationBuilder.AddColumn<string>(
                name: "nome_usuario",
                table: "historicos",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}
