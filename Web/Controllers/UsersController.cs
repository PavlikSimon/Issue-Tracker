using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BL.DTO;
using BL.Facades;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        UserFacade userFacade = new UserFacade();

        [Authorize]
        public ActionResult Index()
        {
            var model = userFacade.GetAllUsers();
            return View(model);
        }

       [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            userFacade.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}