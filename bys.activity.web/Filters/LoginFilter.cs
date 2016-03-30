using bys.activity.dal;
using bys.activity.web.Utils;
using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

namespace bys.activity.web.Filter
{
    /// <summary>
    /// this class will filter all action and log to db
    /// </summary>
    public class LoginFilter: IActionFilter
    {
        private MemberDal memberDal = new MemberDal();

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string domainAlias = filterContext.HttpContext.User.Identity.Name;
            if (!memberDal.Exists(domainAlias))
            {
                InitMember(domainAlias);
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
        }

        private void InitMember(string domainAlias)
        {

            Member m = new Member()
            {
                ID = Guid.NewGuid(),
                Alias = domainAlias,
                CreateDate = DateTime.UtcNow.AddHours(8),
                AvantarPath= AvantarHelper.SaveAvantarImage(CommonUtils.GetShortName(domainAlias))
            };
            memberDal.Save(m);
        }
    }
}