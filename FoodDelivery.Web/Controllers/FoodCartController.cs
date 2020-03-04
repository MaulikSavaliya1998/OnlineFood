using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDelivery.Data.Models;
using FoodDelivery.Data.Repository;
using FoodDelivery.Web.Models;

namespace FoodDelivery.Web.Controllers
{
    public class FoodCartController : Controller
    {
		// GET: FoodCart
		[Authorize]
		public ActionResult Index()
        {
			User User = Session["User"] as User;
			FoodCartRepository cartRespository = new FoodCartRepository();
			List<CartItem> cartItems = cartRespository.CartItem(User.Id);

			FoodRepository repository = new FoodRepository();
			if (cartItems != null)
			{
				foreach (var item in cartItems)
				{
					Food food = repository.FoodDetails(item.FoodId);
					item.Price = food.Price;
					item.FoodName = food.Name;
					item.RestorantName = food.RestorantName;
					item.Discount = food.Discount;
					item.DiscountPrice = food.DiscountPrice;

				}
			}
			return View(cartItems);
        }
		[HttpPost]
		public ActionResult Index(List<CartItemQuantity> cartItems)
		{
			FoodCartRepository repository = new FoodCartRepository();
			User user = Session["User"] as User;
			List<ItemQuantity> items = new List<ItemQuantity>();
			foreach (var cartItem in cartItems)
			{
				ItemQuantity item = new ItemQuantity();
				item.FoodId = cartItem.FoodId;
				item.Quantity = cartItem.Quantity;
				item.UserId = user.Id;

				items.Add(item);
			}

			repository.QuantityUpdate(items);

			return RedirectToAction("Index", "FoodCart");
		}
		public ActionResult AddToCart(int id)
		{
			User user = Session["User"] as User;
			if (user != null)
			{
				FoodCartRepository cartRespository = new FoodCartRepository();
				int quantity = cartRespository.CheckProduct(user.Id, id);
				if (quantity != 0)
				{
					ItemQuantity item = new ItemQuantity();
					item.Quantity = quantity + 1;
					item.FoodId = id;
					item.UserId = user.Id;
					cartRespository.QuantityUpdate(item);
				}
				else
				{
					cartRespository.AddToCart(id, user.Id);
				}
			}
			return RedirectToAction("Index", "FoodCart");
		}
		public ActionResult DeleteCartProduct(int id)
		{
			FoodCartRepository cartRespository = new FoodCartRepository();
			cartRespository.DeleteItem(id);

			return RedirectToAction("Index", "FoodCart");
		}
	}
}