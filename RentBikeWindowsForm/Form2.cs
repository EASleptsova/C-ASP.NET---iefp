using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RentBikeWindowsForm
{
    public partial class Form2 : Form
    {
        List<int> list = new List<int>();
        List<int> bikes = new List<int>();
        int index;
        DBconnection con = new DBconnection();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            inicializa_comboBox();
        }
        public void inicializa_comboBox()
        {
            list.Clear();
            bikes.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            if (con.OpenConnection() == true)
            {
                try
                {
                    string query = "SELECT s.id, s.address, COUNT(b.stationId) AS numOfBikes " +
               "FROM station s left JOIN bike b ON s.id = b.stationId " +
               "where s.status='active' GROUP BY s.id";
                    MySqlCommand cmd = new MySqlCommand(query, con.getConnection());
                    MySqlDataReader myReader = cmd.ExecuteReader();
                    while (myReader.Read())
                    {
                        string address = Convert.ToString(myReader["address"]);
                        string numofBikes = Convert.ToString(myReader["numOfBikes"]);
                        int id = Convert.ToInt32(myReader["id"]);
                        string message = address + "  Nº bikes: " + numofBikes;
                        comboBox1.Items.Add(message);
                        comboBox2.Items.Add(message);
                        comboBox3.Items.Add(message);
                        comboBox4.Items.Add(message);
                        list.Add(id);
                        bikes.Add(int.Parse(numofBikes));
                    }
                    con.CloseConnection();
                }
                catch (Exception ex)
                { Console.WriteLine(ex); }
            }
            else
                Console.WriteLine("Connection Failed");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int numOfBikes = 0;
            index = comboBox1.SelectedIndex;
            int id;
            if (index >= 0)
            {
                id = list[index];
                try
                {
                    numOfBikes = int.Parse(textBox1.Text);
                    if (numOfBikes > 0)
                    {
                        if (con.OpenConnection() == true)
                        {
                            con.addBikes(numOfBikes, id);
                            con.CloseConnection();
                            inicializa_comboBox();
                            textBox1.Text = "";

                        }
                        else
                            Console.WriteLine("Connection Failed");
                    }
                    else
                    {
                        MessageBox.Show("A positive number is expected!");
                    }
                }
                catch { MessageBox.Show("A number is expected!"); }
            }
            else MessageBox.Show("no station is selected");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int numOfBikes = 0;
            index = comboBox2.SelectedIndex;
            if (index >= 0)
            {
                int id = list[index];
                try
                {
                    numOfBikes = int.Parse(textBox2.Text);
                    if (numOfBikes > 0 && bikes[index] >= numOfBikes)
                    {
                        if (con.OpenConnection() == true)
                        {
                            string message = con.deleteBikes(numOfBikes, id);
                            con.CloseConnection();
                            if (message.Equals("ready"))
                            {
                                inicializa_comboBox();
                                MessageBox.Show("Operation succeed!");
                            }
                            textBox2.Text = "";
                        }
                        else
                            Console.WriteLine("Connection Failed");
                    }
                    else
                    {
                        MessageBox.Show("Invalid number of bikes!");
                        textBox2.Text = "";
                    }
                }
                catch { MessageBox.Show("A number is expected!"); }
            }
            else { MessageBox.Show("no station is selected"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int numOfBikes = 0;
            index = comboBox3.SelectedIndex;
            int index1 = comboBox4.SelectedIndex;
            if (index >= 0 && index1 >= 0)
            {
                int id = list[index];
                int id1 = list[index1];
                try
                {
                    numOfBikes = int.Parse(textBox3.Text);
                    if (numOfBikes > 0 && bikes[index] >= numOfBikes)
                    {
                        if (con.OpenConnection() == true)
                        {
                            con.deleteBikes(numOfBikes, id);
                            con.addBikes(numOfBikes, id1);
                            con.CloseConnection();
                            inicializa_comboBox();
                            textBox3.Text = "";
                        }
                        else
                            Console.WriteLine("Connection Failed");
                    }
                    else
                    {
                        MessageBox.Show("Invalid number of bikes!");
                        textBox2.Text = "";
                    }
                }
                catch { MessageBox.Show("A number is expected!"); }
            }
            else
            { MessageBox.Show("no station is selected"); }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
