using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eclipseworks.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataConclucao",
                table: "tarefas",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataConclucao",
                table: "tarefas");
        }
    }
}
