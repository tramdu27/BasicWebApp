using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;

namespace WebApplication1.Controllers
{
    public partial class HomeController : Controller
    {
        //UserSvc svc = new UserSvc() ;
        public ActionResult Editing_Popup()
        {
            return View();
        }
    
        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult EditingPopup_Read([DataSourceRequest] DataSourceRequest request)
        {
            var users = new UserSvc().Read();


            var dataSourceResult = users.ToDataSourceResult(request);

            return Json(dataSourceResult, JsonRequestBehavior.AllowGet);


           

        }

            [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, Users users)
        {
            var userSvc = new UserSvc();
            if (users != null && ModelState.IsValid)
            {
                userSvc.Create(users);
            }

            return Json(new[] { users }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            //var userSvc = new UserSvc();

            //if (users != null && ModelState.IsValid)
            //{
            //    try
            //    {
            //        userSvc.Create(users);
            //        TempData["SuccessMessage"] = "Tạo người dùng thành công.";
            //    }
            //    catch (Exception ex)
            //    {

            //        ModelState.AddModelError("", "Lỗi khi tạo người dùng: " + ex.Message);
            //    }
            //}
            //return Json(new
            //{
            //    Data = new[] { users }.ToDataSourceResult(request, ModelState),

            //}, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, Users users)
        //{
        //    var userSvc = new UserSvc();
        //    if (users != null && ModelState.IsValid)
        //    {
        //        userSvc.Create(users);
        //    }

        //    return Json(new[] { users }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, Users users)
        {
            var userSvc = new UserSvc();
            if (users != null && ModelState.IsValid)
            {
                userSvc.Update(users);
            }

            return Json(new[] { users }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, Users users)
        {
            var userSvc = new UserSvc();
            if (users != null)
            {
                userSvc.Destroy(users);
            }

            return Json(new[] { users }.ToDataSourceResult(request, ModelState));
        }

    }
}