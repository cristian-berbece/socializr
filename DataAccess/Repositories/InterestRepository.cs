using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Entities;
using System.Data.Entity;

namespace DataAccess.Repositories
{
    public class InterestRepository : BaseRepository
    {
        public InterestRepository(SocializrContext context) :
            base(context)
        {
        }

        public List<Interest> GetAllInterests()
        {
            return Context.Interests
                .ToList();
        }

        public List<int> GetUserInterestsIds(int id)
        {
            return Context.Users
                .AsNoTracking()
                .Include(usr => usr.Interests)
                .Where(usr => usr.IdUser == id)
                .Single()
                .Interests
                .Select(i => i.IdInterest)
                .ToList();
        }

        public void SetInterestList(int idUser, List<int> interestIds)
        {
            var user = Context.Users
                .Include(usr => usr.Interests)
                .Where(u => u.IdUser == idUser)
                .Single();

            var interestList = new List<Interest>();
            interestIds.ForEach(i => interestList.Add(new Interest() { IdInterest = i }));

            user.Interests = Context.Interests.Where(i => interestIds.Contains(i.IdInterest)).ToList();
            Context.SaveChanges();
        }

        public int GetInterestCount()
        {
            return Context.Interests.Count();

        }

        public void DeleteInterest(int idToBeDeleted)
        {
            //attempt to delete by id

            var toDelete = new Interest() { IdInterest = idToBeDeleted };
            Context.Interests.Attach(toDelete);
            Context.Interests.Remove(toDelete);
            Context.SaveChanges();
        }

        public List<Interest> SearchInterests(string query)
        {
            return Context.Interests
                .AsNoTracking()
                .Where(i => i.Name.StartsWith(query))
                .Take(10)
                .ToList();
        }

        public List<Interest> GetUserInterests(int idUser)
        {
            //I use select many because I can't convince EF that I'm getting only
            //one user from my 'Where' clause
            //I would use single() or smth like that but that would mean doing less stuff at the DB level
            return Context.Users
                  .Where(user => user.IdUser == idUser)
                  .AsNoTracking()
                  .Include(user => user.Interests)
                  .SelectMany(user => user.Interests)
                  .ToList();
        }

        public void EditInterest(Interest interest)
        {
            var oldInterest = Context.Interests.Find(interest.IdInterest);
            oldInterest.Name = interest.Name;
            oldInterest.Description = interest.Description;

            Context.SaveChanges();
        }

        public Interest GetInterestById(int id)
        {
            return Context.Interests
                .AsNoTracking()
                .Where(i => i.IdInterest == id)
                .FirstOrDefault();
        }

        public void AddInterest(Interest newInterest)
        {
            Context.Interests.Add(newInterest);
            Context.SaveChanges();
        }

        public List<Interest> GetInterests(int pageNumber, int resultNumber)
        {
            return Context.Interests
                .AsNoTracking()
                .OrderBy(i => i.Name)
                .Skip((pageNumber - 1) * resultNumber)
                .Take(resultNumber)
                .ToList();
        }
    }
}
