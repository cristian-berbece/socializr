using BusinessEntities.Entities;
using BusinessLogic.Services.Base;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class InterestService : BaseService
    {
        public InterestService(Repositories repos)
            : base(repos)
        {
        }

        public List<Interest> GetAllInterests()
        {
            return Repositories.InterestRepository.GetAllInterests();
        }

        public List<int> GetUserInterestsIds(int id)
        {
            return Repositories.InterestRepository.GetUserInterestsIds(id);
        }

        public void SetInterestList(int idUser, List<int> interestIds)
        {
            Repositories.InterestRepository.SetInterestList(idUser, interestIds);
        }

        public List<Interest> GetInterests(int pageNumber, int resultNumber)
        {
            return Repositories.InterestRepository.GetInterests(pageNumber, resultNumber);
        }

        public void AddInterest(Interest newInterest)
        {
            Repositories.InterestRepository.AddInterest(newInterest);
        }

        public void DeleteInterest(int idToBeDeleted)
        {
            Repositories.InterestRepository.DeleteInterest(idToBeDeleted);
        }

        public int GetInterestCount()
        {
            return Repositories.InterestRepository.GetInterestCount();
        }

        public Interest GetInterestById(int id)
        {
            return Repositories.InterestRepository.GetInterestById(id);
        }

        public void EditInterest(Interest interest)
        {
            Repositories.InterestRepository.EditInterest(interest);
        }

        public List<Interest> SearchInterests(string query)
        {
            return Repositories.InterestRepository.SearchInterests(query);
        }

        public List<Interest> GetUserInterests(int idUser)
        {
            return Repositories.InterestRepository.GetUserInterests(idUser);
        }
    }
}
