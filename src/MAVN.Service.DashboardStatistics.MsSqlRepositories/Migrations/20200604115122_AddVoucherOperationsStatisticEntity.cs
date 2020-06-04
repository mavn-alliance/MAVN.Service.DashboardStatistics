using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories.Migrations
{
    public partial class AddVoucherOperationsStatisticEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "voucher_operations_statistics",
                schema: "dashboard_statistic",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    partner_id = table.Column<Guid>(nullable: false),
                    operation_type = table.Column<string>(nullable: false),
                    sum = table.Column<decimal>(nullable: false),
                    currency = table.Column<string>(nullable: true),
                    total_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voucher_operations_statistics", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_voucher_operations_statistics_partner_id_operation_type",
                schema: "dashboard_statistic",
                table: "voucher_operations_statistics",
                columns: new[] { "partner_id", "operation_type" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "voucher_operations_statistics",
                schema: "dashboard_statistic");
        }
    }
}
