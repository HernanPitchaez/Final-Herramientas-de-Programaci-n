﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Proyecto_ClubDeportes.Migrations
{
    [DbContext(typeof(ClubContext))]
    [Migration("20231217005550_add_membership")]
    partial class add_membership
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("Practice", b =>
                {
                    b.Property<int>("PartnersId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SportsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PartnersId", "SportsId");

                    b.HasIndex("SportsId");

                    b.ToTable("Practice");
                });

            modelBuilder.Entity("Proyecto_ClubDeportes.Models.IncomeRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MembershipFee")
                        .HasColumnType("TEXT");

                    b.Property<int>("PartnerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PaymentType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReceiptNumber")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PartnerId");

                    b.ToTable("IncomeRecord");
                });

            modelBuilder.Entity("Proyecto_ClubDeportes.Models.Membership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Value")
                        .HasColumnType("TEXT");

                    b.Property<string>("membershipType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Membership");
                });

            modelBuilder.Entity("Proyecto_ClubDeportes.Models.Partner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberPhone")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Years")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Partner");
                });

            modelBuilder.Entity("Proyecto_ClubDeportes.Models.Sport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Activity")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sport");
                });

            modelBuilder.Entity("RecordSport", b =>
                {
                    b.Property<int>("IncomeRecordsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SportsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("IncomeRecordsId", "SportsId");

                    b.HasIndex("SportsId");

                    b.ToTable("RecordSport");
                });

            modelBuilder.Entity("Practice", b =>
                {
                    b.HasOne("Proyecto_ClubDeportes.Models.Partner", null)
                        .WithMany()
                        .HasForeignKey("PartnersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proyecto_ClubDeportes.Models.Sport", null)
                        .WithMany()
                        .HasForeignKey("SportsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Proyecto_ClubDeportes.Models.IncomeRecord", b =>
                {
                    b.HasOne("Proyecto_ClubDeportes.Models.Partner", "Partner")
                        .WithMany("IncomeRecords")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("RecordSport", b =>
                {
                    b.HasOne("Proyecto_ClubDeportes.Models.IncomeRecord", null)
                        .WithMany()
                        .HasForeignKey("IncomeRecordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proyecto_ClubDeportes.Models.Sport", null)
                        .WithMany()
                        .HasForeignKey("SportsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Proyecto_ClubDeportes.Models.Partner", b =>
                {
                    b.Navigation("IncomeRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
