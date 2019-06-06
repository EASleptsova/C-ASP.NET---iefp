using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace RentBikeWindowsForm
{
    class DBconnection
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public MySqlConnection getConnection()
        {
            return connection;
        }

        public void insertStation(string address)
        {
            try
            {
                string query = "insert into station (address, status) values ('" + address + "', 'active')";
                MySqlCommand cmd = new MySqlCommand(query, getConnection());
                cmd.ExecuteNonQuery();
                //long id = cmd.LastInsertedId;                           
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void updateStation(string query)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, getConnection());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Operation succeed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }       
        }

        public MySqlDataReader listStations()
        {
            string query = "SELECT s.id, s.address, COUNT(b.stationId) AS numOfBikes " +
                "FROM station s left JOIN bike b ON s.id = b.stationId " +
                "WHERE s.status='active' GROUP BY s.id";

            MySqlCommand cmd = new MySqlCommand(query, getConnection());
            MySqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            return myReader;
        }

        public MySqlDataReader listAllStations()
        {
            string query = "SELECT s.id, s.address, s.status, COUNT(b.stationId) AS numOfBikes " +
                "FROM station s left JOIN bike b ON s.id = b.stationId " +
                "GROUP BY s.id";

            MySqlCommand cmd = new MySqlCommand(query, getConnection());
            MySqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            return myReader;
        }

        public MySqlDataReader listRents()
        {
            string query = "SELECT r.id, r.startDate, r.endDate, r.STATUS, u.NAME," +
            " s.address AS destination, s1.address AS origin FROM rent r" +
            " LEFT join user u ON u.id = r.userId" +
            " left join station s ON s.id = r.destinationId" +
            " left join station s1 ON s1.id = r.startStationId ORDER BY r.id";
            MySqlCommand cmd = new MySqlCommand(query, getConnection());
            MySqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            return myReader;
        }
        public MySqlDataReader listActiveRents()
        {
            string query = "SELECT r.id, r.startDate, r.endDate, r.STATUS, u.NAME," +
            " s.address AS destination, s1.address AS origin FROM rent r" +
            " LEFT join user u ON u.id = r.userId" +
            " left join station s ON s.id = r.destinationId" +
            " left join station s1 ON s1.id = r.startStationId WHERE r.STATUS = 'active'";
            MySqlCommand cmd = new MySqlCommand(query, getConnection());
            MySqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            return myReader;
        }
        public MySqlDataReader listUserRents(int id)
        {
            string query = "SELECT r.id, r.startDate, r.endDate, r.STATUS, u.NAME," +
            " s.address AS destination, s1.address AS origin FROM rent r" +
            " LEFT join user u ON u.id = r.userId" +
            " left join station s ON s.id = r.destinationId" +
            " left join station s1 ON s1.id = r.startStationId WHERE u.id = " + id;
            MySqlCommand cmd = new MySqlCommand(query, getConnection());
            MySqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            return myReader;
        }

        public void addBikes(int numBikes, int id)
        {
            try
            {
                for (int i = 0; i < numBikes; i++)
                {
                    string query = "insert into bike (stationId) values (" + id + ")";
                    MySqlCommand cmd = new MySqlCommand(query, getConnection());
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Operation succeed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public string deleteBikes(int numBikes, int id)
        {
            string message = "";
            try
            {
                for (int i = 0; i < numBikes; i++)
                {
                    string query = "delete from bike where stationId =" + id + " limit 1";
                    MySqlCommand cmd = new MySqlCommand(query, getConnection());
                    cmd.ExecuteNonQuery();
                }
                message = "ready";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                message = ex.ToString();
            }
            return message;
        }
    }
}
