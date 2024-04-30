using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPIApp.Models;

public partial class AngularDbContext : DbContext
{
    public AngularDbContext()
    {
    }

    public AngularDbContext(DbContextOptions<AngularDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmployeeDetail> EmployeeDetails { get; set; }

    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<PolicyDetail> PolicyDetails { get; set; }

    public virtual DbSet<UserAuth> UserAuths { get; set; }

    public virtual DbSet<UserPolicy> UserPolicies { get; set; }

/*    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DATASCYLAB\\SQLEXPRESS;Database=angularDB;Integrated Security=True; TrustServerCertificate=True");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeDetail>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__16EBFA26B1EB44F7");

            entity.HasIndex(e => e.UserId, "UQ__Employee__F3BEEBFE68078D1A").IsUnique();

            entity.Property(e => e.EmpId).HasColumnName("EMP_ID");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("COMPANY_NAME");
            entity.Property(e => e.JoinDate)
                .HasColumnType("date")
                .HasColumnName("JOIN_DATE");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithOne(p => p.EmployeeDetail)
                .HasForeignKey<EmployeeDetail>(d => d.UserId)
                .HasConstraintName("FK__EmployeeD__USER___70DDC3D8");
        });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentD__3214EC077BEB1075");

            entity.Property(e => e.CardNumber)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CARD_NUMBER");
            entity.Property(e => e.CardOwnerName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CARD_OWNER_NAME");
            entity.Property(e => e.SecurityCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("SECURITY_CODE");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.ValidThrough)
                .HasColumnType("date")
                .HasColumnName("VALID_THROUGH");

            entity.HasOne(d => d.User).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentDe__USER___04E4BC85");
        });

        modelBuilder.Entity<PolicyDetail>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__PolicyDe__90D231B2DA57822F");

            entity.Property(e => e.PolicyId).HasColumnName("POLICY_ID");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.Insurer)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("INSURER");
            entity.Property(e => e.PolicyType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POLICY_TYPE");
        });

        modelBuilder.Entity<UserAuth>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PK__UserAuth__7B9E7F35D6426565");

            entity.ToTable("UserAuth");

            entity.HasIndex(e => e.Email, "UQ__UserAuth__161CF724B0016090").IsUnique();

            entity.Property(e => e.Userid).HasColumnName("USERID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
        });

        modelBuilder.Entity<UserPolicy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserPoli__3214EC27884F5104");

            entity.ToTable("UserPolicy");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("END_DATE");
            entity.Property(e => e.PolicyId).HasColumnName("POLICY_ID");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("START_DATE");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.Policy).WithMany(p => p.UserPolicies)
                .HasForeignKey(d => d.PolicyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserPolic__POLIC__02084FDA");

            entity.HasOne(d => d.User).WithMany(p => p.UserPolicies)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserPolic__USER___01142BA1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
