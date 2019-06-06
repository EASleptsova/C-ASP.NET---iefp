using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RentBikeWindowsForm
{
    public partial class Form4 : Form
    {
        int id;
        string status;
        DBconnection con = new DBconnection();
        public Form4()
        {
            InitializeComponent();
        }
        public Form4(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (con.OpenConnection() == true)
            {
                try
                {

                    string query = "SELECT * from station where id=" + id;
                    MySqlCommand cmd = new MySqlCommand(query, con.getConnection());
                    MySqlDataReader myReader = cmd.ExecuteReader();
                    while (myReader.Read())
                    {
                        string address = Convert.ToString(myReader["address"]);
                        status = Convert.ToString(myReader["status"]);
                        textBox1.Text = address;
                        if (status.Equals("active"))
                            comboBox1.SelectedIndex = 0;
                        else comboBox1.SelectedIndex = 1;
                    }
                }
                catch { }
                con.CloseConnection();
            }
            else Console.WriteLine("Connection Failed");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string address = Regex.Replace(textBox1.Text, @"\s+", " ");
            string query = "";
            if ((comboBox1.SelectedIndex == 0 && status.Equals("active")) ||
                (comboBox1.SelectedIndex == 0 && status.Equals("not active")))
            {
                query = "update station set status='active', address='" +
                    address + "' where id=" + id;
            }
            else if (comboBox1.SelectedIndex == 1 && status.Equals("not active"))
            {
                query = "update station set status='not active', address='" +
                   address + "' where id=" + id;
            }
            else if (comboBox1.SelectedIndex == 1 && status.Equals("active"))
            {
                //delete bikes
                DialogResult result = MessageBox.Show("Changing status to the 'not active'" +
                        " will also delet all the bikes. Are you sure?", "", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    if (con.OpenConnection() == true)
                    {
                        string query2 = "delete from  bike where stationId=" + id;
                        MySqlCommand cmd2 = new MySqlCommand(query2, con.getConnection());
                        cmd2.ExecuteNonQuery();
                        query = "update station set status='not active', address='" +
                        address + "' where id=" + id;
                        con.CloseConnection();
                    }                   
                }                
            }
            if (con.OpenConnection() == true)
            {
                con.updateStation(query);
                con.CloseConnection();
            }
            else Console.WriteLine("Connection Failed");
        }
    }
}

