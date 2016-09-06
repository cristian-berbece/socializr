using BusinessEntities.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class MediaMap : EntityTypeConfiguration<Media>
    {

        public MediaMap()
        {
            HasKey(m => m.IdMedia);

            Property(m => m.Caption)
                .HasMaxLength(1000);

            Property(m => m.Type)
                .IsRequired();

            Property(m => m.CreatedOn)
                .IsRequired()
                .HasColumnType("datetime");

            HasOptional(m => m.Album)
                .WithMany(a => a.Medias)
                .HasForeignKey(m => m.IdAlbum)
                .WillCascadeOnDelete(false);

            HasRequired(m => m.Author)
                .WithMany(u => u.Medias)
                .HasForeignKey(m => m.IdUser)
                .WillCascadeOnDelete(false);

        }
    }
}
