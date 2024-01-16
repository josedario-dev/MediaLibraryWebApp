using System;
using System.Collections.Generic;
using MediaLibrary.WebApp.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.WebApp.Server.Data;

public partial class DataContext : IdentityDbContext<User>
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contributor> Contributor { get; set; }
    public virtual DbSet<Media> Media { get; set; }
    public virtual DbSet<Country> Country { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.Entity<Contributor>(entity =>
        //{
        //    entity.ToTable("Contributor");

        //    entity.Property(e => e.CreateUser)
        //        .HasMaxLength(255)
        //        .HasDefaultValueSql("(suser_sname())");
        //    entity.Property(e => e.CreatedAt)
        //        .HasDefaultValueSql("(getdate())")
        //        .HasColumnType("datetime");
        //    entity.Property(e => e.FullName).HasMaxLength(255);
        //    entity.Property(e => e.Nationality).HasMaxLength(100);
        //    entity.Property(e => e.PhotoPath).HasMaxLength(500);
        //    entity.Property(e => e.UpdateUser)
        //        .HasMaxLength(255)
        //        .HasDefaultValueSql("(suser_sname())");
        //    entity.Property(e => e.UpdatedAt)
        //        .HasDefaultValueSql("(getdate())")
        //        .HasColumnType("datetime");
        //});

        //modelBuilder.Entity<MediaType>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("FK_MediaType");

        //    entity.ToTable("MediaType");

        //    entity.Property(e => e.Name).HasMaxLength(50);
        //});

        //modelBuilder.Entity<Medium>(entity =>
        //{
        //    entity.HasKey(e => new { e.Id, e.MediaTypeId });

        //    entity.Property(e => e.Id).ValueGeneratedOnAdd();
        //    entity.Property(e => e.CreateUser)
        //        .HasMaxLength(255)
        //        .HasDefaultValueSql("(suser_sname())");
        //    entity.Property(e => e.CreatedAt)
        //        .HasDefaultValueSql("(getdate())")
        //        .HasColumnType("datetime");
        //    entity.Property(e => e.Genre).HasMaxLength(100);
        //    entity.Property(e => e.ImagePath).HasMaxLength(500);
        //    entity.Property(e => e.Title).HasMaxLength(255);
        //    entity.Property(e => e.UpdateUser)
        //        .HasMaxLength(255)
        //        .HasDefaultValueSql("(suser_sname())");
        //    entity.Property(e => e.UpdatedAt)
        //        .HasDefaultValueSql("(getdate())")
        //        .HasColumnType("datetime");

        //    entity.HasOne(d => d.Contributor).WithMany(p => p.Media)
        //        .HasForeignKey(d => d.ContributorId)
        //        .HasConstraintName("FK_Media_Contributor");

        //    entity.HasOne(d => d.MediaType).WithMany(p => p.Media)
        //        .HasForeignKey(d => d.MediaTypeId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_Media_MediaType");
        //});

        //OnModelCreatingPartial(modelBuilder);
    }
}
