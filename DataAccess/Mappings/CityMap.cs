using BusinessEntities.Entities;
using System.Data.Entity.ModelConfiguration;


namespace DataAccess.Mappings
{
    class CityMap : EntityTypeConfiguration<City>
    {
        public CityMap()
        {
            HasKey(c => c.IdCity);

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
