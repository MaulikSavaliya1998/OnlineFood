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
           Session["code"] = userRepository.Register(user);
            return RedirectToAction("Confirm");
        }

        public ActionResult Confirm()
        {
            return View((object)Session["code"]);
        }

        public ActionResult Activate(string code)
        {
            UserRepository userRepository = new UserRepository();
            var result = userRepository.Activate(code);
            return View(result);
        }

        [Authorize]
        public ActionResult MyAccount()
        {
            User user = Session["User"] as User;
            UserRepository userRepository = new UserRepository();

            User data = userRepository.DataRetrive(user.Id);
            data.Id = user.Id;
            return View(data);
        }

        [HttpPost]
        public ActionResult MyAccount(User usr)
        {
            User user = Session["User"] as User;

            UserRepository repository = new UserRepository();
            repository.MyAccount(usr);
            User data = repository.DataRetrive(user.Id);

            user.FirstName = data.FirstName;

            Session["User"] = user;

            return RedirectToAction("Index","Home");
        }
    }
}