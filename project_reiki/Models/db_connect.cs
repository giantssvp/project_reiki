using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace project_reiki.Models
{
    public class db_connect
    {
        private MySqlConnection connection;
        public List<string>[] list_feedback_show = new List<string>[3];
        public List<string>[] list_time_show = new List<string>[1];
        public List<string>[] list_gallery_show = new List<string>[2];

        private bool OpenConnection()
        {
            string connetionString = null;
            connetionString = "server=localhost;database=reiki_healing;uid=root;pwd=password;Allow User Variables=True;";
            connection = new MySqlConnection(connetionString);
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public int Insert(string name, string email, string sub, string msg)
        {
            try
            {
                int id = -1;
                string query = "INSERT INTO testimony (Name, Email_id, Subject, Message, Status, Date) VALUES(@name, @email, @sub, @msg, @sts, CURDATE())";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@sub", sub);
                    cmd.Parameters.AddWithValue("@msg", msg);
                    cmd.Parameters.AddWithValue("@sts", false);

                    cmd.ExecuteNonQuery();

                    MySqlCommand cmd1 = new MySqlCommand("SELECT LAST_INSERT_ID()", connection);
                    id = Convert.ToInt32(cmd1.ExecuteScalar());

                    this.CloseConnection();
                }
                return id;
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return -1;
            }
        }

        public int Insert_Booking(string start_time, string session_type, string date)
        {
            try
            {
                int id = -1;
                string query = "INSERT INTO booking (Start_time, Session_type, Date) VALUES(@time, @session, @dt)";
                
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@time", start_time);
                    cmd.Parameters.AddWithValue("@session", session_type);
                    cmd.Parameters.AddWithValue("@dt", date);
                    cmd.ExecuteNonQuery();

                    MySqlCommand cmd1 = new MySqlCommand("SELECT LAST_INSERT_ID()", connection);
                    id = Convert.ToInt32(cmd1.ExecuteScalar());

                    this.CloseConnection();
                }
                return id;
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return -1;
            }
        }

        public List<string>[] testimony_show(int offset)
        {
            try
            {
                //string query = "SELECT * FROM testimony ORDER BY Date DESC, ID DESC LIMIT 2 OFFSET @offset";
                string query = "SELECT * FROM testimony";

                list_feedback_show[0] = new List<string>();
                list_feedback_show[1] = new List<string>();
                list_feedback_show[2] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_feedback_show[0].Add(dataReader["Subject"] + "");
                        list_feedback_show[1].Add(dataReader["Message"] + "");
                        list_feedback_show[2].Add(dataReader["Date"] + "");
                    }

                    dataReader.Close();
                    this.CloseConnection();
                    return list_feedback_show;
                }
                else
                {
                    return list_feedback_show;
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return list_feedback_show;
            }
        }

        public List<string>[] time_slot_show(string date)
        {
            try
            {
                string query = "SELECT start_time FROM booking where date = @date";

                list_time_show[0] = new List<string>();
                //list_feedback_show[1] = new List<string>();
                //list_feedback_show[2] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@date", date);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_time_show[0].Add(dataReader["start_time"] + "");
                    }

                    dataReader.Close();
                    this.CloseConnection();
                    return list_time_show;
                }
                else
                {
                    return list_time_show;
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return list_time_show;
            }
        }

    } //db_connect class
} // namespace

