using System.Data.Common;
using MAVN.Common.MsSql;
using MAVN.Service.DashboardStatistics.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories
{
    public class DashboardStatisticsContext : MsSqlContext
    {
        private const string Schema = "dashboard_statistic";

        public DashboardStatisticsContext()
            : base(Schema)
        {
        }

        public DashboardStatisticsContext(string connectionString, bool isTraceEnabled)
            : base(Schema, connectionString, isTraceEnabled)
        {
        }

        public DashboardStatisticsContext(DbConnection dbConnection)
            : base(Schema, dbConnection)
        {
        }

        public DbSet<CustomerActivityEntity> CustomerActivities { get; set; }

        public DbSet<CustomerStatisticEntity> CustomerStatistics { get; set; }

        public DbSet<LeadStatisticEntity> LeadStatistics { get; set; }

        protected override void OnLykkeConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnLykkeModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerStatisticEntity>()
                .HasIndex(c => c.CustomerId);
        }
    }
}
