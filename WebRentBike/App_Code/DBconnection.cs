using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
// check unused methods
public class DBconnection
{  
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        public DBconnection()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "rentbike";
            uid = "root";
            password = "123456";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database +
                ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

        }
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }
        public MySqlConnection getConnection()
        {
            return connection;
        }

        public void addUser(string login, string password, string name, int nif, string contact)
        {
        if (this.OpenConnection() == true)
        {
            try
            {
                string query = "insert into user (name,nif, contact,login,password) values ('" + name + "'," +
                nif + ",'" + contact + "','" + login + "','" + password + "')";
                MySqlCommand cmd = new MySqlCommand(query, getConnection());
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                //long id = cmd.LastInsertedId;                           
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        else { Console.WriteLine("Connection Failed"); }
        }
    public bool uniqueUsername(string login)
    {
        if (this.OpenConnection() == true)
        {
            try
            {
                string query = "select login from user where login ='" + login + "'";
                MySqlCommand cmd = new MySqlCommand(query, getConnection());
                MySqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string username = Convert.ToString(myReader["login"]);
                    if (username.Equals(login))
                    {
                        this.CloseConnection();
                        return false;
                    }
                }
                this.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return true;
            }
        } else { Console.WriteLine("Connection Failed");
            return true;
        }
    }
        public MySqlDataReader listStations()
        {
            MySqlDataReader myReader = null;
            if (this.OpenConnection() == true)
            {
                try
                {
                    string query = "SELECT s.id, s.address, COUNT(b.stationId) AS numOfBikes " +
                        "FROM station s left JOIN bike b ON s.id = b.stationId where s.status='active' " +
                        "GROUP BY s.id";
                    MySqlCommand cmd = new MySqlCommand(query, getConnection());
                    myReader = cmd.ExecuteReader();              
                return myReader;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);               
                return myReader;
                }
            }
            else
            {
                Console.WriteLine("Connection Failed");
                return myReader;
            }
        }
    public string  addRent(int id, int station)
    {
        string result = "";
        if (this.OpenConnection() == true)
        {
            try
            {
                int bike=-1;
                string query1 = "select id from bike where stationId=" + station+" limit 1";
                MySqlCommand cmd = new MySqlCommand(query1, getConnection());
                MySqlDataReader myReader=cmd.ExecuteReader();
                while (myReader.Read())
                {
                    bike = Convert.ToInt32(myReader["id"]);                  
                }
                this.CloseConnection();
                this.OpenConnection();

                string query2 = "delete from bike where id=" + bike;
                MySqlCommand cmd2 = new MySqlCommand(query2, getConnection());
                cmd2.ExecuteNonQuery();               
                DateTime data = DateTime.Now;
                string formatForMySql = data.ToString("yyyy-MM-dd HH:mm:ss");
                this.CloseConnection();
                this.OpenConnection();

                string query;
                if (bike > 0) {
                  query  = "insert into rent (bikeId,startStationId, userId,startDate,status)" +
                        " values (" + bike + "," + station + "," + id + ",'" + formatForMySql + "','active')";
                    MySqlCommand cmd1 = new MySqlCommand(query, getConnection());
                    cmd1.ExecuteNonQuery();
                    result = "ready";                   
                }
                else { result = "no bikes available"; }
                this.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        else { Console.WriteLine("Connection Failed"); }
        return result;
    }
    public void finishRent(int id, int destination)
    {
        if (this.OpenConnection() == true)
        {
            try
            {
                int bike = -1;
                string query1 = "select bikeId from rent where userId=" +
                    id + " and status='active'";
                MySqlCommand cmd1 = new MySqlCommand(query1, getConnection());
                MySqlDataReader myReader = cmd1.ExecuteReader();
                while (myReader.Read())
                {
                    bike = Convert.ToInt32(myReader["bikeId"]);
                }               
                this.CloseConnection();
                this.OpenConnection();

                DateTime data = DateTime.Now;
                string formatForMySql = data.ToString("yyyy-MM-dd HH:mm:ss");
                string query = "update rent set status='finished' , endDate='"+ 
                    formatForMySql+"' , destinationId="+ 
                    destination+" where userId=" + id + " and status='active'";
                MySqlCommand cmd = new MySqlCommand(query, getConnection());
                cmd.ExecuteNonQuery();               
                this.CloseConnection();
                this.OpenConnection();

                string query2 = "insert into bike values ("+bike+","+destination+")";
                MySqlCommand cmd2 = new MySqlCommand(query2, getConnection());
                cmd2.ExecuteNonQuery();               
                this.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        else { Console.WriteLine("Connection Failed"); }
    }
}

