using bys.activity.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bys.activity.web.Models
{
    public class HomeVM
    {
        public List<ActivityVM> Activities { get; set; }

        public bool IsAdmin { get; set; }

        public Member CurrentMember { get; set; }
        
    }

    public class ActivityVM
    {
        public Activity activity { get; set; }
        public ActivityType activityType { get; set; }
        public List<ActivityJoinInfo> JoinInfos { get; set; }
        public List<ActivityLikeInfo> LikeInfos { get; set; }
        public bool IsExpeired { get; set; }
        public bool IsJoin { get; set; }
        public bool IsLike { get; set; }
        public string TimeInfo { get; set; }
    }

}