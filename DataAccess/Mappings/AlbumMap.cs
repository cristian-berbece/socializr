using BusinessEntities.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class AlbumMap : EntityTypeConfiguration<Album>
    {
        public AlbumMap()
        {
            HasKey(a => a.IdAlbum);

            Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(a => a.CreatedOn)
                .HasColumnType("date");

        }
    }
}
