using Microsoft.EntityFrameworkCore;

namespace claimsprocessing.api.Models
{
    public partial class claims_processingContext
    {
        // Ensure this is the only implementation of the partial method
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_user>(entity => entity.Property(e => e.user_fullname).ValueGeneratedOnAddOrUpdate());
        }
    }
}