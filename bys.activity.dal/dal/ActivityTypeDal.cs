using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bys.activity.dal
{
    public class ActivityTypeDal
    {
        public IEnumerable<ActivityType> GetAll()
        {
            BADBContext db = new BADBContext();
            return db.ActivityTypes.OrderByDescending(r=>r.Rank).ToList();
        }

        public bool Save(ActivityType a)
        {
            BADBContext db = new BADBContext();

            var maxRank = db.ActivityTypes.Max(r => r.Rank);
            if (null == maxRank) maxRank = 0;
            a.Rank = ++maxRank;
            a.CreateDate = DateTime.UtcNow.AddHours(8);

            db.ActivityTypes.Add(a);
            return db.SaveChanges() > 0;
        }

        public IList<ActivityType> GetByCondition(Func<ActivityType, bool> condition)
        {
            using (BADBContext db = new BADBContext())
            {
                return db.ActivityTypes.Where(condition).ToList();
            }
        }

        public bool Delete(Guid ActivityTypeID)
        {
            BADBContext db = new BADBContext();
            var info = db.ActivityTypes.Where(r => r.ID == ActivityTypeID).FirstOrDefault();
            db.ActivityTypes.Remove(info);
            return db.SaveChanges() > 0;
        }


        public void Edit(ActivityType at)
        {
            using (BADBContext db = new BADBContext())
            {
                var old = db.ActivityTypes.Where(r => r.ID == at.ID).FirstOrDefault();
                if (null != old)
                {
                    old.Name = at.Name;
                    old.Description = at.Description;
                    old.PosterImagePath1 = at.PosterImagePath1;
                    old.CreateDate = DateTime.UtcNow.AddHours(8);
                }
                db.SaveChanges();
            }
        }

        public bool Up(Guid activityTypeID)
        {
            BADBContext db = new BADBContext();
            var maxRank = db.ActivityTypes.Max(r => r.Rank);
            var cur = db.ActivityTypes.Where(r => r.ID == activityTypeID).FirstOrDefault();
            
            if (cur.Rank == maxRank) { return true; }
            var previous = db.ActivityTypes.Where(r => r.Rank == cur.Rank + 1).FirstOrDefault();

            int? curRank = cur.Rank;
            cur.Rank = previous.Rank;
            previous.Rank = curRank;

            return db.SaveChanges() > 0;
        }

        public bool Down(Guid activityTypeID)
        {
            BADBContext db = new BADBContext();
            var minRank = db.ActivityTypes.Min(r => r.Rank);
            var cur = db.ActivityTypes.Where(r => r.ID == activityTypeID).FirstOrDefault();
            if (cur.Rank == minRank) { return true; }

            var next = db.ActivityTypes.Where(r => r.Rank == cur.Rank - 1).FirstOrDefault();

            int? curRank = cur.Rank;
            cur.Rank = next.Rank;
            next.Rank = curRank;

            return db.SaveChanges() > 0;
        }
    }
}
