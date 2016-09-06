using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using BusinessLogic.Services.Base;
using DataAccess.Repositories;
using BusinessEntities.Entities;
using BusinessEntities.Enums;
using BusinessEntities.CustomTypes.ProfileDisplay;

namespace BusinessLogic.Services
{
    public class UserService : BaseService
    {
        public UserService(Repositories repos)
            : base(repos)
        {
        }

        public bool HasAccessToProfile(int idRequested, int idViewer)
        {
            if (idRequested == idViewer)
            {
                return true;
            }

            if (Repositories.UserRepository.IsPublicUser(idRequested))
            {
                return true;
            }

            if (Repositories.FriendshipRepository.IsFriendshipBetween(idRequested, idViewer))
            {
                return true;
            }

            return false;
        }

        public void SaveUserChanges(User editedUser)
        {
            Repositories.UserRepository.SaveUserChanges(editedUser);
        }

        public void RegisterPublicUser(User newUser)
        {
            newUser.Roles.Add(Repositories.UserRepository.PublicRole);
            Repositories.UserRepository.SaveUser(newUser);
        }

        public bool IsEmailAvailable(string email)
        {
            return Repositories.UserRepository.IsEmailAvailable(email);
        }

        public User GetUserById(int id)
        {
            return Repositories.UserRepository.GetUserById(id);
        }

        public User GetUserById(int id, int viewerId)
        {
            if (HasAccessToProfile(id, viewerId))
            {
                return Repositories.UserRepository.GetUserById(id);
            }
            else
            {
                return Repositories.UserRepository.GetUserMinimalInfo(id);
            }
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            return Repositories.UserRepository.GetByEmailAndPassword(email, password);
        }

        public List<User> SearchUsers(string query, int resultNumber)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                var list = Repositories.UserRepository.SearchUsers(query,resultNumber);
                return list;
            }
            else
                return new List<User>();
        }

       
    }
}
