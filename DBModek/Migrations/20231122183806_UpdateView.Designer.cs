﻿// <auto-generated />
using System;
using DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DBModels.Migrations
{
    [DbContext(typeof(MaterialsSuplyContext))]
    [Migration("20231122183806_UpdateView")]
    partial class UpdateView
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourceProject.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

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

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UserOrSuperUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("DBModels.ActualyDeliveryViewTable", b =>
                {
                    b.Property<double?>("DeliveredForQuartal")
                        .HasColumnType("float");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int")
                        .HasColumnName("MaterialID");

                    b.Property<int?>("QuarterOfDelivery")
                        .HasColumnType("int");

                    b.Property<double?>("UsedDeliveredForQuartal")
                        .HasColumnType("float");

                    b.Property<int?>("YearOfDelivery")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("ActualyDeliveryViewTable", (string)null);
                });

            modelBuilder.Entity("DBModels.DeliveredResource", b =>
                {
                    b.Property<int>("DeliveredResourcesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DeliveredResourcesID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeliveredResourcesId"));

                    b.Property<int?>("QuarterOfDelivery")
                        .HasColumnType("int");

                    b.Property<double?>("SizeOfResourseUsed")
                        .HasColumnType("float");

                    b.Property<int?>("SupplyContractsId")
                        .HasColumnType("int")
                        .HasColumnName("SupplyContractsID");

                    b.Property<int?>("YearOfDelivery")
                        .HasColumnType("int");

                    b.HasKey("DeliveredResourcesId")
                        .HasName("PK__Delivere__75B6C0E2D8EB7EAA");

                    b.HasIndex("SupplyContractsId");

                    b.ToTable("DeliveredResources");
                });

            modelBuilder.Entity("DBModels.Employe", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("FirstName")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .IsFixedLength();

                    b.Property<string>("LastName")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .IsFixedLength();

                    b.Property<string>("Post")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.HasKey("EmployeeId")
                        .HasName("PK__Employes__7AD04FF1B54BBD2E");

                    b.ToTable("Employes");
                });

            modelBuilder.Entity("DBModels.EmployesWhoCantCompleteContractToTime", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    b.Property<string>("LastName")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .IsFixedLength();

                    b.Property<string>("Post")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.Property<int>("SupplyContractsId")
                        .HasColumnType("int")
                        .HasColumnName("SupplyContractsID");

                    b.ToTable((string)null);

                    b.ToView("EmployesWhoCantCompleteContractToTime", (string)null);
                });

            modelBuilder.Entity("DBModels.EmployesWhoCompleteContractToTime", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    b.Property<string>("LastName")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .IsFixedLength();

                    b.Property<string>("Post")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.Property<int>("SupplyContractsId")
                        .HasColumnType("int")
                        .HasColumnName("SupplyContractsID");

                    b.ToTable((string)null);

                    b.ToView("EmployesWhoCompleteContractToTime", (string)null);
                });

            modelBuilder.Entity("DBModels.Material", b =>
                {
                    b.Property<int>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MaterialID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaterialId"));

                    b.Property<string>("Characteristics")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.Property<string>("MaterialType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MeasureOfMeasurement")
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength();

                    b.Property<string>("NameOfStateStandart")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.Property<string>("StateStandart")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.HasKey("MaterialId")
                        .HasName("PK__Material__C50613175A790854");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("DBModels.RemaningResourcesViewTable", b =>
                {
                    b.Property<int>("MaterialId")
                        .HasColumnType("int")
                        .HasColumnName("MaterialID");

                    b.Property<double?>("RemaningResources")
                        .HasColumnType("float");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("RemaningResourcesViewTable", (string)null);
                });

            modelBuilder.Entity("DBModels.RequiredResource", b =>
                {
                    b.Property<int>("RequiredResourcesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RequiredResourcesID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequiredResourcesId"));

                    b.Property<int?>("MaterialId")
                        .HasColumnType("int")
                        .HasColumnName("MaterialID");

                    b.Property<int?>("Quarter")
                        .HasColumnType("int");

                    b.Property<double?>("SizeOfResurces")
                        .HasColumnType("float");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("RequiredResourcesId")
                        .HasName("PK__Required__4136AC8570C9C0FF");

                    b.HasIndex("MaterialId");

                    b.ToTable("RequiredResources");
                });

            modelBuilder.Entity("DBModels.SupplyContract", b =>
                {
                    b.Property<int>("SupplyContractsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SupplyContractsID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplyContractsId"));

                    b.Property<DateTime?>("DateOfConclusion")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DateOfDiliver")
                        .HasColumnType("date");

                    b.Property<double?>("DiliverySize")
                        .HasColumnType("float");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    b.Property<int?>("MaterialId")
                        .HasColumnType("int")
                        .HasColumnName("MaterialID");

                    b.Property<string>("Supplyer")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.HasKey("SupplyContractsId")
                        .HasName("PK__SupplyCo__1B36F175AED85B27");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("MaterialId");

                    b.ToTable("SupplyContracts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DBModels.DeliveredResource", b =>
                {
                    b.HasOne("DBModels.SupplyContract", "SupplyContracts")
                        .WithMany("DeliveredResources")
                        .HasForeignKey("SupplyContractsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_DeliveredResources_SupplyContracts");

                    b.Navigation("SupplyContracts");
                });

            modelBuilder.Entity("DBModels.RequiredResource", b =>
                {
                    b.HasOne("DBModels.Material", "Material")
                        .WithMany("RequiredResources")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_RequiredResources_Materials");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("DBModels.SupplyContract", b =>
                {
                    b.HasOne("DBModels.Employe", "Employee")
                        .WithMany("SupplyContracts")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_SupplyContracts_Employes");

                    b.HasOne("DBModels.Material", "Material")
                        .WithMany("SupplyContracts")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_SupplyContracts_Materials");

                    b.Navigation("Employee");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CourceProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CourceProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourceProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CourceProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DBModels.Employe", b =>
                {
                    b.Navigation("SupplyContracts");
                });

            modelBuilder.Entity("DBModels.Material", b =>
                {
                    b.Navigation("RequiredResources");

                    b.Navigation("SupplyContracts");
                });

            modelBuilder.Entity("DBModels.SupplyContract", b =>
                {
                    b.Navigation("DeliveredResources");
                });
#pragma warning restore 612, 618
        }
    }
}
