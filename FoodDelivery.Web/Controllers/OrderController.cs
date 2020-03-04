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
		[Authorize]
		public ActionResult Checkout()
		{

			User user = Session["User"] as User;
			Order order = ShoppingCartToOrder(user);
			return View(order);
		}

		private static Order ShoppingCartToOrder(User user)
		{
			FoodCartRepository cartRespository = new FoodCartRepository();
			List<CartItem> cartItems = cartRespository.CartItem(user.Id);
			Order order = new Order();
			List<OrderItem> listItem = new List<OrderItem>();
			FoodRepository repository = new FoodRepository();
			if (cartItems != null)
			{
				order.TotalItem = 0;
				order.OrderTotal = 0;
				order.TotalDiscount = 0;
				foreach (var item in cartItems)
				{
					OrderItem orderItem = new OrderItem();
					Food food = repository.FoodDetails(item.FoodId);
					order.UserId = user.Id;
					orderItem.FoodPrice = food.Price;
					orderItem.FoodName = food.Name;
					orderItem.RestorantName = food.RestorantName;
					orderItem.FoodQuantity = item.Quantity;

					if (food.Discount == 0)
					{
						orderItem.LineTotal = food.Price * item.Quantity;
					}
					else
					{
						orderItem.Discount = food.Discount;
						orderItem.DiscountPrice = orderItem.DiscountPrice;
						orderItem.LineTotal = orderItem.DiscountPrice * item.Quantity;
					}
					order.TotalItem = order.TotalItem + orderItem.FoodQuantity;
					order.OrderTotal = order.OrderTotal + orderItem.LineTotal;
					order.TotalDiscount = order.TotalDiscount + (food.Price * food.Discount * item.Quantity) / 100;
					listItem.Add(orderItem);
				}
				order.OrderItemList = listItem;

			}
			return order;
		}

		public ActionResult PlaceOrder()
		{
			User user = Session["User"] as User;
			Order order = ShoppingCartToOrder(user);
			OrderRepository repository = new OrderRepository();
			int OrderId = repository.PlaceOrder(order);
			return RedirectToAction("OrderDetail", "Order", new { Id = OrderId });
		}
	}
}