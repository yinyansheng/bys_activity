using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bys.activity.dal
{
    [Table("ba.activityJoinInfo")]
    public class ActivityJoinInfo
    {
        public Guid ID { get; set; }
        public Guid ActivityID { get; set; }
        [MaxLength(100)]
        public string MemberAlias { get; set; }
        public DateTime JoinTime { get; set; }

    }
}
