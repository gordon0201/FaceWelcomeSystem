﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FaceWelcome.Repository.Models;

public partial class FaceWelcomeContext : DbContext
{

    public FaceWelcomeContext()
    {

    }
    public FaceWelcomeContext(DbContextOptions<FaceWelcomeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<GuestImage> GuestImages { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<OrganizationGroup> OrganizationGroups { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<WelcomeTemplate> WelcomeTemplates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDbStore"));
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Event__3214EC0789BCC7EC");

            entity.ToTable("Event");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Group__3214EC075EE07858");

            entity.ToTable("Group");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Event).WithMany(p => p.Groups)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_Group_Event");

            entity.HasOne(d => d.Staff).WithMany(p => p.Groups)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_Group_Staff");

            entity.HasOne(d => d.Welcome).WithMany(p => p.Groups)
                .HasForeignKey(d => d.WelcomeId)
                .HasConstraintName("FK_Group_WelcomeTemplate");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Guest__3214EC07B6661F0C");

            entity.ToTable("Guest");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CheckInTime).HasColumnType("datetime");
            entity.Property(e => e.CheckOutTime).HasColumnType("datetime");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Event).WithMany(p => p.Guests)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_Guest_Event");

            entity.HasOne(d => d.Group).WithMany(p => p.Guests)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_Guest_Group");

            entity.HasOne(d => d.Person).WithMany(p => p.Guests)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_Guest_Person");
        });

        modelBuilder.Entity<GuestImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GuestIma__3214EC07E4445EA5");

            entity.ToTable("GuestImage");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Guest).WithMany(p => p.GuestImages)
                .HasForeignKey(d => d.GuestId)
                .HasConstraintName("FK_GuestImage_Guest");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Organiza__3214EC070DA302C0");

            entity.ToTable("Organization");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.District).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Province).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.OrganizationGroup).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.OrganizationGroupId)
                .HasConstraintName("FK_Organization_OrganizationGroup");
        });

        modelBuilder.Entity<OrganizationGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Organiza__3214EC07A44BAF32");

            entity.ToTable("OrganizationGroup");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3214EC0785DDA85A");

            entity.ToTable("Person");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email1).HasMaxLength(255);
            entity.Property(e => e.Email2).HasMaxLength(255);
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Position).HasMaxLength(100);

            entity.HasOne(d => d.Organization).WithMany(p => p.People)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK_Person_Organization");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Staff__3214EC079DA954CA");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(100);

            entity.HasOne(d => d.Event).WithMany(p => p.Staff)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_Staff_Event");
        });

        modelBuilder.Entity<WelcomeTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WelcomeT__3214EC07F22ADBB9");

            entity.ToTable("WelcomeTemplate");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Event).WithMany(p => p.WelcomeTemplates)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_WelcomeTemplate_Event");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}