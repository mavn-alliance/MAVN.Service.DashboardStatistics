using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dashboard_statistic");

            migrationBuilder.CreateTable(
                name: "lead_statistics",
                schema: "dashboard_statistic",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    lead_id = table.Column<string>(nullable: true),
                    time_stamp = table.Column<DateTime>(nullable: false),
                    state = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lead_statistics", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lead_statistics",
                schema: "dashboard_statistic");
        }
    }
}
