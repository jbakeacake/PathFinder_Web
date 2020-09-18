using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Persistence.Migrations
{
    public partial class AddItemTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId",
                table: "Users_Tbl",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemType_Tbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemType_Tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Player_Tbl",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Max_HP = table.Column<int>(nullable: false),
                    HP = table.Column<int>(nullable: false),
                    XP = table.Column<int>(nullable: false),
                    Gold = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Strength = table.Column<int>(nullable: false),
                    Dexterity = table.Column<int>(nullable: false),
                    Intelligence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player_Tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Potion_Tbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Gold = table.Column<int>(nullable: false),
                    Heal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Potion_Tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shield_Tbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Gold = table.Column<int>(nullable: false),
                    ArmorRating = table.Column<int>(nullable: false),
                    MaxDurability = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shield_Tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weapon_Tbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Gold = table.Column<int>(nullable: false),
                    MinDamage = table.Column<int>(nullable: false),
                    MaxDamage = table.Column<int>(nullable: false),
                    MaxDurability = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapon_Tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items_Tbl",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TypeReferenceId = table.Column<int>(nullable: false),
                    SubTypeReferenceId = table.Column<int>(nullable: false),
                    Container = table.Column<int>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items_Tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Tbl_Player_Tbl_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player_Tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Tbl_PlayerId",
                table: "Users_Tbl",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Tbl_PlayerId",
                table: "Items_Tbl",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tbl_Player_Tbl_PlayerId",
                table: "Users_Tbl",
                column: "PlayerId",
                principalTable: "Player_Tbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tbl_Player_Tbl_PlayerId",
                table: "Users_Tbl");

            migrationBuilder.DropTable(
                name: "Items_Tbl");

            migrationBuilder.DropTable(
                name: "ItemType_Tbl");

            migrationBuilder.DropTable(
                name: "Potion_Tbl");

            migrationBuilder.DropTable(
                name: "Shield_Tbl");

            migrationBuilder.DropTable(
                name: "Weapon_Tbl");

            migrationBuilder.DropTable(
                name: "Player_Tbl");

            migrationBuilder.DropIndex(
                name: "IX_Users_Tbl_PlayerId",
                table: "Users_Tbl");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Users_Tbl");
        }
    }
}
