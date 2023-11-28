using System;
using System.Collections.Generic;
using CourceProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DBModels;

public partial class MaterialsSuplyContext : IdentityDbContext<User>
{
    public MaterialsSuplyContext()
    {
    }

    public MaterialsSuplyContext(DbContextOptions<MaterialsSuplyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActualyDeliveryViewTable> ActualyDeliveryViewTables { get; set; }

    public virtual DbSet<DeliveredResource> DeliveredResources { get; set; }

    public virtual DbSet<Employe> Employes { get; set; }

    public virtual DbSet<EmployesWhoCantCompleteContractToTime> EmployesWhoCantCompleteContractToTimes { get; set; }

    public virtual DbSet<EmployesWhoCompleteContractToTime> EmployesWhoCompleteContractToTimes { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<RemaningResourcesViewTable> RemaningResourcesViewTables { get; set; }

    public virtual DbSet<RequiredResource> RequiredResources { get; set; }

    public virtual DbSet<SupplyContract> SupplyContracts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-J7VJ20H\\SQLEXPRESS;Database=materials_suply;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ActualyDeliveryViewTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ActualyDeliveryViewTable");

            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
        });

        modelBuilder.Entity<DeliveredResource>(entity =>
        {
            entity.HasKey(e => e.DeliveredResourcesId).HasName("PK__Delivere__75B6C0E2D8EB7EAA");

            entity.Property(e => e.DeliveredResourcesId).HasColumnName("DeliveredResourcesID");
            entity.Property(e => e.SupplyContractsId).HasColumnName("SupplyContractsID");

            entity.HasOne(d => d.SupplyContracts).WithMany(p => p.DeliveredResources)
                .HasForeignKey(d => d.SupplyContractsId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DeliveredResources_SupplyContracts");
        });

        modelBuilder.Entity<Employe>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employes__7AD04FF1B54BBD2E");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.Post)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<EmployesWhoCantCompleteContractToTime>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("EmployesWhoCantCompleteContractToTime");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.Post)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.SupplyContractsId).HasColumnName("SupplyContractsID");
        });

        modelBuilder.Entity<EmployesWhoCompleteContractToTime>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("EmployesWhoCompleteContractToTime");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.Post)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.SupplyContractsId).HasColumnName("SupplyContractsID");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__Material__C50613175A790854");

            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
            entity.Property(e => e.Characteristics)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.MaterialType).HasMaxLength(50);
            entity.Property(e => e.MeasureOfMeasurement)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.NameOfStateStandart)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.StateStandart)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<RemaningResourcesViewTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RemaningResourcesViewTable");

            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
        });

        modelBuilder.Entity<RequiredResource>(entity =>
        {
            entity.HasKey(e => e.RequiredResourcesId).HasName("PK__Required__4136AC8570C9C0FF");

            entity.Property(e => e.RequiredResourcesId).HasColumnName("RequiredResourcesID");
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");

            entity.HasOne(d => d.Material).WithMany(p => p.RequiredResources)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RequiredResources_Materials");
        });

        modelBuilder.Entity<SupplyContract>(entity =>
        {
            entity.HasKey(e => e.SupplyContractsId).HasName("PK__SupplyCo__1B36F175AED85B27");

            entity.Property(e => e.SupplyContractsId).HasColumnName("SupplyContractsID");
            entity.Property(e => e.DateOfConclusion).HasColumnType("date");
            entity.Property(e => e.DateOfDiliver).HasColumnType("date");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
            entity.Property(e => e.Supplyer)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.Employee).WithMany(p => p.SupplyContracts)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SupplyContracts_Employes");

            entity.HasOne(d => d.Material).WithMany(p => p.SupplyContracts)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SupplyContracts_Materials");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
