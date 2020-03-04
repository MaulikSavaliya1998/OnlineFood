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
   public class FoodCartRepository
    {
		private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["FoodDelivery"].ConnectionString;
		public List<CartItem> CartItem(int id)
		{

			List<CartItem> carts = null;

			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				con.Open();

				string query = "select FoodId,Quantity from [CartItem] where UserId=@id";
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@id", id);
				SqlDataReader dataReader = cmd.ExecuteReader();
				if (dataReader.HasRows)
				{
					carts = new List<CartItem>();
					while (dataReader.Read())
					{
						CartItem cart = new CartItem();
						cart.FoodId = (int)dataReader.GetValue(0);
						cart.Quantity = (int)dataReader.GetValue(1);

						carts.Add(cart);

					}
				}
				dataReader.Close();
				con.Close();
			}
			return carts;
		}
		public void AddToCart(int FoodId, int userId)
		{
			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				con.Open();
				string query = "insert into [CartItem](UserId,FoodId,Quantity) values(@userId,@FoodId,1)";
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@UserId", userId);
				cmd.Parameters.AddWithValue("@FoodId", FoodId);
				cmd.ExecuteNonQuery();
				con.Close();
			}
		}
		public void AddToCart()
		{
			throw new NotImplementedException();
		}
		public void DeleteItem(int Id)
		{
			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				con.Open();
				string sql = "Delete from CartItem where FoodId=@id";
				SqlCommand cmd = new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@id", Id);
				cmd.ExecuteNonQuery();
				con.Close();
			}
		}

		public void QuantityUpdate(ItemQuantity item)
		{

			this.QuantityUpdate(new List<ItemQuantity>() { item });
		}

		public void QuantityUpdate(List<ItemQuantity> items)
		{
			string quary = "update [CartItem] set Quantity=@quantity where FoodId=@id and UserId = @userId";

			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				con.Open();
				foreach (var item in items)
				{
					SqlCommand cmd = new SqlCommand(quary, con);
					cmd.Parameters.AddWithValue("@quantity", item.Quantity);
					cmd.Parameters.AddWithValue("@id", item.FoodId);
					cmd.Parameters.AddWithValue("@userId", item.UserId);
					cmd.ExecuteNonQuery();
				}

				con.Close();
			}
		}
		public int CheckProduct(int userId, int FoodId)
		{
			int Quantity = 0;
			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				con.Open();

				string quary = "select Quantity from [CartItem] where UserId=@UI and FoodId=@PI";
				SqlCommand cmd = new SqlCommand(quary, con);
				cmd.Parameters.AddWithValue("@UI", userId);
				cmd.Parameters.AddWithValue("@PI", FoodId);
				SqlDataReader dataReader = cmd.ExecuteReader();
				if (dataReader.Read())
				{
					Quantity = (int)dataReader.GetValue(0);
				}
				con.Close();
			}
			return Quantity;
		}
	}
}
