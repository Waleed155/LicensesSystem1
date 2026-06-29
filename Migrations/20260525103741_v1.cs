using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licenses.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExcutivePositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcutivePositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotNum = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AreaName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NeighborhoodName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BuildingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileNumber = table.Column<int>(type: "int", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ExcutivePositionId = table.Column<int>(type: "int", nullable: false),
                    ActivityTypeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lots_ActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Lots_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Lots_ExcutivePositions_ExcutivePositionId",
                        column: x => x.ExcutivePositionId,
                        principalTable: "ExcutivePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "OrderSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepNumber = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    StepId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSteps_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrderSteps_Steps_StepId",
                        column: x => x.StepId,
                        principalTable: "Steps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LotOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartWorkingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    LotId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotOrders_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LotOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TransactionStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StageNumber = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionStages_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TransactionStages_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LotOrdersSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepStatus = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LotOrderId = table.Column<int>(type: "int", nullable: false),
                    StepOrderId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotOrdersSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotOrdersSteps_LotOrders_LotOrderId",
                        column: x => x.LotOrderId,
                        principalTable: "LotOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LotOrdersSteps_OrderSteps_StepOrderId",
                        column: x => x.StepOrderId,
                        principalTable: "OrderSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TransactionLotOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LotOrderId = table.Column<int>(type: "int", nullable: false),
                    TransactionStagesId = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionLotOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionLotOrders_LotOrders_LotOrderId",
                        column: x => x.LotOrderId,
                        principalTable: "LotOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TransactionLotOrders_TransactionStages_TransactionStagesId",
                        column: x => x.TransactionStagesId,
                        principalTable: "TransactionStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TransactionLotOrders_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CollectionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FeesTypes = table.Column<int>(type: "int", nullable: false),
                    TransactionLotOrderId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fees_TransactionLotOrders_TransactionLotOrderId",
                        column: x => x.TransactionLotOrderId,
                        principalTable: "TransactionLotOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TransactionLotOrderStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepStatus = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TransactionStagesId = table.Column<int>(type: "int", nullable: false),
                    TransactionLotOrderId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionLotOrderStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionLotOrderStages_TransactionLotOrders_TransactionLotOrderId",
                        column: x => x.TransactionLotOrderId,
                        principalTable: "TransactionLotOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TransactionLotOrderStages_TransactionStages_TransactionStagesId",
                        column: x => x.TransactionStagesId,
                        principalTable: "TransactionStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fees_TransactionLotOrderId",
                table: "Fees",
                column: "TransactionLotOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LotOrders_LotId",
                table: "LotOrders",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_LotOrders_OrderId",
                table: "LotOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LotOrdersSteps_LotOrderId",
                table: "LotOrdersSteps",
                column: "LotOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LotOrdersSteps_StepOrderId",
                table: "LotOrdersSteps",
                column: "StepOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_ActivityTypeId",
                table: "Lots",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_ClientId",
                table: "Lots",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_ExcutivePositionId",
                table: "Lots",
                column: "ExcutivePositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_LotNum_AreaName_NeighborhoodName",
                table: "Lots",
                columns: new[] { "LotNum", "AreaName", "NeighborhoodName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderSteps_OrderId",
                table: "OrderSteps",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSteps_StepId",
                table: "OrderSteps",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSteps_StepNumber",
                table: "OrderSteps",
                column: "StepNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLotOrders_LotOrderId",
                table: "TransactionLotOrders",
                column: "LotOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLotOrders_TransactionId",
                table: "TransactionLotOrders",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLotOrders_TransactionNumber",
                table: "TransactionLotOrders",
                column: "TransactionNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLotOrders_TransactionStagesId",
                table: "TransactionLotOrders",
                column: "TransactionStagesId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLotOrderStages_TransactionLotOrderId",
                table: "TransactionLotOrderStages",
                column: "TransactionLotOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLotOrderStages_TransactionStagesId",
                table: "TransactionLotOrderStages",
                column: "TransactionStagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OrderId",
                table: "Transactions",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionStages_StageId",
                table: "TransactionStages",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionStages_StageNumber",
                table: "TransactionStages",
                column: "StageNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionStages_TransactionId",
                table: "TransactionStages",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fees");

            migrationBuilder.DropTable(
                name: "LotOrdersSteps");

            migrationBuilder.DropTable(
                name: "TransactionLotOrderStages");

            migrationBuilder.DropTable(
                name: "OrderSteps");

            migrationBuilder.DropTable(
                name: "TransactionLotOrders");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "LotOrders");

            migrationBuilder.DropTable(
                name: "TransactionStages");

            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "ActivityTypes");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ExcutivePositions");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
