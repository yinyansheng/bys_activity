using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bys.activity.dal
{
    public class ActivityDal
    {
        public bool Save(Activity a)
        {
            BADBContext db = new BADBContext();
            db.Activities.Add(a);
            return db.SaveChanges() > 0;
        }

        public List<Activity> GetByPage(string sortOrder,string searchString)
        {
            BADBContext db = new BADBContext();

            var activities = from a in db.Activities
                           select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                activities = activities.Where(s => s.Name.Contains(searchString)
                                       || s.Detail.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    activities = activities.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    activities = activities.OrderBy(s => s.StartDateTime);
                    break;
                case "date_desc":
                    activities = activities.OrderByDescending(s => s.StartDateTime);
                    break;
                default:
                    activities = activities.OrderBy(s => s.Name);
                    break;
            }

            return activities.ToList();
        }

        public List<Activity> GetAll()
        {
            BADBContext db = new BADBContext();
            return db.Activities.OrderByDescending(r=>r.CreateDate).ToList();
        }

        public bool SaveJoinInfo(ActivityJoinInfo info)
        {
            BADBContext db = new BADBContext();

            if (db.ActivityJoinInfos.Where(r => r.MemberAlias == info.MemberAlias && r.ActivityID == info.ActivityID).Count() > 0)
                return true;

            db.ActivityJoinInfos.Add(info);
            return db.SaveChanges()>0;
        }

        public bool SaveLikeInfo(ActivityLikeInfo info)
        {
            BADBContext db = new BADBContext();

            if (db.ActivityLikeInfos.Where(r => r.MemberAlias == info.MemberAlias && r.ActivityID == info.ActivityID).Count() > 0)
                return true;

            db.ActivityLikeInfos.Add(info);
            return db.SaveChanges() > 0;
        }

        public bool DeleteJoinInfo(Guid ActivityID, string MemberAlias)
        {
            BADBContext db = new BADBContext();
            var info = db.ActivityJoinInfos.Where(r => r.MemberAlias == MemberAlias && r.ActivityID == ActivityID).FirstOrDefault();
            db.ActivityJoinInfos.Remove(info);
            return db.SaveChanges() > 0;
        }

        public bool DeleteLikeInfo(Guid ActivityID, string MemberAlias)
        {
            BADBContext db = new BADBContext();
            var info = db.ActivityLikeInfos.Where(r => r.MemberAlias == MemberAlias && r.ActivityID == ActivityID).FirstOrDefault();
            db.ActivityLikeInfos.Remove(info);
            return db.SaveChanges() > 0;
        }

        public List<ActivityJoinInfo> GetAllJoinInfo(Guid ActivityID)
        {
            BADBContext db = new BADBContext();
            return db.ActivityJoinInfos.Where(r =>r.ActivityID == ActivityID).ToList();
        }

        public List<ActivityLikeInfo> GetAllLikeInfo(Guid ActivityID)
        {
            BADBContext db = new BADBContext();
            return db.ActivityLikeInfos.Where(r => r.ActivityID == ActivityID).ToList();
        }

        public bool Delete(Guid ActivityID)
        {
            BADBContext db = new BADBContext();
            var info = db.Activities.Where(r => r.ID == ActivityID).FirstOrDefault();
            db.Activities.Remove(info);
            return db.SaveChanges() > 0;
        }
    }
}
