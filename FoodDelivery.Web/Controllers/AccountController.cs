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
            User user = userRepository.UserLogin(login);
            if(user != null)
            {
                if(user.IsActive.Equals(true))
                {
                    string returnUrl = this.Request.QueryString["ReturnUrl"];
                    Session["User"] = user;
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            login.IsError = true;
            if(user == null)
            {
                ViewData["message"] = "UserName and Password is Invalid";
            }

            return View(login);
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.UserInfo user)
        {
            UserRepository userRepository = new UserRepository();
           // userRepository.Register(user);
            return View();
        }
    }
}