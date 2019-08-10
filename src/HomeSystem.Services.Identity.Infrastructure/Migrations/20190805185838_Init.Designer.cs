﻿// <auto-generated />
using HomeSystem.Services.Identity.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HomeSystem.Services.Identity.Infrastructure.Migrations
{
    [DbContext(typeof(IdentityDbContext))]
    [Migration("20190805185838_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HomeSystem.Services.Identity.Domain.Aggregates.OneTimeSecuredOperation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Consumed")
                        .HasColumnName("Consumed");

                    b.Property<DateTime?>("ConsumedAt")
                        .HasColumnName("ConsumedAt");

                    b.Property<string>("ConsumerIpAddress")
                        .HasColumnName("ConsumerIpAddress")
                        .HasMaxLength(50);

                    b.Property<string>("ConsumerUserAgent")
                        .HasColumnName("ConsumerUserAgent")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("CreatedAt");

                    b.Property<DateTime>("Expiry")
                        .HasColumnName("Expiry");

                    b.Property<string>("RequesterIpAddress")
                        .HasColumnName("RequesterIpAddress")
                        .HasMaxLength(50);

                    b.Property<string>("RequesterUserAgent")
                        .HasColumnName("RequesterUserAgent")
                        .HasMaxLength(50);

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnName("Token")
                        .HasMaxLength(500);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("Type")
                        .HasMaxLength(100);

                    b.Property<Guid>("UserId")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("OneTimeSecuredOperations");
                });

            modelBuilder.Entity("HomeSystem.Services.Identity.Domain.Aggregates.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasMaxLength(300);

                    b.Property<string>("FirstName")
                        .HasColumnName("FirstName")
                        .HasMaxLength(150);

                    b.Property<string>("LastName")
                        .HasColumnName("LastName")
                        .HasMaxLength(150);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("Password")
                        .HasMaxLength(1000);

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("PhoneNumber")
                        .HasMaxLength(12);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnName("Role");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnName("Salt")
                        .HasMaxLength(500);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnName("State")
                        .HasMaxLength(30);

                    b.Property<bool>("TwoFactorAuthentication")
                        .HasColumnName("TwoFactorAuthentication")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("UpdatedAt");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnName("Username")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HomeSystem.Services.Identity.Domain.Aggregates.UserSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("CreatedAt");

                    b.Property<bool>("Destroyed")
                        .HasColumnName("Destroyed")
                        .HasColumnType("bit");

                    b.Property<string>("IpAddress")
                        .HasColumnName("IpAddress")
                        .HasMaxLength(50);

                    b.Property<string>("Key")
                        .HasColumnName("Key")
                        .HasMaxLength(1000);

                    b.Property<Guid?>("ParentId")
                        .HasColumnName("ParentId");

                    b.Property<bool>("Refreshed")
                        .HasColumnName("Refreshed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("UpdatedAt");

                    b.Property<string>("UserAgent")
                        .HasColumnName("UserAgent")
                        .HasMaxLength(50);

                    b.Property<Guid>("UserId")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserSessions");
                });

            modelBuilder.Entity("HomeSystem.Services.Identity.Domain.Aggregates.OneTimeSecuredOperation", b =>
                {
                    b.HasOne("HomeSystem.Services.Identity.Domain.Aggregates.User", "User")
                        .WithMany("OneTimeSecuredOperations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HomeSystem.Services.Identity.Domain.Aggregates.User", b =>
                {
                    b.OwnsOne("HomeSystem.Services.Identity.Domain.ValueObjects.Avatar", "Avatar", b1 =>
                        {
                            b1.Property<Guid>("UserId");

                            b1.Property<bool>("IsEmpty");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(500);

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(2000);

                            b1.ToTable("Users");

                            b1.HasOne("HomeSystem.Services.Identity.Domain.Aggregates.User")
                                .WithOne("Avatar")
                                .HasForeignKey("HomeSystem.Services.Identity.Domain.ValueObjects.Avatar", "UserId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("HomeSystem.Services.Identity.Domain.ValueObjects.UserAddress", "Address", b1 =>
                        {
                            b1.Property<Guid>("UserId");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100);

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(90);

                            b1.Property<string>("State")
                                .HasMaxLength(60);

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(180);

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(18);

                            b1.ToTable("Users");

                            b1.HasOne("HomeSystem.Services.Identity.Domain.Aggregates.User")
                                .WithOne("Address")
                                .HasForeignKey("HomeSystem.Services.Identity.Domain.ValueObjects.UserAddress", "UserId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("HomeSystem.Services.Identity.Domain.Aggregates.UserSession", b =>
                {
                    b.HasOne("HomeSystem.Services.Identity.Domain.Aggregates.User", "User")
                        .WithMany("UserSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}