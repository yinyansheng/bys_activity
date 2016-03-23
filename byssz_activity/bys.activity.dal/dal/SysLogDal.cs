using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bys.activity.dal
{
    public class SysLogDal
    {
        public bool Save(SysLog log)
        {
            BADBContext db = new BADBContext();
            db.SysLogs.Add(log);
            return db.SaveChanges() > 0;
        }
    }
}
