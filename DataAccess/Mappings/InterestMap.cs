using BusinessEntities.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class InterestMap : EntityTypeConfiguration<Interest>
    {
        public InterestMap()
        {
            HasKey(i => i.IdInterest);

            Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(i => i.Description)
                .HasMaxLength(500);

            HasMany(i => i.Users)
                .WithMany(u => u.Interests)
                .Map(c => c.ToTable("UserInterests").MapLeftKey("IdInterest").MapRightKey("IdUser"));
        }
    }
}
