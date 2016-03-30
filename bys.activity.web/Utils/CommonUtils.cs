using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bys.activity.web.Utils
{
    public class CommonUtils
    {
        public static string GetTimeInfo(DateTime s, DateTime e)
        {

            if (s.Date == e.Date)
            {
                return string.Format("{0}/{1} {2} {3}-{4}", s.Month, s.Day, s.DayOfWeek, s.Hour + ":" + s.Minute, e.Hour + ":" + e.Minute);
            }

            return s.ToString("MM/dd HH:mm") + " -" + e.ToString("MM/dd HH:mm");
        }

        public static string GetShortName(string name)
        {
            var names = name.Split('\\');
            if (name.Length > 1)
                return names[1];
            return name;
        }

        public static string GetNavBarLiClass(string controller,string target)
        {
            if (controller.Equals(target, StringComparison.OrdinalIgnoreCase))
                return "active";
            return "";
            
        }
    }
}