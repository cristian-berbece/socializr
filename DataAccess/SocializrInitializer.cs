using BusinessEntities.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace DataAccess
{
    public class SocializrInitializer : CreateDatabaseIfNotExists<SocializrContext>
    {
        private List<County> initialCounties;
        private List<City> initialCities;


        public void populateLists()
        {
            initialCounties = new List<County>();
            initialCounties.Add(new County { Name = "Alba" });
            initialCounties.Add(new County { Name = "Vrancea" });
            initialCounties.Add(new County { Name = "Alba" });
            initialCounties.Add(new County { Name = "Bihor" });
            initialCounties.Add(new County { Name = "Teleorman" });
            initialCounties.Add(new County { Name = "Neamt" });
            initialCounties.Add(new County { Name = "Bucuresti" });

            initialCities = new List<City>();
            initialCities.Add(new City() { Name = "Piatra Neamt", IdCounty = 1 });
            initialCities.Add(new City() { Name = "Bicaz", IdCounty = 1 });
            initialCities.Add(new City() { Name = "Pangarati", IdCounty = 1 });
            initialCities.Add(new City() { Name = "Targu Neamt", IdCounty = 1 });
            initialCities.Add(new City() { Name = "Focsani", IdCounty = 2 });
            initialCities.Add(new City() { Name = "Golesti", IdCounty = 1 });
        }

        protected override void Seed(SocializrContext context)
        {
            populateLists();
            initialCounties.ForEach(c => context.Counties.Add(c));
            initialCities.ForEach(c => context.Cities.Add(c));
        }

        public static void Init()
        {
            Database.SetInitializer<SocializrContext>(new SocializrInitializer());
        }

    }
}
