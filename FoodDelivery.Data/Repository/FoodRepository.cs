using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using FoodDelivery.Data.Models;

namespace FoodDelivery.Data.Repository
{
	public class FoodRepository
	{
		private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["FoodDelivery"].ConnectionString;

		public List<Food> FoodList()
		{
			List<Food> FoodItems = new List<Food>();

			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				con.Open();
				string query = "select * from [Food]";
				SqlCommand cmd = new SqlCommand(query, con);
				SqlDataReader sqlDataReader = cmd.ExecuteReader();
				while (sqlDataReader.Read())
				{
					Food Food = new Food();
					Food.Id = (int)sqlDataReader.GetValue(0);
					Food.Name = (string)sqlDataReader.GetValue(1);
					Food.RestorantName = (string)sqlDataReader.GetValue(2);
					Food.Price = Convert.ToDouble(sqlDataReader.GetValue(3));
					Food.category = (string)sqlDataReader.GetValue(6);

					if (sqlDataReader.GetValue(5) == DBNull.Value)
						Food.Discount = 0;
					else
					{
						Food.Discount = (int)sqlDataReader.GetValue(5);
					}

					FoodItems.Add(Food);
				}
				con.Close();
			}
			return FoodItems;
		}
	}
}
