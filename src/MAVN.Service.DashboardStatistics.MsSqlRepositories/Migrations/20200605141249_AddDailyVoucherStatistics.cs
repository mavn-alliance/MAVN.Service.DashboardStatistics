using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories.Migrations
{
    public partial class AddDailyVoucherStatistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "partner_vouchers_daily_stats",
                schema: "dashboard_statistic",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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

            migrationBuilder.CreateIndex(
                name: "IX_partner_vouchers_daily_stats_partner_id_operation_type",
                schema: "dashboard_statistic",
                table: "partner_vouchers_daily_stats",
                columns: new[] { "partner_id", "operation_type" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "partner_vouchers_daily_stats",
                schema: "dashboard_statistic");
        }
    }
}
