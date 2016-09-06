using BusinessEntities.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccess.Repositories
{
    public class LocalitiesRepository : BaseRepository
    {
        public LocalitiesRepository(SocializrContext context)
            : base(context)
        {
        }

        public List<County> GetCounties()
        {
            return Context.Counties
                .OrderBy(c => c.Name)
                .AsNoTracking()
                .ToList();
        }

        public List<County> GetCounties(int pageNumber, int resultsNumber)
        {
            return Context.Counties
                 .OrderBy(c => c.Name)
                 .AsNoTracking()
                 .Skip((pageNumber - 1) * resultsNumber)
                 .Take(resultsNumber)
                 .ToList();
        }


        public List<City> GetCitiesByCountyId(int id)
        {
            return Context.Cities
                .AsNoTracking()
                .Where(c => c.IdCounty == id)
                .ToList();
        }

        
        public void AddCountyWithNoCities(County newCounty)
        {
            Context.Counties.Add(newCounty);
            Context.SaveChanges();
        }

        public void DeleteCounty(int countyId)
        {
            //trying delete by id
            var toDelete = new County() { IdCounty = countyId };

            Context.Counties.Attach(toDelete);
            Context.Counties.Remove(toDelete);
            Context.SaveChanges();
        }

        public County GetCounty(int id)
        {
            return Context.Counties
                 .AsNoTracking()
                 .Where(co => co.IdCounty == id)
                 .SingleOrDefault();
        }

        public void UpdateCounty(County updatedCounty)
        {
            var oldCounty = Context.Counties.Find(updatedCounty.IdCounty);
            oldCounty.Name = updatedCounty.Name;
            oldCounty.ShortName = updatedCounty.ShortName;

            Context.SaveChanges();
        }

        public void AddCity(City city)
        {
            Context.Cities.Add(city);
            Context.SaveChanges();
        }

        public void DeleteCity(int cityId)
        {
            //trying delete by id
            var toDelete = new City() { IdCity = cityId };

            Context.Cities.Attach(toDelete);
            Context.Cities.Remove(toDelete);
            Context.SaveChanges();
        }

        public County GetCountyWithCities(int id)
        {
            return Context.Counties
                .AsNoTracking()
                .Include(county => county.Cities)
                .Where(c => c.IdCounty == id)
                .SingleOrDefault();
        }
    }
}
