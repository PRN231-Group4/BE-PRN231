using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class InitalData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Category_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Category_Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Role_Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Supplier_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Supplier_Id);
                });

            migrationBuilder.CreateTable(
                name: "WineStorageLocation",
                columns: table => new
                {
                    Location_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorNumber = table.Column<int>(type: "int", nullable: true),
                    Zone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShelfCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WineStor__D2BA00E22F2DA2F2", x => x.Location_Id);
                });

            migrationBuilder.CreateTable(
                name: "Wine",
                columns: table => new
                {
                    Wine_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Id = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Volume = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    AlcContent = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wine", x => x.Wine_Id);
                    table.ForeignKey(
                        name: "FK__Wine__Category_I__2D27B809",
                        column: x => x.Category_Id,
                        principalTable: "Category",
                        principalColumn: "Category_Id");
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Account_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_Id = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Account_Id);
                    table.ForeignKey(
                        name: "FK__Account__Role_Id__267ABA7A",
                        column: x => x.Role_Id,
                        principalTable: "Role",
                        principalColumn: "Role_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WineImportRequest",
                columns: table => new
                {
                    Request_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Supplier_Id = table.Column<int>(type: "int", nullable: true),
                    Manager_Id = table.Column<int>(type: "int", nullable: true),
                    Wine_Id = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    RequestData = table.Column<DateTime>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WineImpo__E9C5B37369DF6DBB", x => x.Request_Id);
                    table.ForeignKey(
                        name: "FK__WineImpor__Suppl__300424B4",
                        column: x => x.Supplier_Id,
                        principalTable: "Supplier",
                        principalColumn: "Supplier_Id");
                    table.ForeignKey(
                        name: "FK__WineImpor__Wine___30F848ED",
                        column: x => x.Wine_Id,
                        principalTable: "Wine",
                        principalColumn: "Wine_Id");
                });

            migrationBuilder.CreateTable(
                name: "WineBatch",
                columns: table => new
                {
                    Batch_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Wine_Id = table.Column<int>(type: "int", nullable: true),
                    Request_Id = table.Column<int>(type: "int", nullable: true),
                    BatchNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImportDate = table.Column<DateTime>(type: "date", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    ProductionYear = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WineBatc__28E47C73F89C0FAE", x => x.Batch_Id);
                    table.ForeignKey(
                        name: "FK__WineBatch__Reque__34C8D9D1",
                        column: x => x.Request_Id,
                        principalTable: "WineImportRequest",
                        principalColumn: "Request_Id");
                    table.ForeignKey(
                        name: "FK__WineBatch__Wine___33D4B598",
                        column: x => x.Wine_Id,
                        principalTable: "Wine",
                        principalColumn: "Wine_Id");
                });

            migrationBuilder.CreateTable(
                name: "WineImportCheck",
                columns: table => new
                {
                    Check_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Request_Id = table.Column<int>(type: "int", nullable: true),
                    Batch_Id = table.Column<int>(type: "int", nullable: true),
                    Inspector_Id = table.Column<int>(type: "int", nullable: true),
                    Wine_Id = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CheckDate = table.Column<DateTime>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WineImpo__7063BF739B460838", x => x.Check_Id);
                    table.ForeignKey(
                        name: "FK__WineImpor__Batch__3F466844",
                        column: x => x.Batch_Id,
                        principalTable: "WineBatch",
                        principalColumn: "Batch_Id");
                    table.ForeignKey(
                        name: "FK__WineImpor__Reque__3E52440B",
                        column: x => x.Request_Id,
                        principalTable: "WineImportRequest",
                        principalColumn: "Request_Id");
                    table.ForeignKey(
                        name: "FK__WineImpor__Wine___403A8C7D",
                        column: x => x.Wine_Id,
                        principalTable: "Wine",
                        principalColumn: "Wine_Id");
                });

            migrationBuilder.CreateTable(
                name: "WineTransaction",
                columns: table => new
                {
                    Transaction_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Batch_Id = table.Column<int>(type: "int", nullable: true),
                    Wine_Id = table.Column<int>(type: "int", nullable: true),
                    Inspector_Id = table.Column<int>(type: "int", nullable: true),
                    Location_Id = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    TransType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransDate = table.Column<DateTime>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WineTran__9A8D5605092A147D", x => x.Transaction_Id);
                    table.ForeignKey(
                        name: "FK__WineTrans__Batch__398D8EEE",
                        column: x => x.Batch_Id,
                        principalTable: "WineBatch",
                        principalColumn: "Batch_Id");
                    table.ForeignKey(
                        name: "FK__WineTrans__Locat__3B75D760",
                        column: x => x.Location_Id,
                        principalTable: "WineStorageLocation",
                        principalColumn: "Location_Id");
                    table.ForeignKey(
                        name: "FK__WineTrans__Wine___3A81B327",
                        column: x => x.Wine_Id,
                        principalTable: "Wine",
                        principalColumn: "Wine_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Role_Id",
                table: "Account",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Wine_Category_Id",
                table: "Wine",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WineBatch_Request_Id",
                table: "WineBatch",
                column: "Request_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WineBatch_Wine_Id",
                table: "WineBatch",
                column: "Wine_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WineImportCheck_Batch_Id",
                table: "WineImportCheck",
                column: "Batch_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WineImportCheck_Request_Id",
                table: "WineImportCheck",
                column: "Request_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WineImportCheck_Wine_Id",
                table: "WineImportCheck",
                column: "Wine_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WineImportRequest_Supplier_Id",
                table: "WineImportRequest",
                column: "Supplier_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WineImportRequest_Wine_Id",
                table: "WineImportRequest",
                column: "Wine_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WineTransaction_Batch_Id",
                table: "WineTransaction",
                column: "Batch_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WineTransaction_Location_Id",
                table: "WineTransaction",
                column: "Location_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WineTransaction_Wine_Id",
                table: "WineTransaction",
                column: "Wine_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "WineImportCheck");

            migrationBuilder.DropTable(
                name: "WineTransaction");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "WineBatch");

            migrationBuilder.DropTable(
                name: "WineStorageLocation");

            migrationBuilder.DropTable(
                name: "WineImportRequest");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Wine");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
