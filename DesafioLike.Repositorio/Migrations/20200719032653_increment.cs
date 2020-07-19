using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioLike.Repositorio.Migrations
{
    public partial class increment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Respostas",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Categorias",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Respostas_UserId",
                table: "Respostas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Respostas_AspNetUsers_UserId",
                table: "Respostas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respostas_AspNetUsers_UserId",
                table: "Respostas");

            migrationBuilder.DropIndex(
                name: "IX_Respostas_UserId",
                table: "Respostas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Respostas");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Categorias",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}
