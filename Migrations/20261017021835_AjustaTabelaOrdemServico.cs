using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechFix_API.Migrations
{
    /// <inheritdoc />
    public partial class AjustaTabelaOrdemServico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "OrdensServico");

            migrationBuilder.DropColumn(
                name: "DataFinalizacao",
                table: "OrdensServico");

            migrationBuilder.RenameColumn(
                name: "Equipamento",
                table: "OrdensServico",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "DescricaoProblema",
                table: "OrdensServico",
                newName: "DataAbertura");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "OrdensServico",
                newName: "Equipamento");

            migrationBuilder.RenameColumn(
                name: "DataAbertura",
                table: "OrdensServico",
                newName: "DescricaoProblema");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "OrdensServico",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFinalizacao",
                table: "OrdensServico",
                type: "TEXT",
                nullable: true);
        }
    }
}
