using bys.activity.dal;
using bys.activity.dal;
using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

namespace bys.activity.web.Filter
{
    /// <summary>
    /// this class will filter all action and log to db
    /// </summary>
    public class LogFilter: IActionFilter, IResultFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            GetTimer(filterContext, "action").Start();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            GetTimer(filterContext, "action").Stop();
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            GetTimer(filterContext, "render").Start();
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var renderTimer = GetTimer(filterContext, "render");
            renderTimer.Stop();

            var actionTimer = GetTimer(filterContext, "action");

            string executeTime = String.Format(
                 "Action: {0}ms, Render: {1}ms.",
                 actionTimer.ElapsedMilliseconds,
                 renderTimer.ElapsedMilliseconds
             );
            Log(filterContext, "OnResultExecuted", executeTime:executeTime);
        }

        private SysLogDal logger = new SysLogDal();

        private void Log(ControllerContext filterContext, string methodName, string executeTime)
        {
            try
            {
                string domainAlias = filterContext.HttpContext.User.Identity.Name;
                string alias = domainAlias.Split('\\')[1];
                RouteData routeData = filterContext.RouteData;

                SysLog logEntity = new SysLog();
                logEntity.Id = Guid.NewGuid();
                logEntity.UserName = alias;
                logEntity.Type = "ActionFilter";
                logEntity.MethodName = methodName;
                logEntity.ControllerName = routeData.Values["controller"].ToString();
                logEntity.ActionName = routeData.Values["action"].ToString();
                logEntity.LogTime = DateTime.Now;
                logEntity.ExecuteTime = executeTime;
                logger.Save(logEntity);

            }
            catch
            {
                //Do Nothing
            }
        }

        private Stopwatch GetTimer(ControllerContext context, string name)
        {
            string key = "__timer__" + name;
            if (context.HttpContext.Items.Contains(key))
            {
                return (Stopwatch)context.HttpContext.Items[key];
            }

            var result = new Stopwatch();
            context.HttpContext.Items[key] = result;
            return result;
        }

    }
}