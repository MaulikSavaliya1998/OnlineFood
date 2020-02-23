using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDelivery.Web.Controllers
{
    public class FoodCartController : Controller
    {
        // GET: FoodCart
        public ActionResult Index()
        {
            return View();
        }
    }
}