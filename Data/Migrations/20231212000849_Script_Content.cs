using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevOps_Site.Data.Migrations
{
    /// <inheritdoc />
    public partial class Script_Content : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scripts_Authors_AuthorID",
                table: "Scripts");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorID",
                table: "Scripts",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Scripts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ScriptContent",
                table: "Scripts",
                type: "TEXT",
                maxLength: 1200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Scripts_Authors_AuthorID",
                table: "Scripts",
                column: "AuthorID",
                principalTable: "Authors",
                principalColumn: "AuthorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scripts_Authors_AuthorID",
                table: "Scripts");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Scripts");

            migrationBuilder.DropColumn(
                name: "ScriptContent",
                table: "Scripts");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorID",
                table: "Scripts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Scripts_Authors_AuthorID",
                table: "Scripts",
                column: "AuthorID",
                principalTable: "Authors",
                principalColumn: "AuthorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
