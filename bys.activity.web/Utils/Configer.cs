using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace bys.activity.web.Utils
{
    public class Configer
    {
        public static ISet<string> Administrators
        {
            get
            {
                return GetAdministrators();
            }
        }

        private static ISet<string> GetAdministrators()
        {
            string Administrators = ConfigurationManager.AppSettings["Administrators"].ToString();
            ISet<string> admins = new HashSet<string>();
            foreach (var item in Administrators.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                admins.Add(item.Trim());
            }
            return admins;
        }
    }
}