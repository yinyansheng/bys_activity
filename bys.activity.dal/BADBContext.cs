using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bys.activity.dal
{
    public class BADBContext:DbContext
    {
        public BADBContext()
            : base("BysActivity")
        {
            Database.SetInitializer<BADBContext>(new BADBContextInitializer());
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityJoinInfo> ActivityJoinInfos { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<SysLog> SysLogs { get; set; }
        public DbSet<ActivityLikeInfo> ActivityLikeInfos { get; set; }

        
    }

    public class BADBContextInitializer : CreateDatabaseIfNotExists<BADBContext>
    {
        public override void InitializeDatabase(BADBContext context)
        {
            base.InitializeDatabase(context);
        }
    }
}
