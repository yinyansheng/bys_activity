using bys.activity.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bys.activity.dal;
using bys.activity.web.Utils;
using bys.activity.web.Filters;

namespace bys.activity.web.Controllers
{
    public class HomeController : Controller
    {
        private ActivityDal activityDal = new ActivityDal();

        public ActionResult Index()
        {
            HomeVM hvm = new HomeVM();
            string CurrentAlias = User.Identity.Name;

            var activities = activityDal.GetAll();

            hvm.IsAdmin = Configer.Administrators.Contains(CurrentAlias.Split('\\')[1]);

            hvm.Activities = new List<ActivityVM>();
            foreach (var item in activities)
            {
                var joinInfosTemp = activityDal.GetAllJoinInfo(item.ID);
                hvm.Activities.Add(new ActivityVM()
                {
                    activity=item,
                    IsExpeired=DateTime.UtcNow.AddHours(8)>item.StartDateTime,
                    JoinInfos=joinInfosTemp,
                    IsJoin = joinInfosTemp.Select(r=>r.MemberAlias).Contains(CurrentAlias),
                    TimeInfo = GetTimeInfo(item.StartDateTime,item.EndDateTime)
                });
            }

            return View(hvm);
        }

        private string GetTimeInfo(DateTime s, DateTime e) {
            
            if (s.Date == e.Date) {
                return string.Format("{0}/{1} {2} {3}-{4}", s.Month, s.Day, s.DayOfWeek, s.Hour + ":" + s.Minute, e.Hour + ":" + e.Minute);
            }

            return s.ToString("MM/dd HH:mm") + " -" + e.ToString("MM/dd HH:mm");
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Join(Guid ActivityID)
        {
            string CurrentAlias = User.Identity.Name;
            ActivityJoinInfo info = new ActivityJoinInfo()
            {
                ID = Guid.NewGuid(),
                ActivityID = ActivityID,
                MemberAlias = CurrentAlias,
                JoinTime = DateTime.UtcNow.AddHours(8)
            };

            activityDal.SaveJoinInfo(info);
            return RedirectToAction("Index");
        }

        public ActionResult Quit(Guid ActivityID)
        {
            string CurrentAlias = User.Identity.Name;
            activityDal.DeleteJoinInfo(ActivityID,CurrentAlias);
            return RedirectToAction("Index");
        }

        [AdminAuthorize]
        public ActionResult Delete(Guid ActivityID)
        {
            activityDal.Delete(ActivityID);
            return RedirectToAction("Index");
        }

        [AdminAuthorize]
        public ActionResult Submit(CreateActivityVM cvm) {

            Activity activity = new Activity()
            {
                ID = Guid.NewGuid(),
                Name = cvm.Name,
                EndDateTime = cvm.EndDateTime,
                StartDateTime = cvm.StartDateTime,
                Address = cvm.Address,
                Detail = cvm.Description,
                OriginatorAlias=User.Identity.Name,
                CreateDate=DateTime.UtcNow.AddHours(8)
            };

            activityDal.Save(activity);
            return RedirectToAction("Index");
        }


    }
}