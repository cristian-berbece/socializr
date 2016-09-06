using BusinessEntities.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mappings
{
    class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(user => user.IdUser);

            Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(100);

            Property(user => user.LName)
                .IsRequired()
                .HasMaxLength(50);

            Property(user => user.FName)
                .IsRequired()
                .HasMaxLength(50);

            Property(user => user.Password)
                .IsRequired()
                .HasMaxLength(100);

            Property(user => user.Birthday)
                .HasColumnType("date");

            HasOptional(user => user.City)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.IdCity);


            //HasOptional(user => user.ProfilePhoto)
            //    .WithOptionalPrincipal()
            //    .Map(c => c.MapKey("IdProfilePhoto").ToTable("Media"))
            //    .WillCascadeOnDelete(false);

        }
    }
}
