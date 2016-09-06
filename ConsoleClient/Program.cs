using BusinessEntities.Entities;
using BusinessLogic.Services;
using DataAccess;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

namespace ConsoleClient
{
    static class Program
    {
        static void Main(string[] args)
        {
            int viewerId = 1;
            var Context = new SocializrContext();

            Expression<Func<Post, bool>> whereClause = post =>
                post.IdUser != viewerId &&
                (
                post.IsPublic == true ||
                post.Author.InRelationships.Where(relation => relation.IdUser2 == viewerId).Any()
                );


            var posts = Context.Posts
                .AsNoTracking()
                .Include(post => post.Comments)
                .Include(post => post.Author.InRelationships)
                .Where(whereClause)
                .OrderByDescending(post => post.CreatedOn)
                .OrderByDescending(post => post.Likes.Count())
                .ToList();
        }

        static private void Seed()
        {
            var context = new SocializrContext();

            context.Counties.Add(new County { Name = "Neamt" });
            context.Counties.Add(new County { Name = "Vrancea" });
            context.Counties.Add(new County { Name = "Alba" });
            context.Counties.Add(new County { Name = "Bihor" });
            context.Counties.Add(new County { Name = "Alba" });
            context.Counties.Add(new County { Name = "Arad" });
            context.Counties.Add(new County { Name = "Arges" });
            context.Counties.Add(new County { Name = "Bacau" });
            context.Counties.Add(new County { Name = "Bihor" });
            context.Counties.Add(new County { Name = "Bistrita-Nasaud" });
            context.Counties.Add(new County { Name = "Botosani" });
            context.Counties.Add(new County { Name = "Brasov" });
            context.Counties.Add(new County { Name = "Braila" });
            context.Counties.Add(new County { Name = "Bucuresti" });
            context.Counties.Add(new County { Name = "Buzau" });
            context.Counties.Add(new County { Name = "Caras Severin" });
            context.Counties.Add(new County { Name = "Calarasi" });
            context.Counties.Add(new County { Name = "Cluj" });
            context.Counties.Add(new County { Name = "Constanta" });
            context.Counties.Add(new County { Name = "Covasna" });
            context.Counties.Add(new County { Name = "Dambovita" });
            context.Counties.Add(new County { Name = "Dolj" });
            context.Counties.Add(new County { Name = "Galati" });
            context.Counties.Add(new County { Name = "Giurgiu" });

            context.SaveChanges();

            context.Cities.Add(new City() { Name = "Piatra Neamt", IdCounty = 1 });
            context.Cities.Add(new City() { Name = "Bicaz", IdCounty = 1 });
            context.Cities.Add(new City() { Name = "Pangarati", IdCounty = 1 });
            context.Cities.Add(new City() { Name = "Targu Neamt", IdCounty = 1 });
            context.Cities.Add(new City() { Name = "Focsani", IdCounty = 2 });
            context.Cities.Add(new City() { Name = "Golesti", IdCounty = 2 });
            context.SaveChanges();

            context.Roles.Add(new Role() { Name = "Admin" });
            context.SaveChanges();

            context.Roles.Add(new Role() { Name = "Public" });
            context.SaveChanges();

            var adminRole = context.Roles.Find(1);
            var publicRole = context.Roles.Find(2);

            Permission permission;

            permission = new Permission() { Name = "ManageLookups" };
            permission.Roles.Add(adminRole);
            context.Permissions.Add(permission);
            context.SaveChanges();

            permission = new Permission() { Name = "ManageUsers" };
            permission.Roles.Add(adminRole);
            context.Permissions.Add(permission);
            context.SaveChanges();

            permission = new Permission() { Name = "AccessPublicApp" };
            permission.Roles.Add(publicRole);
            context.Permissions.Add(permission);
            context.SaveChanges();

            permission = new Permission() { Name = "ManageLocalities" };
            permission.Roles.Add(adminRole);
            context.Permissions.Add(permission);
            context.SaveChanges();

            //Useri
            //=============================================================

            User user;
            var services = new Services();

            user = new User() { FName = "Berbece", LName = "Cristian", Password = "pass", Email = "cristi.berbece@gmail.com" };
            services.UserService.RegisterPublicUser(user);
            user = new User() { FName = "Rotaru", LName = "Ana", Password = "pass", Email = "ana.rotaru@gmail.com" };
            services.UserService.RegisterPublicUser(user);
            user = new User() { FName = "Husanu", LName = "Marius", Password = "pass", Email = "marius.husanu@gmail.com" };
            services.UserService.RegisterPublicUser(user);
            user = new User() { FName = "Berbece", LName = "Raluca", Password = "pass", Email = "raluca.berbece@gmail.com" };
            services.UserService.RegisterPublicUser(user);

            //Interese
            //===============================================================

            context.Interests.Add(new Interest() { Name = "Ski" });
            context.Interests.Add(new Interest() { Name = "Dans" });
            context.Interests.Add(new Interest() { Name = "Tir cu arcul" });
            context.Interests.Add(new Interest() { Name = "Alergat" });
            context.Interests.Add(new Interest() { Name = "Jocuri de noroc" });

            context.SaveChanges();


        }
    }
}
