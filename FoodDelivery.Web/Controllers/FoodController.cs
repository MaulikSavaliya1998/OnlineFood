using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDelivery.Data.Models;
using FoodDelivery.Data.Repository;

namespace FoodDelivery.Web.Controllers
{
	public class FoodController : Controller
	{

		public ActionResult Dishlist()
		{
			FoodRepository FoodRepository = new FoodRepository();

			List<Food> DishLists = FoodRepository.FoodList();
			return View(DishLists);
		}
	}
}