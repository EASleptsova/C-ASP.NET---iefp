using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class main : System.Web.UI.Page
{
    DBconnection con = new DBconnection();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        checkCredentials();
        
    }
    public void checkCredentials(){
        Label1.Visible = false;
        Label1.Text = "";
        string login = TextBox1.Text;
        string password = TextBox2.Text;         
            if (con.OpenConnection() == true)
            {
                try
                {
                    string query = "SELECT id, name FROM user WHERE login ='"+
                    login+"' AND PASSWORD ='"+password+"'";
                    MySqlCommand cmd = new MySqlCommand(query, con.getConnection());
                    MySqlDataReader myReader;
                    myReader = cmd.ExecuteReader();
                    int id = -1;
                    string name = "";
                    while (myReader.Read())
                    {
                    name= Convert.ToString(myReader["name"]);
                    id = Convert.ToInt32(myReader["id"]);                       
                    }
                    con.CloseConnection();
                if (id != -1) { Response.Redirect("userForm.aspx?id=" + id+"&name="+name, false); }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "Something wrong. User name not found or incorrect password.";
                }                   
            }catch{}
            }
            else { Console.WriteLine("Connection Failed");            
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("signUpForm.aspx", false);
    }
}
