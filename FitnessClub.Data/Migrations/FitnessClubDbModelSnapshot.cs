﻿// <auto-generated />
using System;
using FitnessClub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FitnessClub.Data.Migrations
{
    [DbContext(typeof(FitnessClubDb))]
    partial class FitnessClubDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FitnessClub.Data.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BirthDay")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("FitnessClubId")
                        .HasColumnType("int");

                    b.Property<string>("Membership")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("TypeOfMembershipId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FitnessClubId");

                    b.HasIndex("TypeOfMembershipId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("FitnessClub.Data.FitnessClub", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("FitnessClubs");
                });

            modelBuilder.Entity("FitnessClub.Data.TypeOfMembership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Expired")
                        .HasColumnType("date");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.Property<decimal>("Money")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("EmployeeTypes");
                });

            modelBuilder.Entity("FitnessClub.Data.Client", b =>
                {
                    b.HasOne("FitnessClub.Data.FitnessClub", "FitnessClub")
                        .WithMany("Clients")
                        .HasForeignKey("FitnessClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitnessClub.Data.TypeOfMembership", "TypeOfMembership")
                        .WithMany("Clients")
                        .HasForeignKey("TypeOfMembershipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FitnessClub");

                    b.Navigation("TypeOfMembership");
                });

            modelBuilder.Entity("FitnessClub.Data.FitnessClub", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("FitnessClub.Data.TypeOfMembership", b =>
                {
                    b.Navigation("Clients");
                });
#pragma warning restore 612, 618
        }
    }
}
