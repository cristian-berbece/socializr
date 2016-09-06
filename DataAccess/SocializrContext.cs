using BusinessEntities.Entities;
using DataAccess.Mappings;
using System.Data.Entity;

namespace DataAccess
{
    public class SocializrContext : DbContext
    {
        public SocializrContext() 
            : base("name=SocializrContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<FriendRequest> FriendRequests { get; set; }
        public virtual DbSet<Relationship> Relationships { get; set; }
        public virtual DbSet<Interest> Interests { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Media> Medias { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AlbumMap());
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new CountyMap());
            modelBuilder.Configurations.Add(new RelationshipMap());
            modelBuilder.Configurations.Add(new FriendRequestMap());
            modelBuilder.Configurations.Add(new InterestMap());
            modelBuilder.Configurations.Add(new LikeMap());
            modelBuilder.Configurations.Add(new PostMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new PermissionMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new MediaMap());
        }
    }
}
