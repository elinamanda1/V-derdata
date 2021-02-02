﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherData.Models;

namespace WeatherData.Migrations
{
    [DbContext(typeof(TDContext))]
    partial class TDContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("WeatherData.Models.Average", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AvgHum")
                        .HasColumnType("int");

                    b.Property<int?>("AvgHumInside")
                        .HasColumnType("int");

                    b.Property<double?>("AvgTemp")
                        .HasColumnType("float");

                    b.Property<double?>("AvgTempInside")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Averages");
                });

            modelBuilder.Entity("WeatherData.Models.MoldRisk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("MoldPercentage")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("MoldRisk");
                });

            modelBuilder.Entity("WeatherData.Models.Temperature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AverageId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Environment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Humidity")
                        .HasColumnType("int");

                    b.Property<double?>("Temp")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AverageId");

                    b.ToTable("Temperatures");
                });

            modelBuilder.Entity("WeatherData.Models.Temperature", b =>
                {
                    b.HasOne("WeatherData.Models.Average", "Average")
                        .WithMany()
                        .HasForeignKey("AverageId");

                    b.Navigation("Average");
                });
#pragma warning restore 612, 618
        }
    }
}
