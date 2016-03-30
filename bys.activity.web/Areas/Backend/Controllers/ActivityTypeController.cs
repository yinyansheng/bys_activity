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
    public class ActivityTypeController : Controller
    {
        private ActivityTypeDal activityTypeDal = new ActivityTypeDal();

        [AdminAuthorize]
        public ActionResult Index()
        {
            return View(activityTypeDal.GetAll());
        }

        [AdminAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        [AdminAuthorize]
        public ActionResult Submit(ActivityType at)
        {
            at.ID = Guid.NewGuid();
            activityTypeDal.Save(at);
            return RedirectToAction("Index");
        }

        [AdminAuthorize]
        public ActionResult Repeat(Guid ActivityTypeID)
        {
            var target = activityTypeDal.GetByCondition(delegate (ActivityType a)
            {
                return a.ID == ActivityTypeID;
            }).FirstOrDefault();

            if (null != target)
            {
                target.ID = Guid.NewGuid();
                activityTypeDal.Save(target);
            }

            return RedirectToAction("Index");
        }

        [AdminAuthorize]
        public ActionResult Delete(Guid ActivityTypeID)
        {
            activityTypeDal.Delete(ActivityTypeID);
            return RedirectToAction("Index");
        }

        [AdminAuthorize]
        public ActionResult Edit(Guid ActivityTypeID)
        {
            ActivityType model = activityTypeDal.GetByCondition(delegate (ActivityType act)
            {
                return act.ID.Equals(ActivityTypeID);
            }).FirstOrDefault();

            return View(model);
        }

        [AdminAuthorize]
        public ActionResult SubmitEdit(ActivityType at)
        {
            activityTypeDal.Edit(at);
            return RedirectToAction("Index");
        }
    }
}