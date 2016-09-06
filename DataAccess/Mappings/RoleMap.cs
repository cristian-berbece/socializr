using BusinessEntities.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            HasKey(role => role.IdRole);

            Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(r => r.Description)
                .HasMaxLength(500);

            HasMany(r => r.Users)
                .WithMany(u => u.Roles)
                .Map(c => c.ToTable("UsersRoles").MapLeftKey("IdRole").MapRightKey("IdUser"));

            HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .Map(c => c.ToTable("RolesPermissions").MapLeftKey("IdRole").MapRightKey("IdPermission"));
        }
    }
}
