using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDelivery.Data.Models;

namespace FoodDelivery.Data.Repository
{
   public class OrderRepository
    {
		private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["FoodDelivery"].ConnectionString;

		public List<Order> GetOrderList(int Id)
		{
			List<Order> Order = null;


			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				con.Open();
				string quary = "select OrderId,OrderTotal,OrderStatus from [Order] where UserId=@id";
				SqlCommand cmd = new SqlCommand(quary, con);
				cmd.Parameters.AddWithValue("@id", Id);
				SqlDataReader sqlDataReader = cmd.ExecuteReader();
				if (sqlDataReader.HasRows)
				{
					Order = new List<Order>();
					while (sqlDataReader.Read())
					{

						Order OrderInfo = new Order();
						OrderInfo.OrderId = (int)sqlDataReader.GetValue(0);
						OrderInfo.OrderTotal = Convert.ToDouble(sqlDataReader.GetValue(1));
						OrderInfo.OrderStatus = (string)sqlDataReader.GetValue(2);
						Order.Add(OrderInfo);

					}
				}
				con.Close();
			}

			return Order;
		}

		public List<OrderItem> GetFoodName(int UserId, long OrderId)
		{
			List<OrderItem> OrderItem = new List<OrderItem>();
			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				con.Open();
				string quary = "select FoodName from [OrderItem] where UserId=@id and OrderId=@OrderId";
				SqlCommand cmd = new SqlCommand(quary, con);
				cmd.Parameters.AddWithValue("@id", UserId);
				cmd.Parameters.AddWithValue("@OrderId", OrderId);
				SqlDataReader sqlDataReader = cmd.ExecuteReader();

				while (sqlDataReader.Read())
				{
					OrderItem myorder = new OrderItem();
					myorder.FoodName = (string)sqlDataReader.GetValue(0);
					OrderItem.Add(myorder);
				}
				con.Close();
			}
			return OrderItem;
		}

		public Order GetOrderDetail(long OrderId, int userid)
		{
			Order Order = new Order();
			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				con.Open();
				string quary = "select * from [Order] where OrderId=@id and UserId=@userid";
				SqlCommand cmd = new SqlCommand(quary, con);
				cmd.Parameters.AddWithValue("@id", OrderId);
				cmd.Parameters.AddWithValue("@userid", userid);
				SqlDataReader sqlDataReader = cmd.ExecuteReader();
				if (sqlDataReader.Read())
				{
					Order.OrderId = (int)sqlDataReader.GetValue(1);
					Order.OrderTotal = Convert.ToDouble(sqlDataReader.GetValue(2));
					Order.OrderStatus = (string)sqlDataReader.GetValue(3);
					Order.OrderDate = (DateTime)sqlDataReader.GetValue(4);
					Order.TotalItem = (int)sqlDataReader.GetValue(5);
					Order.TotalDiscount = Convert.ToDouble(sqlDataReader.GetValue(6));
				}
				con.Close();
			}
			return Order;
		}

		public List<OrderItem> GetItemList(long OrderId, int UserId)
		{
			List<OrderItem> OrderItem = new List<OrderItem>();
			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				con.Open();
				string quary = "select FoodName,FoodPrice,FoodQuantity,Discount,RestorantName from [OrderItem] where UserId=@id and OrderId=@OrderId";
				SqlCommand cmd = new SqlCommand(quary, con);
				cmd.Parameters.AddWithValue("@id", UserId);
				cmd.Parameters.AddWithValue("@OrderId", OrderId);
				SqlDataReader sqlDataReader = cmd.ExecuteReader();

				while (sqlDataReader.Read())
				{
					OrderItem Orderfood = new OrderItem();
					Orderfood.FoodName = (string)sqlDataReader.GetValue(0);
					Orderfood.FoodPrice = Convert.ToDouble(sqlDataReader.GetValue(1));
					Orderfood.FoodQuantity = (int)sqlDataReader.GetValue(2);
					Orderfood.Discount = (int)sqlDataReader.GetValue(3);
					Orderfood.RestorantName = (string)sqlDataReader.GetValue(4);
					OrderItem.Add(Orderfood);
				}
				con.Close();
			}
			return OrderItem;
		}
		public void CancelOrder(long OrderId, int UserId)
		{

			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				con.Open();
				string query = "update [Order] Set OrderStatus=@status where OrderId=@id and UserId=@userid";
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@status", "Cancelled");
				cmd.Parameters.AddWithValue("@id", OrderId);
				cmd.Parameters.AddWithValue("@userid", UserId);
				cmd.ExecuteNonQuery();
				con.Close();
			}
		}
		public int PlaceOrder(Order order)
		{

			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				con.Open();
				DateTime Date = DateTime.Now;
				string Query = "insert into [Order] (UserId,OrderTotal,OrderStatus,OrderDate,TotalItem,TotalDiscount)values(@U_Id,@O_Total,@O_Status,@Date,@T_Item,@T_Discount)";
				SqlCommand Cmd = new SqlCommand(Query, con);
				Cmd.Parameters.AddWithValue("@U_Id", order.UserId);
				Cmd.Parameters.AddWithValue("@O_Total", order.OrderTotal);
				Cmd.Parameters.AddWithValue("@O_Status", "Placed");
				Cmd.Parameters.AddWithValue("@Date", Date);
				Cmd.Parameters.AddWithValue("@T_Item", order.TotalItem);
				Cmd.Parameters.AddWithValue("@T_Discount", order.TotalDiscount);
				Cmd.ExecuteNonQuery();
				Cmd.Dispose();

				string query = "select OrderId from [Order] where OrderDate=@O_Date";
				SqlCommand command = new SqlCommand(query, con);
				command.Parameters.AddWithValue("@O_Date", Date);
				SqlDataReader sqlData = command.ExecuteReader();
				if (sqlData.Read())
				{
					order.OrderId = (int)sqlData.GetValue(0);
				}
				command.Dispose();
				sqlData.Close();
				foreach (var Item in order.OrderItemList)
				{
					string Str = "insert into [OrderItem] (UserId,OrderId,FoodName,RestorantName,FoodPrice,FoodQuantity,Discount) values(@U_Id,@O_Id,@F_Name,@R_Name,@F_Price,@F_Quantity,@Discount)";
					SqlCommand comand = new SqlCommand(Str, con);
					comand.Parameters.AddWithValue("@U_Id", order.UserId);
					comand.Parameters.AddWithValue("@O_Id", order.OrderId);
					comand.Parameters.AddWithValue("@F_Name", Item.FoodName);
					comand.Parameters.AddWithValue("@R_Name", Item.RestorantName);
					comand.Parameters.AddWithValue("@F_Price", Item.FoodPrice);
					comand.Parameters.AddWithValue("@F_Quantity", Item.FoodQuantity);
					comand.Parameters.AddWithValue("@Discount", Item.Discount);
					comand.ExecuteNonQuery();

				}


				con.Close();
			}
			return order.OrderId;
		}
	}
}
