using BusinessEntities.Entities;
using BusinessEntities.Enums;
using Socializr.Code.Attributes;
using Socializr.Code.Base;
using Socializr.Models.Localities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socializr.Controllers
{
    [AuthorizePermissions(Permissions.ManageLocalities)]
    public class LocalitiesController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("ListCounty", new { pageNumber = 1 });
        }

        #region Counties CRUD

        [HttpGet]
        public ActionResult ListCounty(int pageNumber = 1)
        {
            var countyList = Services.LocalitiesService.GetCounties(pageNumber, Config.ResultNumber);
            var list = countyList
                .Select(c => new ListCountyModel()
                {
                    CountyId = c.IdCounty,
                    CountyName = c.Name,
                }).ToList();

            ViewBag.PageNumber = pageNumber;
            return View(list);
        }

        [HttpGet]
        public ActionResult CreateCounty()
        {
            var model = new CreateCountyModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCounty(CreateCountyModel model)
        {
            var county = new County()
            {
                Name = model.Name,
                ShortName = model.ShortName
            };
            Services.LocalitiesService.AddCountyWithNoCities(county);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteCounty(FormCollection form)
        {
            Services.LocalitiesService.DeleteCounty(Convert.ToInt32(form["countyId"]));
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Edit County Name and Short Name
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditCounty(int id)
        {
            var county = Services.LocalitiesService.GetCounty(id);
            if (county == null)
            {
                return HttpNotFound();
            }

            var model = new EditCountyModel()
            {
                CountyId = county.IdCounty,
                Name = county.Name,
                ShortName = county.ShortName
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditCounty(EditCountyModel model)
        {
            if (ModelState.IsValid)
            {
                var updatedCounty = new County()
                {
                    Name = model.Name,
                    ShortName = model.ShortName,
                    IdCounty = model.CountyId
                };

                Services.LocalitiesService.UpdateCounty(updatedCounty);
            }

            return RedirectToAction("EditCounty", new { id = model.CountyId });
        }

        #endregion

        #region Cities CRUD

        /// <summary>
        /// Add city 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ListCity(int id)
        {
            var cities = Services.LocalitiesService.GetCitiesByCountyId(id);
            var model = cities.Select(city => new ListCityModel()
            {
                CityId = city.IdCity,
                Name = city.Name
            }).ToList();
            ViewBag.CountyId = id;
            return View(model);
        }

        [HttpGet]
        public ActionResult AddCity(int id)
        {
            var model = new AddCityModel()
            {
                CountyId = id
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCity(AddCityModel model)
        {
            if (ModelState.IsValid)
            {

                var newCity = new City()
                {
                    IdCounty = model.CountyId,
                    Name = model.Name,
                };
                Services.LocalitiesService.AddCity(newCity);
            }
            return RedirectToAction("EditCounty", new { id = model.CountyId });
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCity(FormCollection form)
        {
            Services.LocalitiesService.DeleteCity(Convert.ToInt32(form["item.CityId"]));

            return RedirectToAction("EditCounty", new { id = Convert.ToInt32(form["CountyId"]) });
        }

        #endregion
    }
}