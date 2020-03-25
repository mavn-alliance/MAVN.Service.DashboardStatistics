using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.DashboardStatistics.MsSqlRepositories.Migrations
{
    public partial class CustomerStatisticIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "customer_id",
                schema: "dashboard_statistic",
                table: "customer_statistics",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_statistics_customer_id",
                schema: "dashboard_statistic",
                table: "customer_statistics",
                column: "customer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_customer_statistics_customer_id",
                schema: "dashboard_statistic",
                table: "customer_statistics");

            migrationBuilder.AlterColumn<string>(
                name: "customer_id",
                schema: "dashboard_statistic",
                table: "customer_statistics",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(36)");
        }
    }
}
