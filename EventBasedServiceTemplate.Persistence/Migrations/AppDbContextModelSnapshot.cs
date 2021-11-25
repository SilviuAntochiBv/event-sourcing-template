﻿// <auto-generated />
using System;
using EventBasedServiceTemplate.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EventBasedServiceTemplate.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("EventBasedServiceTemplate.Domain.AggregateInformation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("Type")
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<int>("Version")
                        .HasColumnType("integer")
                        .HasColumnName("version");

                    b.HasKey("Id", "Type")
                        .HasName("pk_aggregates");

                    b.ToTable("aggregates");
                });

            modelBuilder.Entity("EventBasedServiceTemplate.Domain.DomainEvent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AggregateId")
                        .HasColumnType("text")
                        .HasColumnName("aggregate_id");

                    b.Property<string>("AggregateName")
                        .HasColumnType("text")
                        .HasColumnName("aggregate_name");

                    b.Property<DateTime>("OccurredAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("occurred_at");

                    b.Property<string>("RawEventData")
                        .HasColumnType("json")
                        .HasColumnName("raw_event_data");

                    b.Property<string>("Type")
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_events");

                    b.ToTable("events");
                });
#pragma warning restore 612, 618
        }
    }
}
