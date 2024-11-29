﻿// <auto-generated />
using System;
using Deliver.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Deliver.DAL.Migrations
{
    [DbContext(typeof(DeliverContext))]
    [Migration("20241128221318_addDeliverModel")]
    partial class addDeliverModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Deliver.DAL.Entities.DeiliverModel", b =>
                {
                    b.Property<int>("CourierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LicensePlate")
                        .HasColumnType("longtext");

                    b.Property<string>("VehicleType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CourierId");

                    b.ToTable("CourierModel");
                });

            modelBuilder.Entity("Deliver.DAL.Entities.DeliverSchedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourierId")
                        .HasColumnType("int");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time(6)");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time(6)");

                    b.HasKey("ScheduleId");

                    b.ToTable("CourierSchedules");
                });

            modelBuilder.Entity("Deliver.DAL.Entities.DeliverStats", b =>
                {
                    b.Property<int>("CourierStatsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<TimeSpan>("AverageTime")
                        .HasColumnType("time(6)");

                    b.Property<int>("CourierId")
                        .HasColumnType("int");

                    b.Property<int>("DeliveredOrders")
                        .HasColumnType("int");

                    b.Property<decimal>("Rating")
                        .HasColumnType("decimal(2, 1)");

                    b.HasKey("CourierStatsId");

                    b.ToTable("CourierStats");
                });
#pragma warning restore 612, 618
        }
    }
}
