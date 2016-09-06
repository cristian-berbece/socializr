using BusinessEntities.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class LikeMap : EntityTypeConfiguration<Like>
    {
        public LikeMap()
        {
            HasKey(l => new { l.IdPost, l.IdUser });

            Property(l => l.CreatedOn)
                .HasColumnType("datetime")
                .IsRequired();

            HasRequired(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.IdPost)
                .WillCascadeOnDelete(false);

            HasRequired(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.IdUser)
                .WillCascadeOnDelete(false);
        }

    }
}
