using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace RentBikeWindowsForm
{
    public partial class Form1 : Form
    {
        List<int> list = new List<int>();
        List<int> list1 = new List<int>();
        int index;
        DBconnection con = new DBconnection();
        Form2 manageBikesForm = null;
        Form3 listRentForm = null;      
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setup_dataGridView();
            CarregarGV();           
            manageBikesForm = new Form2();
            listRentForm = new Form3();          
        }
        public void setup_dataGridView()
        {
            dataGridView1.ColumnCount = 4;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font =
                new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView1.Name = "Stations";
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.GridColor = Color.Black;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].Name = "id";
            dataGridView1.Columns[1].Name = "address";
            dataGridView1.Columns[2].Name = "Nº of bikes available";
            dataGridView1.Columns[3].Name = "status";
            dataGridView1.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor =
                Color.Beige;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        public void CarregarGV()
        {
            if (con.OpenConnection() == true)
            {
                try
                {
                    MySqlDataReader myReader = con.listAllStations();
                    dataGridView1.Rows.Clear();
                    list.Clear();
                    while (myReader.Read())
                    {
                        int id = Convert.ToInt32(myReader["id"]);
                        string address = Convert.ToString(myReader["address"]);
                        int numOfBikes = Convert.ToInt32(myReader["numOfBikes"]);
                        string status= Convert.ToString(myReader["status"]);
                        string[] row = { id.ToString(), address, numOfBikes.ToString(), status };
                        dataGridView1.Rows.Add(row);
                        list.Add(id);
                    }
                    con.CloseConnection();
                }
                catch
                { }
            }
            else
                Console.WriteLine("Connection Failed");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            index = dataGridView1.CurrentCell.RowIndex;
            int id = list[index];           
            Form4 updateStationForm = new Form4(id);           
            updateStationForm.StartPosition = FormStartPosition.Manual;
            updateStationForm.Show();
            updateStationForm.Activate();
                             
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string address = Regex.Replace(textBox1.Text, @"\s+", " ");            
            if (con.OpenConnection() == true)
            {
                con.insertStation(address);
                con.CloseConnection();
                CarregarGV();               
                textBox1.Text = "";
            }
            else
                Console.WriteLine("Connection Failed");
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void manageBikesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (manageBikesForm.IsDisposed)
            {
                manageBikesForm = new Form2();

            }
            manageBikesForm.StartPosition = FormStartPosition.Manual;
            manageBikesForm.Show();
            manageBikesForm.Activate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CarregarGV();
           
        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listRentForm.IsDisposed)
            {
                listRentForm = new Form3();

            }
            listRentForm.StartPosition = FormStartPosition.Manual;
            listRentForm.Show();
            listRentForm.Activate();
        }
    }
}
