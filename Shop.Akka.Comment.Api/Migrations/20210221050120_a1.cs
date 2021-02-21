using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Migrations
{
    public partial class a1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_comment",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 36, nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    last_modification_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deletion_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    reply = table.Column<string>(maxLength: 500, nullable: true),
                    order_detail_id = table.Column<string>(maxLength: 36, nullable: true),
                    nickname = table.Column<string>(maxLength: 20, nullable: true),
                    star = table.Column<int>(nullable: false),
                    product_id = table.Column<string>(maxLength: 36, nullable: true),
                    order_id = table.Column<string>(maxLength: 36, nullable: true),
                    content = table.Column<string>(maxLength: 500, nullable: true),
                    status = table.Column<string>(maxLength: 1, nullable: true),
                    account = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_comment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "t_comment_type",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 36, nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    last_modification_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deletion_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    code = table.Column<string>(maxLength: 10, nullable: true),
                    name = table.Column<string>(maxLength: 20, nullable: true),
                    status = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_comment_type", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_comment");

            migrationBuilder.DropTable(
                name: "t_comment_type");
        }
    }
}
