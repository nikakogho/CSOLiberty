using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSOLiberty.Migrations
{
    public partial class setup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    client_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_fname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    client_lname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.client_id);
                });

            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    seller_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    seller_fname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    seller_lname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    seller_boss_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.seller_id);
                    table.ForeignKey(
                        name: "FK_Sellers_Sellers_seller_boss_id",
                        column: x => x.seller_boss_id,
                        principalTable: "Sellers",
                        principalColumn: "seller_id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_client_id = table.Column<int>(type: "int", nullable: false),
                    order_seller_id = table.Column<int>(type: "int", nullable: false),
                    order_parent_id = table.Column<int>(type: "int", nullable: true),
                    order_amount = table.Column<double>(type: "float", nullable: false),
                    order_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_order_client_id",
                        column: x => x.order_client_id,
                        principalTable: "Clients",
                        principalColumn: "client_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Orders_order_parent_id",
                        column: x => x.order_parent_id,
                        principalTable: "Orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Sellers_order_seller_id",
                        column: x => x.order_seller_id,
                        principalTable: "Sellers",
                        principalColumn: "seller_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_order_client_id",
                table: "Orders",
                column: "order_client_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_order_parent_id",
                table: "Orders",
                column: "order_parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_order_seller_id",
                table: "Orders",
                column: "order_seller_id");

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_seller_boss_id",
                table: "Sellers",
                column: "seller_boss_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Sellers");
        }
    }
}
