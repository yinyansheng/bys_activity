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
            return db.ActivityTypes.ToList();
        }

        public bool Save(ActivityType a)
        {
            BADBContext db = new BADBContext();
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
                }
                db.SaveChanges();
            }
        }

    }
}
