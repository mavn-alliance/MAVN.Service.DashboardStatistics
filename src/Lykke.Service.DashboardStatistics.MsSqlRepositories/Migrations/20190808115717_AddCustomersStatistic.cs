using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.DashboardStatistics.MsSqlRepositories.Migrations
{
    public partial class AddCustomersStatistic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer_statistics",
                schema: "dashboard_statistic",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    customer_id = table.Column<string>(nullable: true),
                    time_stamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_statistics", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer_statistics",
                schema: "dashboard_statistic");
        }
    }
}
