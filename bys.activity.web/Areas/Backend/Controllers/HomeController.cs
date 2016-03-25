﻿using bys.activity.dal;
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
            return View();
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
        public ActionResult Delete(Guid ActivityID)
        {
            activityDal.Delete(ActivityID);
            return RedirectToAction("Index");
        }
    }
}