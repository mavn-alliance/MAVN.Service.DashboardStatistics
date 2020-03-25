using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.DashboardStatistics.MsSqlRepositories.Migrations
{
    public partial class CustomersActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "lead_id",
                schema: "dashboard_statistic",
                table: "lead_statistics",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "customer_id",
                schema: "dashboard_statistic",
                table: "customer_statistics",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(36)");

            migrationBuilder.CreateTable(
                name: "customer_activities",
                schema: "dashboard_statistic",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    customer_id = table.Column<Guid>(nullable: false),
                    activity_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_activities", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer_activities",
                schema: "dashboard_statistic");

            migrationBuilder.AlterColumn<string>(
                name: "lead_id",
                schema: "dashboard_statistic",
                table: "lead_statistics",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "customer_id",
                schema: "dashboard_statistic",
                table: "customer_statistics",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(Guid));
        }
    }
}
