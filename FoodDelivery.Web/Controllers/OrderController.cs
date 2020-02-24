using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDelivery.Data.Models;
using FoodDelivery.Data.Repository;

namespace FoodDelivery.Web.Controllers
{
    public class OrderController : Controller
    {
		// GET: Order
		[Authorize]
		public ActionResult Index()
		{
			User User = Session["User"] as User;
			OrderRepository OrderRepository = new OrderRepository();
			List<Order> OrderList = OrderRepository.GetOrderList(User.Id);
			if (OrderList != null)
			{
				foreach (var item in OrderList)
				{
					List<OrderItem> ProductName = OrderRepository.GetFoodName(User.Id, item.OrderId);
					item.OrderItemList = ProductName;
				}
			}

			return View(OrderList);
		}

		[Authorize]
		public ActionResult OrderDetail(int Id)
		{
			User User = Session["User"] as User;
			OrderRepository OrderRepository = new OrderRepository();
			Order OrderDetail = OrderRepository.GetOrderDetail(Id, User.Id);
			if (OrderDetail != null)
			{
				List<OrderItem> OrderItem = OrderRepository.GetItemList(Id, User.Id);
				foreach (var item in OrderItem)
				{
					item.DiscountPrice = item.DiscountPrice;
					item.LineTotal = item.LineTotal;

				}

				OrderDetail.OrderItemList = OrderItem;
			}
			return View(OrderDetail);
		}
		[Authorize]
		public ActionResult CancelOrder(int Id)
		{

			User User = Session["User"] as User;
			OrderRepository OrderRepository = new OrderRepository();
			OrderRepository.CancelOrder(Id, User.Id);
			return RedirectToAction("OrderDetail", new { Id = Id });
		}
	}
}