using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Data.Models
{
	public class Order
	{
		public int UserId { get; set; }

		public int OrderId { get; set; }

		public Double OrderTotal { get; set; }

		public string OrderStatus { get; set; }

		public DateTime OrderDate { get; set; }

		public int TotalItem { get; set; }

		public Double TotalDiscount { get; set; }

		public List<OrderItem> OrderItemList { get; set; }

	}
	public class OrderItem
	{
		public int FoodId { get; set; }

		public string FoodName { get; set; }

		public string RestorantName { get; set; }

		public Double FoodPrice { get; set; }

		public int FoodQuantity { get; set; }

		public int Discount { get; set; }

		public double _LineTotal { get; set; }
		public double LineTotal
		{
			get
			{
				return _LineTotal;
			}
			set
			{

				_LineTotal = (double)(FoodPrice * FoodQuantity);
			}
		}


		public double _DiscountPrice;
		public double DiscountPrice
		{
			get
			{
				return _DiscountPrice;
			}
			set
			{
				_DiscountPrice = (double)(FoodPrice - (FoodPrice * Discount) / 100);
			}
		}


	}
}
