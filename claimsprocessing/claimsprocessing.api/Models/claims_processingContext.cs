using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace claimsprocessing.api.Models;

public partial class claims_processingContext : DbContext
{
    public claims_processingContext()
    {
    }

    public claims_processingContext(DbContextOptions<claims_processingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<tbl_user> tbl_user { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=BHARADWAJ\\SQLEXPRESS;Database=claims_processing;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tbl_user>(entity =>
        {
            entity.HasKey(e => e.user_id);

            entity.Property(e => e.created_on)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.modified_on).HasColumnType("datetime");
            entity.Property(e => e.user_email).HasMaxLength(255);
            entity.Property(e => e.user_fname).HasMaxLength(50);
            entity.Property(e => e.user_lname).HasMaxLength(50);
            entity.Property(e => e.user_mname).HasMaxLength(50);
            entity.Property(e => e.user_password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
