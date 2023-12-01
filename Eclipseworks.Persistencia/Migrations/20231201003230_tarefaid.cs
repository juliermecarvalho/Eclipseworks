using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eclipseworks.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class tarefaid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "campo_id",
                table: "historicos");

            migrationBuilder.AddColumn<long>(
                name: "tarefa_id",
                table: "historicos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tarefa_id",
                table: "historicos");

            migrationBuilder.AddColumn<string>(
                name: "campo_id",
                table: "historicos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
