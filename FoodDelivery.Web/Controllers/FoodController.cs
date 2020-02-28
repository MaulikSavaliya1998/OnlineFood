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
		public ActionResult Details(int Id)
		{
			FoodRepository foodRepository = new FoodRepository();
			Food Fooddetail = foodRepository.FoodDetails(Id);
			return View(Fooddetail);
		}
		public ActionResult Restorant()
		{
			FoodRepository FoodRepository = new FoodRepository();
			List<Menu> RestorantList = FoodRepository.RestorantList();
			foreach (var item in RestorantList)
			{
				List<FoodItem> menu = FoodRepository.RestorantMenu(item.RestorantName);
				item.FoodItemList = menu;
			}
			return View(RestorantList);
		}
	}
}