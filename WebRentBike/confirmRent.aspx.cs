using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class confirmRent : System.Web.UI.Page
{
    int id;
    int station;
    string name;  
    DBconnection con = new DBconnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = int.Parse(Request.QueryString["id"]);
        name = Request.QueryString["name"];
        station = int.Parse(Request.QueryString["station"]);
 
    }

    protected void Button2_Click(object sender, EventArgs e)
    {       
        Response.Redirect("userForm.aspx?id=" + id + "&name=" + name, false);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string result= con.addRent(id, station);
        if (result.Equals("ready"))
            { Response.Redirect("userForm.aspx?id=" + id + "&name=" + name, false); }
        else
        {
            Label1.Visible = true;
            Label1.Text = result;
        }
        
    }
}