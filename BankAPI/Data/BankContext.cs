﻿using System;
using System.Collections.Generic;
using BankAPI.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Data;

public partial class BankContext : DbContext
{
    public BankContext()
    {
    }

    public BankContext(DbContextOptions<BankContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<BankTransaction> BankTransactions { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Bank;Trusted_connection=true;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC2766EB4E41");

            entity.ToTable("Account");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.RegDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AccountTypeNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.AccountType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Account__Account__52593CB8");

            entity.HasOne(d => d.Client).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Account__ClientI__534D60F1");
        });

        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AccountT__3214EC273F74A7C4");

            entity.ToTable("AccountType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RegDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Administ__3214EC277333C38B");

            entity.ToTable("Administrator");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AdminType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Pwd)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<BankTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BankTran__3214EC275304BEB0");

            entity.ToTable("BankTransaction");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RegDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.BankTransactions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BankTrans__Accou__571DF1D5");

            entity.HasOne(d => d.TransactionTypeNavigation).WithMany(p => p.BankTransactions)
                .HasForeignKey(d => d.TransactionType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BankTrans__Trans__5812160E");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Client__3214EC2727835195");

            entity.ToTable("Client", tb =>
                {
                    tb.HasTrigger("ClientAfterInsert");
                    tb.HasTrigger("ClientInsteadOfDeletr");
                });

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Pwd)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC2727EDA715");

            entity.ToTable("TransactionType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RegDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}