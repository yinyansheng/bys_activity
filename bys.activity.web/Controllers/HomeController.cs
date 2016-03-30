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
        private MemberDal memberDal = new MemberDal();

        public ActionResult Index()
        {
            HomeVM hvm = new HomeVM();
            string CurrentAlias = User.Identity.Name;

            var activities = activityDal.GetAll();

            hvm.IsAdmin = Configer.Administrators.Contains(CommonUtils.GetShortName(CurrentAlias));
            hvm.CurrentMember = memberDal.GetByCondition(delegate (Member m)
            {
                return m.Alias.Equals(CurrentAlias);
            }).FirstOrDefault();

            hvm.Activities = new List<ActivityVM>();
            foreach (var item in activities)
            {
                var joinInfosTemp = activityDal.GetAllJoinInfo(item.ID);
                var likeInfosTemp = activityDal.GetAllLikeInfo(item.ID);
                hvm.Activities.Add(new ActivityVM()
                {
                    activity = item,
                    IsExpeired = DateTime.UtcNow.AddHours(8) > item.StartDateTime,
                    JoinInfos = joinInfosTemp,
                    LikeInfos = likeInfosTemp,
                    IsJoin = joinInfosTemp.Select(r => r.MemberAlias).Contains(CurrentAlias),
                    IsLike = likeInfosTemp.Select(r => r.MemberAlias).Contains(CurrentAlias),
                    TimeInfo = CommonUtils.GetTimeInfo(item.StartDateTime, item.EndDateTime)
                });
            }

            return View(hvm);
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
            activityDal.DeleteJoinInfo(ActivityID, CurrentAlias);
            return RedirectToAction("Index");
        }

        public ActionResult Like(Guid ActivityID)
        {
            string CurrentAlias = User.Identity.Name;
            ActivityLikeInfo info = new ActivityLikeInfo()
            {
                ID = Guid.NewGuid(),
                ActivityID = ActivityID,
                MemberAlias = CurrentAlias,
                LikeTime = DateTime.UtcNow.AddHours(8)
            };

            activityDal.SaveLikeInfo(info);
            return RedirectToAction("Index");
        }

        public ActionResult UnLike(Guid ActivityID)
        {
            string CurrentAlias = User.Identity.Name;
            activityDal.DeleteLikeInfo(ActivityID, CurrentAlias);
            return RedirectToAction("Index");
        }

    }
}