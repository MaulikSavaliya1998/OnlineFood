using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDelivery.Data.Models;
using FoodDelivery.Data.Repository;


namespace FoodDelivery.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult UserLogin()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult UserLogin(Login login)
        {
            UserRepository userRepository = new UserRepository();
            userRepository.UserLogin(login);
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.UserInfo user)
        {
            UserRepository userRepository = new UserRepository();
            userRepository.Register(user);
            return View();
        }
    }
}