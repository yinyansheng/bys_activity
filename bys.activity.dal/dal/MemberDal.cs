using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bys.activity.dal
{
    public class MemberDal
    {
        public bool Save(Member m)
        {
            BADBContext db = new BADBContext();
            db.Members.Add(m);
            return db.SaveChanges() > 0;
        }

        public bool Exists(string domainAlias)
        {
            BADBContext db = new BADBContext();
            return db.Members.Where(r => r.Alias.Equals(domainAlias)).Count()>0;
        }

        public IList<Member> GetByCondition(Func<Member, bool> condition)
        {
            using (BADBContext db = new BADBContext())
            {
                return db.Members.Where(condition).ToList();
            }
        }

        public IEnumerable<Member> GetAll()
        {
            BADBContext db = new BADBContext();
            return db.Members.OrderByDescending(r => r.CreateDate).ToList();
        }

        public void Edit(Member m)
        {
            using (BADBContext db = new BADBContext())
            {
                var old = db.Members.Where(r => r.ID == m.ID).FirstOrDefault();
                if (null != old)
                {
                    old.DisplayName = m.DisplayName;
                    old.AvantarPath = m.AvantarPath;
                    old.CreateDate = DateTime.UtcNow.AddHours(8);
                }
                db.SaveChanges();
            }
        }

        public bool Delete(Guid memberID)
        {
            BADBContext db = new BADBContext();
            var info = db.Members.Where(r => r.ID == memberID).FirstOrDefault();
            db.Members.Remove(info);
            return db.SaveChanges() > 0;
        }
    }
}
