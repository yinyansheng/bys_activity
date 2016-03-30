using bys.activity.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using bys.activity.web.Models;
using bys.activity.web.Filters;

namespace bys.activity.web.Areas.Backend.Controllers
{
    public class HomeController : Controller
    {
        private ActivityDal activityDal = new ActivityDal();
        private ActivityTypeDal activityTypeDal = new ActivityTypeDal();

        [AdminAuthorize]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(activityDal.GetByPage(sortOrder, searchString).ToPagedList(pageNumber, pageSize));
        }

        [AdminAuthorize]
        public ActionResult Create()
        {
            CreateActivityVM cvm = new CreateActivityVM();
            cvm.ActivityTypes = activityTypeDal.GetAll();
            return View(cvm);
        }

        [AdminAuthorize]
        public ActionResult Edit(Guid ActivityID)
        {
            CreateActivityVM model = activityDal.GetByCondition(delegate (Activity act)
            {
                return act.ID.Equals(ActivityID);
            }).Select(r => new CreateActivityVM()
            {
                ID = r.ID,
                Name = r.Name,
                StartDateTime = r.StartDateTime,
                EndDateTime = r.EndDateTime,
                Address = r.Address,
                Description = r.Detail,
                ActivityTypes = activityTypeDal.GetAll(),
                ActivityTypeID=r.ActivityTypeID
            })
            .FirstOrDefault();
            return View(model);
        }

        [AdminAuthorize]
        public ActionResult Submit(CreateActivityVM cvm)
        {

            Activity activity = new Activity()
            {
                ID = Guid.NewGuid(),
                Name = cvm.Name,
                EndDateTime = cvm.EndDateTime,
                StartDateTime = cvm.StartDateTime,
                Address = cvm.Address,
                Detail = cvm.Description,
                OriginatorAlias = User.Identity.Name,
                CreateDate = DateTime.UtcNow.AddHours(8)
            };

            activityDal.Save(activity);
            return RedirectToAction("Index");
        }

        [AdminAuthorize]
        public ActionResult Repeat(Guid ActivityID)
        {
            var target = activityDal.GetByCondition(delegate (Activity a)
            {
                return a.ID == ActivityID;
            }).FirstOrDefault();

            if (null != target) {
                target.ID = Guid.NewGuid();
                target.CreateDate = DateTime.UtcNow.AddHours(8);
                activityDal.Save(target);
            }

            return RedirectToAction("Index");
        }

        [AdminAuthorize]
        public ActionResult SubmitEdit(CreateActivityVM cvm)
        {

            Activity activity = new Activity()
            {
                ID = cvm.ID,
                Name = cvm.Name,
                EndDateTime = cvm.EndDateTime,
                StartDateTime = cvm.StartDateTime,
                Address = cvm.Address,
                Detail = cvm.Description,
                OriginatorAlias = User.Identity.Name,
                ActivityTypeID=cvm.ActivityTypeID,
                CreateDate = DateTime.UtcNow.AddHours(8)
            };

            activityDal.Edit(activity);
            return RedirectToAction("Index");
        }

        [AdminAuthorize]
        public ActionResult Delete(Guid ActivityID)
        {
            activityDal.Delete(ActivityID);
            return RedirectToAction("Index");
        }

    }
}