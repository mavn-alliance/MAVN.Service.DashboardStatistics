using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dashboard_statistic");

            migrationBuilder.CreateTable(
                name: "customer_activities",
                schema: "dashboard_statistic",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    customer_id = table.Column<Guid>(nullable: false),
                    activity_date = table.Column<DateTime>(nullable: false),
                    partner_id = table.Column<Guid>(nullable: true),
                    activity_type = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_activities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer_statistics",
                schema: "dashboard_statistic",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    customer_id = table.Column<Guid>(nullable: false),
                    partner_id = table.Column<Guid>(nullable: true),
                    time_stamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_statistics", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "partner_vouchers_daily_stats",
                schema: "dashboard_statistic",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    partner_id = table.Column<Guid>(nullable: false),
                    operation_type = table.Column<string>(nullable: false),
                    sum = table.Column<decimal>(nullable: false),
                    currency = table.Column<string>(nullable: false),
                    total_count = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partner_vouchers_daily_stats", x => x.id);
                });

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
                name: "IX_customer_statistics_customer_id",
                schema: "dashboard_statistic",
                table: "customer_statistics",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_statistics_partner_id",
                schema: "dashboard_statistic",
                table: "customer_statistics",
                column: "partner_id");

            migrationBuilder.CreateIndex(
                name: "IX_partner_vouchers_daily_stats_partner_id_operation_type",
                schema: "dashboard_statistic",
                table: "partner_vouchers_daily_stats",
                columns: new[] { "partner_id", "operation_type" });

            migrationBuilder.CreateIndex(
                name: "IX_voucher_operations_statistics_partner_id_operation_type",
                schema: "dashboard_statistic",
                table: "voucher_operations_statistics",
                columns: new[] { "partner_id", "operation_type" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer_activities",
                schema: "dashboard_statistic");

            migrationBuilder.DropTable(
                name: "customer_statistics",
                schema: "dashboard_statistic");

            migrationBuilder.DropTable(
                name: "partner_vouchers_daily_stats",
                schema: "dashboard_statistic");

            migrationBuilder.DropTable(
                name: "voucher_operations_statistics",
                schema: "dashboard_statistic");
        }
    }
}
