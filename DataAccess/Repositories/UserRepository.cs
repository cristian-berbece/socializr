using BusinessEntities.Entities;
using BusinessEntities.Enums;
using DataAccess;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(SocializrContext context)
            : base(context) { }

        private Role publicRole;
        public Role PublicRole
        {
            get
            {
                return publicRole ?? (publicRole = Context.Roles.Where(r => r.IdRole == (int)Roles.Public).Single());
            }
        }

        public void SaveUser(User newUser)
        {
            Context.Users.Add(newUser);
            Context.SaveChanges();
        }

        public bool IsPublicUser(int idRequested)
        {
            return Context.Users
                .Where(user => user.IdUser == idRequested && user.IsPublic == true)
                .Any();
        }

        public void SaveUserChanges(User editedUser)
        {
            var existingUser = Context.Users
                .First(u => u.IdUser == editedUser.IdUser);

            existingUser.Birthday = editedUser.Birthday ?? existingUser.Birthday;
            existingUser.IdCity = editedUser.IdCity ?? existingUser.IdCity;
            existingUser.FName = editedUser.FName ?? existingUser.FName;
            existingUser.LName = editedUser.LName ?? existingUser.LName;
            existingUser.IsMale = editedUser.IsMale ?? existingUser.IsMale;
            existingUser.IsPublic = editedUser.IsPublic;
            Context.SaveChanges();
        }

        public List<User> SearchUsers(string query, int resultNumber)
        {
            Expression<Func<User, bool>> whereClause =
                user =>
                user.FName.StartsWith(query) ||
                user.LName.StartsWith(query) ||
                (user.LName + " " + user.FName).StartsWith(query) ||
                (user.FName + " " + user.LName).StartsWith(query);

            var resultList = Context.Users
                .AsNoTracking()
                .Where(whereClause)
                .Take(resultNumber)
                .ToList();

            return resultList;
        }

        public User GetUserMinimalInfo(int id)
        {
            return Context.Users
                .AsNoTracking()
                .Where(u => u.IdUser == id)
                .Select(user => new User()
                {
                    FName = user.FName,
                    LName = user.LName,
                }).FirstOrDefault();
        }

        public bool IsEmailAvailable(string email)
        {
            return !Context.Users
                .Any(u => u.Email == email);
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            return Context.Users
                .AsNoTracking()
                .Include(u => u.Roles.Select(r => r.Permissions))
                .FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public User GetUserById(int id)
        {
            return Context.Users
                .AsNoTracking()
                .Where(u => u.IdUser == id)
                .Include(u => u.City)
                .Include(u => u.City.County)
                .Include(u => u.Interests)
                .FirstOrDefault();
        }
    }
}
