using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories.Migrations
{
    public partial class AddPartnerIdToCustomerStatisticsAndRemoveLeads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lead_statistics",
                schema: "dashboard_statistic");

            migrationBuilder.AddColumn<Guid>(
                name: "partner_id",
                schema: "dashboard_statistic",
                table: "customer_statistics",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_statistics_partner_id",
                schema: "dashboard_statistic",
                table: "customer_statistics",
                column: "partner_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_customer_statistics_partner_id",
                schema: "dashboard_statistic",
                table: "customer_statistics");

            migrationBuilder.DropColumn(
                name: "partner_id",
                schema: "dashboard_statistic",
                table: "customer_statistics");

            migrationBuilder.CreateTable(
                name: "lead_statistics",
                schema: "dashboard_statistic",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    lead_id = table.Column<Guid>(nullable: false),
                    state = table.Column<int>(nullable: false),
                    time_stamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lead_statistics", x => x.id);
                });
        }
    }
}
