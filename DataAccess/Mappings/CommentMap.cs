using BusinessEntities.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            HasKey(c => c.IdComment);

            Property(c => c.Body)
                .IsRequired()
                .HasMaxLength(1000);

            Property(c => c.CreatedOn)
                .IsRequired()
                .HasColumnType("datetime");

        }
    }
}
