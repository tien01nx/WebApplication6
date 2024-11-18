using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication6.Models.db;

public partial class Demo2Context : DbContext
{
    public Demo2Context()
    {
    }

    public Demo2Context(DbContextOptions<Demo2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<SpGroup> SpGroups { get; set; }

    public virtual DbSet<SpGroupPer> SpGroupPers { get; set; }

    public virtual DbSet<SpGroupUser> SpGroupUsers { get; set; }

    public virtual DbSet<SpPermission> SpPermissions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-GRR6EAN;Initial Catalog=demo2;Persist Security Info=True;User ID=sa; Password=123456;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SpGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SP_Group__3214EC075D022B6C");

            entity.ToTable("SP_Group");

            entity.HasIndex(e => e.GroupName, "UQ__SP_Group__6EFCD434A9DEA88C").IsUnique();

            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreateBy).HasMaxLength(100);
            entity.Property(e => e.GroupName).HasMaxLength(100);
        });

        modelBuilder.Entity<SpGroupPer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SP_Group__3214EC0733FD1C43");

            entity.ToTable("SP_GroupPer");

            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.Form).HasMaxLength(100);
            entity.Property(e => e.GroupName).HasMaxLength(100);
            entity.Property(e => e.Permission).HasMaxLength(100);

            entity.HasOne(d => d.GroupNameNavigation).WithMany(p => p.SpGroupPers)
                .HasPrincipalKey(p => p.GroupName)
                .HasForeignKey(d => d.GroupName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SP_GroupP__Group__3E52440B");

            entity.HasOne(d => d.PermissionNavigation).WithMany(p => p.SpGroupPers)
                .HasPrincipalKey(p => p.Permission)
                .HasForeignKey(d => d.Permission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SP_GroupP__Permi__3F466844");
        });

        modelBuilder.Entity<SpGroupUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SP_Group__3214EC07A77F306A");

            entity.ToTable("SP_GroupUser");

            entity.Property(e => e.GroupName).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.GroupNameNavigation).WithMany(p => p.SpGroupUsers)
                .HasPrincipalKey(p => p.GroupName)
                .HasForeignKey(d => d.GroupName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SP_GroupU__Group__4222D4EF");
        });

        modelBuilder.Entity<SpPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SP_Permi__3214EC07C1EB6E97");

            entity.ToTable("SP_Permission");

            entity.HasIndex(e => e.Permission, "UQ__SP_Permi__F5C738EB5DF49F38").IsUnique();

            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.Form).HasMaxLength(100);
            entity.Property(e => e.Permission).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0740D448BD");

            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "UQ__User__536C85E46ACB98C3").IsUnique();

            entity.Property(e => e.Fullname).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
