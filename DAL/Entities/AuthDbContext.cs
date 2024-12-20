﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

public partial class AuthDbContext : DbContext
{
    public AuthDbContext()
    {
    }

    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ListRole> ListRoles { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Profil> Profils { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=authDB;Username=postgres;Password=salma");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ListRole>(entity =>
        {
            entity.HasKey(e => new { e.IdRole, e.IdMenu }).HasName("list_role_pkey");

            entity.ToTable("list_role");

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.IdMenu).HasColumnName("id_menu");
            entity.Property(e => e.Etat).HasColumnName("etat");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.ListRoles)
                .HasForeignKey(d => d.IdMenu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("list_role_id_menu_fkey");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.ListRoles)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("list_role_id_role_fkey");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("menu_pkey");

            entity.ToTable("menu");

            entity.Property(e => e.IdMenu).HasColumnName("id_menu");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("link");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.Titre)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("titre");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("menu_parent_id_fkey");
        });

        modelBuilder.Entity<Profil>(entity =>
        {
            entity.HasKey(e => e.IdProfil).HasName("profil_pkey");

            entity.ToTable("profil");

            entity.Property(e => e.IdProfil).HasColumnName("id_profil");
            entity.Property(e => e.Adresse)
                .HasMaxLength(255)
                .HasColumnName("adresse");
            entity.Property(e => e.Nom)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("prenom");
            entity.Property(e => e.Telephone).HasColumnName("telephone");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.IdProfil).HasColumnName("id_profil");
            entity.Property(e => e.TypeRole)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("type_role");

            entity.HasOne(d => d.IdProfilNavigation).WithMany(p => p.Roles)
                .HasForeignKey(d => d.IdProfil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_id_profil_fkey");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("utilisateur_pkey");

            entity.ToTable("utilisateur");

            entity.HasIndex(e => e.Matricule, "utilisateur_matricule_key").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Matricule)
                .HasMaxLength(50)
                .HasColumnName("matricule");
            entity.Property(e => e.Motdepasse)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("motdepasse");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Utilisateurs)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("utilisateur_id_role_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
