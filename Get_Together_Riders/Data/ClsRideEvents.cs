using Microsoft.Data.SqlClient;

namespace Get_Together_Riders.Data
{
    public class ClsRideEvents
    {
        public int EventID { get; set; }
        public string? EventName { get; set; }
        public string? CoverPhotoUrl { get; set; }
        public string? CoverPhotoFileName { get; set; }
        public byte[]? CoverPhotoFileContent { get; set; }
        public string? Description { get; set; }
        public string? EventCategory { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int User_ID { get; set; }
        public string? User_Name { get; set; }
        public string? RideEventPhotoUrl { get; set; }
        public string? RideEventPhotoFileName { get; set; }
        public byte[]? RideEventPhotoFileContent { get; set; }

        string connectionString = "Data Source=DESKTOP-EK5ETTJ\\SQLEXPRESS;Initial Catalog=Get_Together_Riders;Integrated Security=True";

        public List<ClsRideEvents> GetRideEvent()
        {
            List<ClsRideEvents> _listOfEvents = new List<ClsRideEvents>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetAllRideEvents", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
           
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ClsRideEvents _event = new ClsRideEvents();
                _event.EventID = Convert.ToInt32(rdr["EventID"]);
                _event.EventName = rdr["EventName"].ToString();
                _event.Description = rdr["Description"].ToString();
                _event.EventCategory = rdr["Category"].ToString();
                _event.StartDate = (DateTime)rdr["StartDate"];
                _event.EndDate = (DateTime)rdr["EndDate"];
                _event.User_ID = (int)rdr["CreatedBy"];
                _event.User_Name = rdr["UserName"].ToString();

                _listOfEvents.Add(_event);
            }
            con.Close();
            return _listOfEvents;
        }
        public List<ClsRideEvents> GetRideEventByUser(int userid)
        {
            List<ClsRideEvents> _listOfEvents = new List<ClsRideEvents>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetRideEventsByID", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", userid);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ClsRideEvents _event = new ClsRideEvents();
                _event.EventID = Convert.ToInt32(rdr["EventID"]);
                _event.EventName = rdr["EventName"].ToString();
                _event.Description = rdr["Description"].ToString();
                _event.EventCategory = rdr["Category"].ToString();
                _event.StartDate = (DateTime)rdr["StartDate"];
                _event.EndDate = (DateTime)rdr["EndDate"];
                //_event.User_ID = (int)rdr["User_ID"];
                _event.User_Name = rdr["UserName"].ToString();

                _listOfEvents.Add(_event);
            }
            con.Close();
            return _listOfEvents;
        }
        public int SaveRideEvent(ClsRideEvents _event)
        {
            int res = 0;
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("AddRideEvent", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EventID", _event.EventID);
                cmd.Parameters.AddWithValue("@EventName", _event.EventName);
                cmd.Parameters.AddWithValue("@CoverPhoto", _event.CoverPhotoUrl ?? "");
                cmd.Parameters.AddWithValue("@Description", _event.Description);
                cmd.Parameters.AddWithValue("@Category", _event.EventCategory);
                cmd.Parameters.AddWithValue("@StartDate", _event.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", _event.EndDate);
                cmd.Parameters.AddWithValue("@User_ID", _event.User_ID);
                cmd.Parameters.AddWithValue("@EventPhoto", _event.RideEventPhotoUrl ?? "");
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

        public int RegisterForEvent(ClsRideEvents _event)
        {
            int res = 0;
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("RegisterForRideEvent", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Event_ID", _event.EventID);
                cmd.Parameters.AddWithValue("@User_ID", _event.User_ID);
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
        public List<ClsUser> RideEventUsers(int id)
        {
            List<ClsUser> _listofUsers = new List<ClsUser>();
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("RideEventUsers", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Event_ID", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ClsUser _user = new ClsUser();
                    _user.UserID = Convert.ToInt32(rdr["UserID"]);
                    _user.UserName = (rdr["UserName"]).ToString();
                    _user.BikeModel = Convert.ToInt32(rdr["BikeModel"]);
                    _listofUsers.Add(_user);
                }
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
            return _listofUsers;
        }

    }
}
