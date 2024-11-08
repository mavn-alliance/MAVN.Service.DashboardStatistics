﻿// <auto-generated />
using System;
using MAVN.Service.DashboardStatistics.MsSqlRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories.Migrations
{
    [DbContext(typeof(DashboardStatisticsContext))]
    [Migration("20200702075302_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dashboard_statistic")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("MAVN.Service.DashboardStatistics.MsSqlRepositories.Entities.CustomerActivityEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ActivityDate")
                        .HasColumnName("activity_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("ActivityType")
                        .HasColumnName("activity_type")
                        .HasColumnType("integer");

                    b.Property<Guid>("CustomerId")
                        .HasColumnName("customer_id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PartnerId")
                        .HasColumnName("partner_id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("customer_activities");
                });

            modelBuilder.Entity("MAVN.Service.DashboardStatistics.MsSqlRepositories.Entities.CustomerStatisticEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnName("customer_id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PartnerId")
                        .HasColumnName("partner_id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnName("time_stamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PartnerId");

                    b.ToTable("customer_statistics");
                });

            modelBuilder.Entity("MAVN.Service.DashboardStatistics.MsSqlRepositories.Entities.PartnerVouchersDailyStatsEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnName("currency")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("OperationType")
                        .IsRequired()
                        .HasColumnName("operation_type")
                        .HasColumnType("text");

                    b.Property<Guid>("PartnerId")
                        .HasColumnName("partner_id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Sum")
                        .HasColumnName("sum")
                        .HasColumnType("numeric");

                    b.Property<int>("TotalCount")
                        .HasColumnName("total_count")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PartnerId", "OperationType");

                    b.ToTable("partner_vouchers_daily_stats");
                });

            modelBuilder.Entity("MAVN.Service.DashboardStatistics.MsSqlRepositories.Entities.VoucherOperationsStatisticsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Currency")
                        .HasColumnName("currency")
                        .HasColumnType("text");

                    b.Property<string>("OperationType")
                        .IsRequired()
                        .HasColumnName("operation_type")
                        .HasColumnType("text");

                    b.Property<Guid>("PartnerId")
                        .HasColumnName("partner_id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Sum")
                        .HasColumnName("sum")
                        .HasColumnType("numeric");

                    b.Property<int>("TotalCount")
                        .HasColumnName("total_count")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PartnerId", "OperationType");

                    b.ToTable("voucher_operations_statistics");
                });
#pragma warning restore 612, 618
        }
    }
}