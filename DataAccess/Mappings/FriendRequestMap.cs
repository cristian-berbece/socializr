using BusinessEntities.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class FriendRequestMap : EntityTypeConfiguration<FriendRequest>
    {
        public FriendRequestMap()
        {
            HasKey(f => new { f.IdRequester, f.IdReceiver, f.CreatedOn });

            Property(f => f.CreatedOn)
                .HasColumnType("date")
                .IsRequired();

            Property(f => f.State)
                .IsRequired();

            HasRequired(f => f.Requester)
                .WithMany(u => u.RequestedRelationships)
                .HasForeignKey(f => f.IdRequester)
                .WillCascadeOnDelete(false);

            HasRequired(f => f.Receiver)
               .WithMany(u => u.ReceivedRelationships)
               .HasForeignKey(f => f.IdReceiver)
               .WillCascadeOnDelete(false);
        }
    }
}
