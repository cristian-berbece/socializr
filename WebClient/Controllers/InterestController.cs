using BusinessEntities.Entities;
using BusinessEntities.Enums;
using Socializr.Code.Attributes;
using Socializr.Code.Base;
using Socializr.Code.Pagination;
using Socializr.Models.Interest;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Socializr.Controllers
{
    [AuthorizePermissions(Permissions.ManageLookups)]
    public class InterestController : BaseController
    {
        // GET: Interest
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult List(int pageNumber = 1)
        {
            var resultsNumber = Config.ResultNumber;
            var interests = Services.InterestService.GetInterests(pageNumber, resultsNumber);
            var model = interests.Select(i => new ViewInterestModel()
            {
                InterestId = i.IdInterest,
                Description = i.Description,
                Name = i.Name
            })
            .ToList();

            var pager = new Pager<ViewInterestModel>()
            {
                CurrentPageNumber = pageNumber,
                ElementList = model,
                ResultsPerPage = resultsNumber,
                TotalResultNumber = Services.InterestService.GetInterestCount()
            };

            if (pager.isEmptyPage())
            {
                if (pager.LastPageNumber > 0)
                    return RedirectToAction("List", new { pageNumber = pager.LastPageNumber });
                else
                    return View("List");
            }

            return View(pager);
        }

        [HttpGet]
        public ActionResult Create(int pageNumber = 1)
        {
            var model = new CreateInterestModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateInterestModel model)
        {
            var newInterest = new Interest()
            {
                Name = model.Name,
                Description = model.Description
            };
            Services.InterestService.AddInterest(newInterest);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var interes = Services.InterestService.GetInterestById(id);
            if (interes == null)
            {
                return HttpNotFound();
            }
            var model = new EditInterestModel()
            {
                InterestId = interes.IdInterest,
                Description = interes.Description,
                Name = interes.Name
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditInterestModel model)
        {
            var interest = new Interest()
            {
                Name = model.Name,
                IdInterest = model.InterestId,
                Description = model.Description
            };

            Services.InterestService.EditInterest(interest);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(FormCollection form)
        {
            Services.InterestService.DeleteInterest(Convert.ToInt32(form["InterestId"]));
            return RedirectToAction("List");

        }
    }
}