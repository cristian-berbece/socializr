using BusinessLogic.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories;
using BusinessEntities.Enums;
using BusinessEntities.Entities;

namespace BusinessLogic.Services
{
    public class FriendshipService : BaseService
    {
        public FriendshipService(Repositories repositories)
            : base(repositories)
        {
        }

        /// <summary>
        /// Returns an enum option; 
        /// The order of the parameters is relevant in the case of a friend request situation
        /// </summary>
        /// <param name="idUser1"></param>
        /// <param name="idUser2"></param>
        /// <returns></returns>
        public RelationshipStatus GetRelationshipStatus(int idFirstUser, int idSecondUser)
        {
            if (idFirstUser == idSecondUser)
                return RelationshipStatus.NoPossibleRelation;

            if (Repositories.FriendshipRepository.IsFriendshipBetween(idFirstUser, idSecondUser))
                return RelationshipStatus.Friends;

            var request = Repositories.FriendshipRepository.GetLatestRequest(idFirstUser, idSecondUser);
            if (IsRequestPending(request))
            {
                return request.IdRequester == idFirstUser ? RelationshipStatus.RequestSentByFirstUser : RelationshipStatus.RequestReceivedByFirstUser;
            }
            return RelationshipStatus.NoRelationship;
        }

        public void CancelFriendRequest(int idRequester, int idReceiver)
        {
            //think conditions through!
            var currentStatus = GetRelationshipStatus(idRequester, idReceiver);
            if(currentStatus == RelationshipStatus.RequestSentByFirstUser)
            {
                Repositories.FriendshipRepository.DeleteRequest(idRequester, idReceiver);
            }

        }

        public void CancelFriendRelation(int idUser1, int idUser2)
        {
            var currentStatus = GetRelationshipStatus(idUser1, idUser2);
            if (currentStatus == RelationshipStatus.Friends)
            {
                Repositories.FriendshipRepository.CancelFriendRelation(idUser1,idUser2);
            }
        }

        public void RejectFriendRequest(int idRequester, int idReceiver)
        {
            var currentStatus = GetRelationshipStatus(idRequester, idReceiver);
            if(currentStatus == RelationshipStatus.RequestSentByFirstUser)
            {
                Repositories.FriendshipRepository.RejectFriendRequest(idRequester, idReceiver);
            }
        }

        public IEnumerable<User> GetRecentRequests(int idRequested, int resultNumber)
        {
            return Repositories.FriendshipRepository.GetRecentRequests(idRequested, resultNumber);
        }

        public void AcceptFriendRequest(int idRequester, int idReceiver)
        {
            var currentStatus = GetRelationshipStatus(idRequester, idReceiver);
            if (currentStatus == RelationshipStatus.RequestSentByFirstUser)
            {
                Repositories.FriendshipRepository.AcceptFriendRequest(idRequester, idReceiver);
            }
        }

        public void SendFriendRequest(FriendRequest request)
        {
            var currentStatus = GetRelationshipStatus(request.IdReceiver, request.IdRequester);
            if(currentStatus == RelationshipStatus.NoRelationship)
            {
                Repositories.FriendshipRepository.SaveRequest(request);
            }
        }

        private bool IsRequestPending(FriendRequest request)
        {
            return (request != null && request.State == (byte)FriendRequestStatus.RequestPending);
        }
    }
}
