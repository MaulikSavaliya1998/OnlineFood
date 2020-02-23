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

        public string Register(User user)
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
            return code;
        }
        public User UserLogin(Login login)
        {
            User user = null;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string quary = "select iUserId,vFirstName,vEmailId,IsActive from [User] where vEmailId = @username and vPassword = @password";
                SqlCommand cmd = new SqlCommand(quary, con);
                cmd.Parameters.AddWithValue("@username", login.UserName);
                cmd.Parameters.AddWithValue("@password", login.Password);
                SqlDataReader userReader = cmd.ExecuteReader();
                if(userReader.Read())
                {
                    user = new User();
                    user.Id = (int)userReader.GetValue(0);
                    user.FirstName = (string)userReader.GetValue(1);
                    user.Email = (string)userReader.GetValue(2);
                    user.IsActive = (bool)userReader.GetValue(3);
                }
                con.Close();
            }
            return user;
        }

        public ActivationResult Activate(string code)
        {
            ActivationResult activationResult = new ActivationResult();
            activationResult.IsSuccess = false;

            if(!string.IsNullOrEmpty(code))
            {
                SqlCommand cmd = null;
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    string sql = "update [User] set IsActive=1 where vCode=@code";
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@code",code);
                    if(cmd.ExecuteNonQuery() == 1)
                    {
                        activationResult.IsSuccess = true;
                        string Quary = "select vFirstName from [User] where vCode=@code";
                        cmd = new SqlCommand(Quary, con);
                        cmd.Parameters.AddWithValue("@code", code);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if(sdr.Read())
                        {
                            activationResult.FirstName = (string)sdr.GetValue(0);
                        }
                        sdr.Close();
                    }
                    cmd.Dispose();
                    con.Close();
                }
            }

            return activationResult;
        }

        public User DataRetrive(int id)
        {
            User user = null;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string Quary = "select vFirstName,vLastName,vEmailId,nGender,vMobile from [User] where iUserId = @id";
                SqlCommand cmd = new SqlCommand(Quary,con);
                cmd.Parameters.AddWithValue("@id",id);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if(dataReader.Read())
                {
                    user = new User();
                    user.FirstName = (string)dataReader.GetValue(0);
                    user.LastName = (string)dataReader.GetValue(1);
                    user.Email = (string)dataReader.GetValue(2);
                    if (dataReader.GetValue(3) == DBNull.Value)
                        user.Gender = null;
                    else
                        user.Gender = (string)dataReader.GetValue(3);
                    if (dataReader.GetValue(4) == DBNull.Value)
                        user.Mobile = null;
                    else
                        user.Mobile = (string)dataReader.GetValue(4);
                }
                con.Close();
            }
                return user;
        }

        public  void MyAccount(User user)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string Quary = "update [User] set vFirstName = @fn,vLastName = @ln,nGender = '" + user.Gender + "',vMobile='" + user.Mobile + "' where iUserId=@Id";
                SqlCommand cmd = new SqlCommand(Quary, con);
                cmd.Parameters.AddWithValue("@fn", user.FirstName);
                cmd.Parameters.AddWithValue("@ln", user.LastName);
                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
