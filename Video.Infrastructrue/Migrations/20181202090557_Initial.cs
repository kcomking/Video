using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Video.Infrastructrue.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: ""),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HeadImage = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Score = table.Column<int>(nullable: false, defaultValue: 0)
                        .Annotation("Sqlite:Autoincrement", true),
                    VipType = table.Column<int>(nullable: false),
                    VipExpirationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    IsGeneralAgent = table.Column<bool>(nullable: false, defaultValue: false),
                    GeneralAgentId = table.Column<int>(nullable: true),
                    FreeVideoCount = table.Column<int>(nullable: false, defaultValue: 0)
                        .Annotation("Sqlite:Autoincrement", true),
                    Balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    Leader3Id = table.Column<int>(nullable: true),
                    Leader1Id = table.Column<int>(nullable: true),
                    Leader2Id = table.Column<int>(nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PromoCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    IsShared = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_GeneralAgentId",
                        column: x => x.GeneralAgentId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_Leader1Id",
                        column: x => x.Leader1Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_Leader2Id",
                        column: x => x.Leader2Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_Leader3Id",
                        column: x => x.Leader3Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_GeneralAgentId",
                table: "Accounts",
                column: "GeneralAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Leader1Id",
                table: "Accounts",
                column: "Leader1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Leader2Id",
                table: "Accounts",
                column: "Leader2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Leader3Id",
                table: "Accounts",
                column: "Leader3Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
