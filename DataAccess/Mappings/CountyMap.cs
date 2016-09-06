using BusinessEntities.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class CountyMap : EntityTypeConfiguration<County>
    {
        public CountyMap()
        {
            HasKey(c => c.IdCounty);

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(c => c.ShortName)
                .HasMaxLength(2);

            HasMany(c => c.Cities)
                .WithRequired(c => c.County)
                .HasForeignKey(c => c.IdCounty)
                .WillCascadeOnDelete(false);
        }
    }
}
