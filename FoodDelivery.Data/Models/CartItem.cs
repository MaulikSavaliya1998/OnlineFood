using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FoodDelivery.Data.Models
{
	public class CartItem
	{
		public int UserId { get; set; }

		public int FoodId { get; set; }

		public string FoodName { get; set; }

		public string RestorantName { get; set; }

		public double Price { get; set; }


		public int Quantity { get; set; }

		public int Discount { get; set; }

		public double _DiscountPrice;
		public double DiscountPrice
		{
			get
			{
				return _DiscountPrice;
			}
			set
			{
				_DiscountPrice = (Double)(Price - (Price * Discount) / 100);
			}
		}
        public string Photo { get; set; }
        public double _QuantityPrice;
		public double QuantityPrice
		{
			get
			{
				return _QuantityPrice;
			}
			set
			{
				if (Discount == 0)
				{
					_QuantityPrice = Price * Quantity;
				}
				else
				{
					_QuantityPrice = (double)(DiscountPrice * Quantity);
				}
			}
		}


	}


}