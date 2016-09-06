using BusinessEntities.Entities;
using BusinessLogic.Services.Base;
using DataAccess.Repositories;
using System.Collections.Generic;
using System;

namespace BusinessLogic.Services
{
    public class LocalitiesService : BaseService
    {
        public LocalitiesService(Repositories repos) : base(repos)
        {
        }

        public List<County> GetCounties()
        {
            return Repositories.LocalitiesRepository.GetCounties();
        }

        public List<County> GetCounties(int pageNumber, int resultsNumber)
        {
            return Repositories.LocalitiesRepository.GetCounties(pageNumber,resultsNumber);

        }

        public List<City> GetCitiesByCountyId(int id)
        {
            return Repositories.LocalitiesRepository.GetCitiesByCountyId(id);
        }

        public void AddCountyWithNoCities(County newCounty)
        {
            Repositories.LocalitiesRepository.AddCountyWithNoCities(newCounty);
        }

        public void DeleteCounty(int countyId)
        {
            Repositories.LocalitiesRepository.DeleteCounty(countyId);

        }

        public County GetCountyWithCities(int id)
        {
            return Repositories.LocalitiesRepository
                .GetCountyWithCities(id);
        }

      

        public void AddCity(City newCity)
        {
            Repositories.LocalitiesRepository.AddCity(newCity);
        }

        public void DeleteCity(int cityId)
        {
            Repositories.LocalitiesRepository.DeleteCity(cityId);
        }

        public County GetCounty(int id)
        {
            return Repositories.LocalitiesRepository.GetCounty(id);
        }

        public void UpdateCounty(County updatedCounty)
        {
            Repositories.LocalitiesRepository.UpdateCounty(updatedCounty);
        }
    }
}
