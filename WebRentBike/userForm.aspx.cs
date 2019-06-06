using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;


public partial class userForm : System.Web.UI.Page
{
    DBconnection con = new DBconnection();
    int id = -1;
    string name;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = int.Parse(Request.QueryString["id"]);
        name = Request.QueryString["name"];
        if (name != null) { Label1.Text = name; }
        else
            Label1.Text = "user";
        if (hasActiveRent(id))
        {
            Label2.Visible = true;
            Button1.Visible = true;
        }
        else
        {
            Label3.Visible = true;
            Button2.Visible = true;
        }           
        CarregarGV();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("main.aspx");
    }
   
    public void CarregarGV()
    {
        if (con.OpenConnection() == true)
        {
            try
            {
                string query = "SELECT r.id, r.startDate, r.endDate, r.STATUS, u.NAME," +
             " s.address AS destination, s1.address AS origin FROM rent r" +
             " LEFT join user u ON u.id = r.userId" +
             " left join station s ON s.id = r.destinationId" +
             " left join station s1 ON s1.id = r.startStationId WHERE u.id =" +id+
             " order by r.id desc";
                MySqlCommand cmd = new MySqlCommand(query, con.getConnection());
                DataTable dataTable = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dataTable);
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
                con.CloseConnection();
            }
            catch
            { }
        }
        else
            Console.WriteLine("Connection Failed");
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public bool hasActiveRent(int id)
    {
        if (con.OpenConnection() == true)
        {
            try
            {
                string query = "SELECT status FROM rent WHERE userId ="+id+" AND STATUS = 'active'";
                MySqlCommand cmd = new MySqlCommand(query, con.getConnection());
                MySqlDataReader myReader;
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {                   
                    string status = Convert.ToString(myReader["status"]);
                    if (status.Equals("active"))
                    {
                        con.CloseConnection();
                        return true;
                    }
                    else {
                        con.CloseConnection();
                        return false;
                    }                      
                }                
                con.CloseConnection();
                return false;                
            }
            catch
            { return false; }
        }
        else
            Console.WriteLine("Connection Failed");
        return false;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("newRent.aspx?id="+id+ "&name=" + name, false);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
        Response.Redirect("finishRent.aspx?id=" + id+ "&name=" + name, false);
    }
}