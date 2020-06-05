using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories.Migrations
{
    public partial class ExtendActivityEntityWithPartnerIdAndType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "activity_type",
                schema: "dashboard_statistic",
                table: "customer_activities",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "partner_id",
                schema: "dashboard_statistic",
                table: "customer_activities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "activity_type",
                schema: "dashboard_statistic",
                table: "customer_activities");

            migrationBuilder.DropColumn(
                name: "partner_id",
                schema: "dashboard_statistic",
                table: "customer_activities");
        }
    }
}
