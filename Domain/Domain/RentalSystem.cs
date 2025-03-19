using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Domain.Domain;

public partial class RentalSystem : DbContext
{
    public RentalSystem()
    {
    }

    public RentalSystem(DbContextOptions<RentalSystem> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }

    public virtual DbSet<RentalDetail> RentalDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MUSSTTAPC\\SQLEXPRESS;Database=RentalSystem;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PK__Client__668DFF3FF77F33B0");

            entity.ToTable("Client");

            entity.HasIndex(e => e.Email, "UQ__Client__A9D105343884567E").IsUnique();

            entity.Property(e => e.IdClient).HasColumnName("Id_Client");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("Last_Name");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("PK__Employee__938D4B10FD1DDE3F");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D10534CB78EE87").IsUnique();

            entity.Property(e => e.IdEmployee).HasColumnName("Id_Employee");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("Last_Name");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.SalaryPerEvent)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Salary_Per_Event");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.IdEquipment).HasName("PK__Equipmen__8301C707D5AEB7A8");

            entity.Property(e => e.IdEquipment).HasColumnName("Id_Equipment");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.DailyRate)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Daily_Rate");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.EquipmentName)
                .HasMaxLength(100)
                .HasColumnName("Equipment_Name");
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.IdEvent).HasName("PK__Evento__0DBEC62A4C20923B");

            entity.ToTable("Evento");

            entity.Property(e => e.IdEvent).HasColumnName("Id_Event");
            entity.Property(e => e.EndDate).HasColumnName("End_Date");
            entity.Property(e => e.EventName)
                .HasMaxLength(100)
                .HasColumnName("Event_Name");
            entity.Property(e => e.IdClient).HasColumnName("Id_Client");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.StartDate).HasColumnName("Start_Date");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK__Evento__Id_Clien__3A81B327");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.IdPayment).HasName("PK__Payment__17C40C66F7628A14");

            entity.ToTable("Payment");

            entity.Property(e => e.IdPayment).HasColumnName("Id_Payment");
            entity.Property(e => e.AmountPaid)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Amount_Paid");
            entity.Property(e => e.IdRental).HasColumnName("Id_Rental");
            entity.Property(e => e.PaymentDate).HasColumnName("Payment_Date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("Payment_Method");

            entity.HasOne(d => d.IdRentalNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.IdRental)
                .HasConstraintName("FK__Payment__Id_Rent__45F365D3");
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.IdRental).HasName("PK__Rental__5ACC9D274D43AC2F");

            entity.ToTable("Rental");

            entity.Property(e => e.IdRental).HasColumnName("Id_Rental");
            entity.Property(e => e.IdEvent).HasColumnName("Id_Event");
            entity.Property(e => e.RentalDate).HasColumnName("Rental_Date");
            entity.Property(e => e.ReturnDate).HasColumnName("Return_Date");
            entity.Property(e => e.TotalCost)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Total_Cost");

            entity.HasOne(d => d.IdEventNavigation).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.IdEvent)
                .HasConstraintName("FK__Rental__Id_Event__3D5E1FD2");
        });

        modelBuilder.Entity<RentalDetail>(entity =>
        {
            entity.HasKey(e => e.IdDetail).HasName("PK__Rental_D__280A4C9B789BBACF");

            entity.ToTable("Rental_Detail");

            entity.Property(e => e.IdDetail).HasColumnName("Id_Detail");
            entity.Property(e => e.IdEquipment).HasColumnName("Id_Equipment");
            entity.Property(e => e.IdRental).HasColumnName("Id_Rental");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdEquipmentNavigation).WithMany(p => p.RentalDetails)
                .HasForeignKey(d => d.IdEquipment)
                .HasConstraintName("FK__Rental_De__Id_Eq__4316F928");

            entity.HasOne(d => d.IdRentalNavigation).WithMany(p => p.RentalDetails)
                .HasForeignKey(d => d.IdRental)
                .HasConstraintName("FK__Rental_De__Id_Re__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
