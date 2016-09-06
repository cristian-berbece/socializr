using BusinessEntities.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class RelationshipMap : EntityTypeConfiguration<Relationship>
    {
        public RelationshipMap()
        {
            HasKey(f => new { f.IdUser1, f.IdUser2 });

            Property(f => f.CreatedOn)
                .IsRequired()
                .HasColumnType("datetime");


            HasRequired(f => f.User1)
                .WithMany(u => u.InRelationships)
                .HasForeignKey(f => f.IdUser1)
                .WillCascadeOnDelete(false);


            HasRequired(f => f.User2)
                .WithMany(u => u.OutRelationships)
                .HasForeignKey(f => f.IdUser2)
                .WillCascadeOnDelete(false);
        }
    }
}
