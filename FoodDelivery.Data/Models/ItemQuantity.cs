using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Data.Models
{
	public class ItemQuantity
	{
		public int FoodId { get; set; }

		public int Quantity { get; set; }

		public int UserId { get; set; }
	}
}
