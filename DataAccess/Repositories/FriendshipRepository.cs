using BusinessEntities.Entities;
using BusinessEntities.Enums;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class FriendshipRepository : BaseRepository
    {
        public FriendshipRepository(SocializrContext context) : base(context)
        {
        }

        public bool IsFriendshipBetween(int idFirstUser, int idSecondUser)
        {
            return Context.Relationships
                .Where(f => f.IdUser1 == idFirstUser && f.IdUser2 == idSecondUser)
                .Any();
        }

        public bool IsRequestPending(int idFirstUser, int idSecondUser)
        {
            var f = Context.FriendRequests
                .Where(r => (r.IdReceiver == idFirstUser && r.IdRequester == idSecondUser) || (r.IdReceiver == idSecondUser && r.IdRequester == idFirstUser))
                .OrderByDescending(r => r.CreatedOn)
                .FirstOrDefault();

            if (f == null || (f.State != (byte)FriendRequestStatus.RequestPending))
                return false;

            return true;
        }

        /// <summary>
        /// Returns the latest valid request between 2 users; Order of the 2 ids does not count here
        /// </summary>
        /// <param name="idFirstUser"></param>
        /// <param name="idSecondUser"></param>
        /// <returns></returns>
        public FriendRequest GetLatestRequest(int idFirstUser, int idSecondUser)
        {
            return Context.FriendRequests
                .AsNoTracking()
                .OrderByDescending(r => r.CreatedOn)
                .Where(r => (r.IdReceiver == idFirstUser && r.IdRequester == idSecondUser) || (r.IdReceiver == idSecondUser && r.IdRequester == idFirstUser))
                .FirstOrDefault();
        }

        public void DeleteRequest(int idRequester, int idReceiver)
        {
            var request = Context.FriendRequests
                .Where(r => r.IdRequester == idRequester && r.IdReceiver == idReceiver)
                .OrderByDescending(r => r.CreatedOn)
                .FirstOrDefault();
            if (request != null)
            {
                Context.FriendRequests.Remove(request);
                Context.SaveChanges();
            }
        }

        public void CancelFriendRelation(int idUser1, int idUser2)
        {
            var relation1 = Context.Relationships
                .Where(r => r.IdUser1 == idUser1 && r.IdUser2 == idUser2)
                .SingleOrDefault();

            var relation2 = Context.Relationships
                .Where(r => r.IdUser1 == idUser2 && r.IdUser2 == idUser1)
                .SingleOrDefault();

            Context.Relationships.Remove(relation1);
            Context.Relationships.Remove(relation2);

            Context.SaveChanges();
        }

        public IEnumerable<User> GetRecentRequests(int idReceiver, int resultNumber)
        {
            return Context.FriendRequests
                .AsNoTracking()
                .Include(r => r.Requester)
                .Where(r => r.IdReceiver == idReceiver)
                .Where(r => r.State == (byte)FriendRequestStatus.RequestPending)
                .OrderByDescending(r => r.CreatedOn)
                .Take(resultNumber)
                .Select(r => r.Requester)
                .ToList();
        }

        public void RejectFriendRequest(int idRequester, int idReceiver)
        {
            var request = Context.FriendRequests
                  .Where(r => r.IdRequester == idRequester && r.IdReceiver == idReceiver)
                  .OrderByDescending(r => r.CreatedOn)
                  .FirstOrDefault();

            request.State = (byte)FriendRequestStatus.RequestRejected;
        }

        public void AcceptFriendRequest(int idRequester, int idReceiver)
        {
            var request = Context.FriendRequests
               .Where(r => r.IdRequester == idRequester && r.IdReceiver == idReceiver)
               .OrderByDescending(r => r.CreatedOn)
               .FirstOrDefault();
            request.State = (byte)FriendRequestStatus.RequestAccepted;

            var friendship1 = new Relationship()
            {
                CreatedOn = DateTime.Now,
                IdUser1 = idRequester,
                IdUser2 = idReceiver,
            };

            var friendship2 = new Relationship()
            {
                CreatedOn = DateTime.Now,
                IdUser2 = idRequester,
                IdUser1 = idReceiver,
            };

            Context.Relationships.Add(friendship1);
            Context.Relationships.Add(friendship2);
            Context.SaveChanges();
        }

        public void SaveRequest(FriendRequest request)
        {
            Context.FriendRequests.Add(request);
            Context.SaveChanges();
        }
    }
}
