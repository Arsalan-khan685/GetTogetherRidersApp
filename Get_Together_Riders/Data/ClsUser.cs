using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Get_Together_Riders.Data
{
    public class ClsUser
    {
        //static IConfiguration? Configuration { get; set; }
        //public ClsUser(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //string cs = System.Configuration.ConfigurationManager.ConnectionStrings[]    //Configuration.GetConnectionString("My_DB"); //Configuration.GetConnectionString["ConnectionStrings:NY_DB"];

        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? LoginUser_ID { get; set; }
        public string? Bio { get; set; }
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
        public string? FileName { get; set; }
        public byte[]? FileContent { get; set; }
        public string? PhoneNo { get; set; }
        public int BikeModel { get; set; }
        public string? Location { get; set; }
        public string? EmergencyContactPerson { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public int RiderNo { get; set; }
        public string? Status { get; set; }
        public int IsActive { get; set; } = 0;
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        string connectionString = "Data Source=DESKTOP-EK5ETTJ\\SQLEXPRESS;Initial Catalog=Get_Together_Riders;Integrated Security=True";

       

       
        public List<ClsUser> GetUser()
        {
            List<ClsUser> _listOfUser = new List<ClsUser>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetAllUser", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ClsUser _user = new ClsUser();
                _user.UserID = Convert.ToInt32(rdr["UserID"]);
                _user.UserName = rdr["UserName"].ToString();
                _user.Password = rdr["Password"].ToString();
                _user.Bio = rdr["Bio"].ToString();
                _user.Email = rdr["Email"].ToString();
                _user.PhoneNo = rdr["PhoneNo"].ToString();
                _user.BikeModel = (int)rdr["BikeModel"];
                _user.Location = rdr["Location"].ToString();
                _user.EmergencyContactPerson = rdr["EmergencyContactPerson"].ToString();
                _user.EmergencyContactNumber = rdr["EmergencyContactNo"].ToString();
                _user.RiderNo = (int)rdr["RiderNo"];
                //     _user.Status = rdr["IsActive"].ToString();
                _listOfUser.Add(_user);
            }
            con.Close();
            return _listOfUser;
        }

        public int SaveUser(ClsUser _user)
        {
            int res = 0;
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("AddUser", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", _user.UserID);
                cmd.Parameters.AddWithValue("@UserName", _user.UserName ?? "");
                cmd.Parameters.AddWithValue("@Password", _user.Password);
                cmd.Parameters.AddWithValue("@LoginUser_ID", _user.LoginUser_ID);
                cmd.Parameters.AddWithValue("@Bio", _user.Bio ?? "" );
                cmd.Parameters.AddWithValue("@Email", _user.Email);              
                cmd.Parameters.AddWithValue("@ImageUrl", _user.ImageUrl ??"");
                cmd.Parameters.AddWithValue("@PhoneNo", _user.PhoneNo ?? "");
                cmd.Parameters.AddWithValue("@BikeModel", _user.BikeModel);
                cmd.Parameters.AddWithValue("@Location", _user.Location ?? "");
                cmd.Parameters.AddWithValue("@EmergencyContactPerson", _user.EmergencyContactPerson ?? "");
                cmd.Parameters.AddWithValue("@EmergencyContactNo", _user.EmergencyContactNumber ?? "");
                cmd.Parameters.AddWithValue("@RiderNo", _user.RiderNo);
                cmd.Parameters.AddWithValue("@Status", _user.IsActive);
                con.Open();
                res = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Dispose();
                cmd.Dispose();
            }
               return res;

        }

        public ClsUser GetUserByID(string userId)
        {
            ClsUser user = new ClsUser();
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("GetUserById", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoginUserId", userId);
                con.Open();
                SqlDataReader reader =cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.UserID = (int)reader["UserID"];
                    user.Email = reader["Email"].ToString();
                    user.Password = reader["Password"].ToString();
                    user.LoginUser_ID = reader["LoginUser_ID"].ToString();
                    user.UserName = reader["UserName"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Dispose();
                cmd.Dispose();
            }
            return user;
        }
    }
}
