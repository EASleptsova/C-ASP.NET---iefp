using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RentBikeWindowsForm
{
    public partial class Form3 : Form
    {
        List<int> list = new List<int>();
        List<int> list1 = new List<int>();
        int index;
        DBconnection con = new DBconnection();
        public Form3()
        {
            InitializeComponent();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }
        public void setup_dataGridView()
        {
            dataGridView1.ColumnCount = 6;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font =
                new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView1.Name = "Rents";
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.GridColor = Color.Black;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].Name = "status";
            dataGridView1.Columns[1].Name = "user";
            dataGridView1.Columns[2].Name = "origin";
            dataGridView1.Columns[3].Name = "destination";
            dataGridView1.Columns[4].Name = "start date";
            dataGridView1.Columns[5].Name = "end date";
            dataGridView1.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor =
                Color.Beige;
        }
        public void CarregarGV()
        {
            if (con.OpenConnection() == true)
            {
                try
                {
                    MySqlDataReader myReader = con.listRents();
                    dataGridView1.Rows.Clear();
                    list.Clear();
                    while (myReader.Read())
                    {
                        int id = Convert.ToInt32(myReader["id"]);
                        DateTime startDate = Convert.ToDateTime(myReader["startDate"]);
                        string status = Convert.ToString(myReader["status"]);
                        string name = Convert.ToString(myReader["name"]);
                        string origin = Convert.ToString(myReader["origin"]);
                        string destination, endDate;
                        if (status.Equals("active")) { destination = ""; endDate = ""; }
                        else
                        {
                            endDate = Convert.ToDateTime(myReader["endDate"]).ToString("yyyy-MM-dd");
                            destination = Convert.ToString(myReader["destination"]);
                        }                       
                        string[] row = { status, name, origin, destination,
                            startDate.ToString("yyyy-MM-dd"), endDate};
                        dataGridView1.Rows.Add(row);
                        list.Add(id);
                    }
                    con.CloseConnection();
                }
                catch
                { con.CloseConnection();}
            }
            else
                Console.WriteLine("Connection Failed");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            CarregarGV();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            setup_dataGridView();
            CarregarGV();
            inicializa_comboBox();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (con.OpenConnection() == true)
            {
                try
                {
                    MySqlDataReader myReader = con.listActiveRents();
                    dataGridView1.Rows.Clear();
                    list.Clear();
                    while (myReader.Read())
                    {
                        int id = Convert.ToInt32(myReader["id"]);
                        DateTime startDate = Convert.ToDateTime(myReader["startDate"]);
                        string endDate = ""; string destination = "";
                        string status = Convert.ToString(myReader["status"]);
                        string name = Convert.ToString(myReader["name"]);                       
                        string origin = Convert.ToString(myReader["origin"]);
                        string[] row = { status, name, origin, destination,
                            startDate.ToString("yyyy-MM-dd"), endDate};
                        dataGridView1.Rows.Add(row);
                        list.Add(id);
                    }
                    con.CloseConnection();
                }
                catch
                { con.CloseConnection(); }
            }
            else
                Console.WriteLine("Connection Failed");
        }
        public void inicializa_comboBox()
        {
            list1.Clear();
            toolStripComboBox1.Items.Clear();
            if (con.OpenConnection() == true)
            {
                try
                {
                    string query = "select id, name from user";
                    MySqlCommand cmd = new MySqlCommand(query, con.getConnection());
                    MySqlDataReader myReader = cmd.ExecuteReader();
                    while (myReader.Read())
                    {
                        string name = Convert.ToString(myReader["name"]);
                        int id = Convert.ToInt32(myReader["id"]);
                        string message = id + "  <<" + name + ">>";
                        toolStripComboBox1.Items.Add(message);
                        list1.Add(id);
                    }
                    con.CloseConnection();
                }
                catch
                { con.CloseConnection(); }
            }
            else
                Console.WriteLine("Connection Failed");
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con.OpenConnection() == true)
            {
                try
                {
                    index = toolStripComboBox1.SelectedIndex;
                    int id1 = list1[index];
                    MySqlDataReader myReader = con.listUserRents(id1);
                    dataGridView1.Rows.Clear();                   
                    while (myReader.Read())
                    {
 //                       int id = Convert.ToInt32(myReader["id"]);
                        DateTime startDate = Convert.ToDateTime(myReader["startDate"]);                       
                        string status = Convert.ToString(myReader["status"]);
                        string name = Convert.ToString(myReader["name"]);              
                        string origin = Convert.ToString(myReader["origin"]);
                        string endDate, destination;
                        if (status.Equals("active")) { endDate = ""; destination=""; }
                        else
                        {
                            endDate = Convert.ToDateTime(myReader["endDate"]).ToString("yyyy-MM-dd");
                            destination = Convert.ToString(myReader["destination"]);
                        }                        
                        string[] row = { status, name, origin, destination,
                            startDate.ToString("yyyy-MM-dd"), endDate};
                        dataGridView1.Rows.Add(row);                        
                    }
                    con.CloseConnection();
                }
                catch
                { con.CloseConnection(); }
            }
            else
                Console.WriteLine("Connection Failed");
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
