using BusinessEntities.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class PermissionMap : EntityTypeConfiguration<Permission>
    {
        public PermissionMap()
        {
            HasKey(p => p.IdPermission);

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.Description)
                .HasMaxLength(500);
        }
    }
}
