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

        public bool DeleteJoinInfo(Guid ActivityID, string MemberAlias)
        {
            BADBContext db = new BADBContext();
            var info = db.ActivityJoinInfos.Where(r => r.MemberAlias == MemberAlias && r.ActivityID == ActivityID).FirstOrDefault();
            db.ActivityJoinInfos.Remove(info);
            return db.SaveChanges() > 0;
        }

        public List<ActivityJoinInfo> GetAllJoinInfo(Guid ActivityID)
        {
            BADBContext db = new BADBContext();
            return db.ActivityJoinInfos.Where(r =>r.ActivityID == ActivityID).ToList();
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
