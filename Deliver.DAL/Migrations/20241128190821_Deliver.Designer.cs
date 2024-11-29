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
    [Migration("20241128190821_Deliver")]
    partial class Deliver
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Deliver.DAL.Entities.CourierSchedule", b =>
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

            modelBuilder.Entity("Deliver.DAL.Entities.CourierStats", b =>
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