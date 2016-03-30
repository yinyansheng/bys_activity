using bys.activity.dal;
using bys.activity.web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bys.activity.web.Areas.Backend.Controllers
{
    public class MemberController : Controller
    {
        private MemberDal memberDal = new MemberDal();

        [AdminAuthorize]
        public ActionResult Index()
        {
            return View(memberDal.GetAll());
        }

        [AdminAuthorize]
        public ActionResult Edit(Guid MemberID)
        {
            Member model = memberDal.GetByCondition(delegate (Member m)
            {
                return m.ID.Equals(MemberID);
            }).FirstOrDefault();

            return View(model);
        }

        [AdminAuthorize]
        public ActionResult SubmitEdit(Member m)
        {
            memberDal.Edit(m);
            return RedirectToAction("Index");
        }

        [AdminAuthorize]
        public ActionResult Delete(Guid MemberID)
        {
            memberDal.Delete(MemberID);
            return RedirectToAction("Index");
        }
    }
}