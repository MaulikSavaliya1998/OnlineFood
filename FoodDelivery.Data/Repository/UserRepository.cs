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
    public class UserRepository
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["FoodDelivery"].ConnectionString;

        public void Register(User user)
        {
            string code;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                Guid guidcode = Guid.NewGuid();
                code = guidcode.ToString();
                Console.Write("connection open");

                string sql = "insert into [User] (vFirstName,vLastName,vEmailId,vPassword,vCode) values(@FName,@LName,@Email,@Password,@Code)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@FName", user.FirstName);
                cmd.Parameters.AddWithValue("@LName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Code", code);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void UserLogin(Login login)
        {

        }
    }
}
