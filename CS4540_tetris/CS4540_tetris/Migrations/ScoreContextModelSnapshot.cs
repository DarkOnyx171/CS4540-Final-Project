﻿// <auto-generated />
using System;
using CS4540_tetris.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CS4540_tetris.Migrations
{
    [DbContext(typeof(ScoreContext))]
    partial class ScoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CS4540_tetris.Areas.Identity.Data.GameUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NickName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameUser");
                });

            modelBuilder.Entity("CS4540_tetris.Models.GameLog", b =>
                {
                    b.Property<int>("GameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("GameTime")
                        .HasColumnType("time");

                    b.Property<int>("Mode")
                        .HasColumnType("int");

                    b.Property<string>("Player")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("GameID");

                    b.ToTable("GameLogs");
                });

            modelBuilder.Entity("CS4540_tetris.Models.MultiPlayerLog", b =>
                {
                    b.Property<int>("MultiPlayerLogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GameLogOneGameID")
                        .HasColumnType("int");

                    b.Property<int?>("GameLogTwoGameID")
                        .HasColumnType("int");

                    b.Property<int>("GameOneID")
                        .HasColumnType("int");

                    b.Property<int>("GameTwoID")
                        .HasColumnType("int");

                    b.HasKey("MultiPlayerLogID");

                    b.HasIndex("GameLogOneGameID");

                    b.HasIndex("GameLogTwoGameID");

                    b.ToTable("MultiPlayerLogs");
                });

            modelBuilder.Entity("CS4540_tetris.Models.PlayerStats", b =>
                {
                    b.Property<int>("PlayerStatsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GamesPlayed")
                        .HasColumnType("int");

                    b.Property<int>("HighestScore")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastGameDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("LongestGame")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("TotalTimePlayed")
                        .HasColumnType("time");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayerStatsID");

                    b.HasIndex("UserId");

                    b.ToTable("PlayerStats");
                });

            modelBuilder.Entity("CS4540_tetris.Models.Score", b =>
                {
                    b.Property<int>("ScoreID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GameMode")
                        .HasColumnType("int");

                    b.Property<string>("Nickname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("ScoreID");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("CS4540_tetris.Models.StatNotes", b =>
                {
                    b.Property<int>("NoteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Liked")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatID")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NoteID");

                    b.HasIndex("StatID")
                        .IsUnique();

                    b.ToTable("StatNotes");
                });

            modelBuilder.Entity("CS4540_tetris.Models.MultiPlayerLog", b =>
                {
                    b.HasOne("CS4540_tetris.Models.GameLog", "GameLogOne")
                        .WithMany()
                        .HasForeignKey("GameLogOneGameID");

                    b.HasOne("CS4540_tetris.Models.GameLog", "GameLogTwo")
                        .WithMany()
                        .HasForeignKey("GameLogTwoGameID");
                });

            modelBuilder.Entity("CS4540_tetris.Models.PlayerStats", b =>
                {
                    b.HasOne("CS4540_tetris.Areas.Identity.Data.GameUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CS4540_tetris.Models.StatNotes", b =>
                {
                    b.HasOne("CS4540_tetris.Models.PlayerStats", "Stat")
                        .WithOne("Note")
                        .HasForeignKey("CS4540_tetris.Models.StatNotes", "StatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
