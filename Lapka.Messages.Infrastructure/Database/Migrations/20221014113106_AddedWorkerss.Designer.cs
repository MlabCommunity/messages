﻿// <auto-generated />
using System;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lapka.Messages.Infrastructure.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221014113106_AddedWorkerss")]
    partial class AddedWorkerss
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("messages")
                .HasAnnotation("ProductVersion", "7.0.0-rc.1.22426.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AppUserRoom", b =>
                {
                    b.Property<Guid>("AppUsersUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoomsRoomId")
                        .HasColumnType("uuid");

                    b.HasKey("AppUsersUserId", "RoomsRoomId");

                    b.HasIndex("RoomsRoomId");

                    b.ToTable("AppUserRoom", "messages");
                });

            modelBuilder.Entity("Lapka.Messages.Core.Entities.AppUser", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("boolean");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.ToTable("AppUsers", "messages");
                });

            modelBuilder.Entity("Lapka.Messages.Core.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsUnread")
                        .HasColumnType("boolean");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Messages", "messages");
                });

            modelBuilder.Entity("Lapka.Messages.Core.Entities.Room", b =>
                {
                    b.Property<Guid>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("RoomId");

                    b.ToTable("Rooms", "messages");
                });

            modelBuilder.Entity("Lapka.Messages.Core.Entities.Worker", b =>
                {
                    b.Property<Guid>("WorkerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ShelterId")
                        .HasColumnType("uuid");

                    b.HasKey("WorkerId");

                    b.ToTable("Workers", "messages");
                });

            modelBuilder.Entity("AppUserRoom", b =>
                {
                    b.HasOne("Lapka.Messages.Core.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("AppUsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lapka.Messages.Core.Entities.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomsRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lapka.Messages.Core.Entities.Message", b =>
                {
                    b.HasOne("Lapka.Messages.Core.Entities.Room", "Room")
                        .WithMany("Messages")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Lapka.Messages.Core.Entities.Room", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
