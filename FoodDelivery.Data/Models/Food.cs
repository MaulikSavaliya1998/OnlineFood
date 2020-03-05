using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Data.Models
{
	 public class Food
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string RestorantName { get; set; }

		public double Price { get; set; }

		public string Description { get; set; }

		public int Discount { get; set; }

		public string category { get; set; }

		public double DiscountPrice
		{
			get
			{
				return (Price - (Price * Discount) / 100);
			}

		}
        public string Photo { get; set; }
    }
	public class Menu
	{

		public string RestorantName { get; set; }

		public List<FoodItem> FoodItemList { get; set; }

	}

	public class FoodItem
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public double Price { get; set; }

		public string Description { get; set; }

		public int Discount { get; set; }

		public string category { get; set; }

		public double DiscountPrice
		{
			get
			{
				return (Price - (Price * Discount) / 100);
			}

		}
        public string Photo { get; set; }
    }
}
