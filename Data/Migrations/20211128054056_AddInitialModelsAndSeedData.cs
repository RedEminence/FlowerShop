using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShop.Data.Migrations
{
    public partial class AddInitialModelsAndSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Bouquets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bouquets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flowers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flowers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowerBouquet",
                columns: table => new
                {
                    FlowerId = table.Column<int>(type: "int", nullable: false),
                    BouquetId = table.Column<int>(type: "int", nullable: false),
                    FlowerCount = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerBouquet", x => new { x.FlowerId, x.BouquetId });
                    table.ForeignKey(
                        name: "FK_FlowerBouquet_Bouquets_BouquetId",
                        column: x => x.BouquetId,
                        principalTable: "Bouquets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowerBouquet_Flowers_FlowerId",
                        column: x => x.FlowerId,
                        principalTable: "Flowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliverAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BouquetId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Bouquets_BouquetId",
                        column: x => x.BouquetId,
                        principalTable: "Bouquets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c5e174e-3b0e-446f-86af-483d56fd7210", null, "Client", "CLIENT" },
                    { "2c5e174e-3b0e-446f-86af-483d56fd7320", null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e1746ac3-bc36-4296-9993-644ec1894298", 0, null, "User", "manager@example.com", true, true, null, "MANAGER@EXAMPLE.COM", "MANAGER@EXAMPLE.COM", "AQAAAAEAACcQAAAAEB/56cHKJe3B55eGS70Un+KBcOiyMGxrNhLo6yMfLb3KDEwAVgQh9LQQ6+/sulZkTw==", null, false, "6275F5YJHD32NVDF7R3BD2MEENA3V5GO", false, "Пользователь-менеджер" });

            migrationBuilder.InsertData(
                table: "Bouquets",
                columns: new[] { "Id", "Description", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Описание букета Вдохновение", "vdohnovenie.jpg", "Вдохновение", 100m },
                    { 2, "Описание букета Нежность", "ocharovanie.jpg", "Нежность", 200m },
                    { 3, "Описание букета Очарование", "nezhnost.jpg", "Очарование", 300m }
                });

            migrationBuilder.InsertData(
                table: "Flowers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Роза" },
                    { 2, "Хризантема" },
                    { 3, "Орхидея" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Принят" },
                    { 2, "В работе" },
                    { 3, "Завершен" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7320", "e1746ac3-bc36-4296-9993-644ec1894298" });

            migrationBuilder.InsertData(
                table: "FlowerBouquet",
                columns: new[] { "BouquetId", "FlowerId", "FlowerCount" },
                values: new object[,]
                {
                    { 1, 1, 3 },
                    { 2, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 2, 2 },
                    { 2, 3, 1 },
                    { 3, 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowerBouquet_BouquetId",
                table: "FlowerBouquet",
                column: "BouquetId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BouquetId",
                table: "Orders",
                column: "BouquetId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowerBouquet");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Flowers");

            migrationBuilder.DropTable(
                name: "Bouquets");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7320", "e1746ac3-bc36-4296-9993-644ec1894298" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7320");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1746ac3-bc36-4296-9993-644ec1894298");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
