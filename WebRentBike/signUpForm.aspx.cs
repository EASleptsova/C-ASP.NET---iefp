using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class signUpForm : System.Web.UI.Page
{
    DBconnection con = new DBconnection();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("main.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Visible = false;
        Label1.Text = "";
        string login = TextBox1.Text.Trim();

        if (con.uniqueUsername(login))
        {
            string password = TextBox2.Text;
            if(password.Count() >= 4) {
                string name = Regex.Replace(TextBox3.Text, @"\s+", " ");
                try
                {
                    int nif = int.Parse(TextBox4.Text);
                    if (TextBox4.Text.Count() == 9)
                    {
                        string contact = TextBox5.Text;
                        con.addUser(login, password, name, nif, contact);
                        // show confirmation alert
                        Label1.Visible = true;
                        Label1.ForeColor = System.Drawing.Color.Green;
                        Label1.Text = "User is successfully added!";
                    }
                    else
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Visible = true;
                        Label1.Text = "Nif must be 9 digits long";
                    }                   
                }
                catch
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Visible = true;
                    Label1.Text = "Nif must be 9 digits long";
                }                               
            }
            else
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Visible = true;
                Label1.Text = "Password must be at least 4 characters";
            }
        }
        else
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Visible = true;
            Label1.Text = "User already exists";
        }
    }


    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
       
    }
}