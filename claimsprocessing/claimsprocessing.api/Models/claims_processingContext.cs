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

    public virtual DbSet<tbl_claim> tbl_claim { get; set; }

    public virtual DbSet<tbl_claim_status_update> tbl_claim_status_update { get; set; }

    public virtual DbSet<tbl_user> tbl_user { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=BHARADWAJ\\SQLEXPRESS;Database=claims_processing;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tbl_claim>(entity =>
        {
            entity.HasKey(e => e.claim_id);

            entity.ToTable(tb =>
            {
                tb.HasTrigger("trg_tbl_claim_insert");
                tb.HasTrigger("trg_tbl_claim_update");
            });

            entity.Property(e => e.claim_amount).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.claim_status).HasMaxLength(256);
            entity.Property(e => e.claim_type).HasMaxLength(256);
            entity.Property(e => e.created_on)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.modified_on).HasColumnType("datetime");

            entity.HasOne(d => d.claim_user).WithMany(p => p.tbl_claim)
                .HasForeignKey(d => d.claim_user_id)
                .HasConstraintName("FK_tbl_claim_tbl_user_claim_user_id_user_id");
        });

        modelBuilder.Entity<tbl_claim_status_update>(entity =>
        {
            entity.HasKey(e => e.claim_status_update_id);

            entity.Property(e => e.claim_amount).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.claim_status).HasMaxLength(256);
            entity.Property(e => e.claim_type).HasMaxLength(256);
            entity.Property(e => e.created_on)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.claim).WithMany(p => p.tbl_claim_status_update)
                .HasForeignKey(d => d.claim_id)
                .HasConstraintName("FK_tbl_claim_status_update_tbl_claim_claim_id_claim_id");

            entity.HasOne(d => d.claim_user).WithMany(p => p.tbl_claim_status_update)
                .HasForeignKey(d => d.claim_user_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_claim_status_update_tbl_user_claim_user_id_user_id");
        });

        modelBuilder.Entity<tbl_user>(entity =>
        {
            entity.HasKey(e => e.user_id);

            entity.HasIndex(e => e.user_email, "IX_tbl_user_user_email").IsUnique();

            entity.Property(e => e.created_on)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.modified_on).HasColumnType("datetime");
            entity.Property(e => e.user_email).HasMaxLength(255);
            entity.Property(e => e.user_fname).HasMaxLength(50);
            entity.Property(e => e.user_fullname).HasMaxLength(150);
            entity.Property(e => e.user_lname).HasMaxLength(50);
            entity.Property(e => e.user_mname).HasMaxLength(50);
            entity.Property(e => e.user_password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}