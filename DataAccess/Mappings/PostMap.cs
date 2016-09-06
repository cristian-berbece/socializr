using BusinessEntities.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class PostMap : EntityTypeConfiguration<Post>
    {
        public PostMap()
        {
            HasKey(p => p.IdPost);

            Property(p => p.Title)
                .HasMaxLength(300);

            Property(p => p.Body)
                .IsRequired()
                .HasMaxLength(1000);

            Property(p => p.CreatedOn)
                .IsRequired()
                .HasColumnType("datetime");

            Property(p => p.IsPublic)
                .IsRequired();

            HasMany(p => p.Comments)
                .WithRequired(c => c.Post)
                .HasForeignKey(c => c.IdPost)
                .WillCascadeOnDelete(false);

            HasRequired(p => p.Author)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.IdUser)
                .WillCascadeOnDelete(false);

        }
    }
}
