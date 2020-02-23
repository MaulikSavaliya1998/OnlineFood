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
		public Food FoodDetails(int Id)
		{
			Food food = null;
			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				con.Open();
				string query = "select * from [Food] where Id=@foodid";
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@foodid", Id);
				SqlDataReader sqlDataReader = cmd.ExecuteReader();
				if (sqlDataReader.Read())
				{
					food = new Food();
					food.Id = (int)sqlDataReader.GetValue(0);
					food.Name = (string)sqlDataReader.GetValue(1);
					food.RestorantName = (string)sqlDataReader.GetValue(2);
					food.Price = Convert.ToDouble(sqlDataReader.GetValue(3));
					food.Description = (string)sqlDataReader.GetValue(4);
					if (sqlDataReader.GetValue(5) == DBNull.Value)
						food.Discount = 0;
					else
					{
						food.Discount = (int)sqlDataReader.GetValue(5);
					}

				}

				con.Close();
			}

			return food;
		}
	}
}
