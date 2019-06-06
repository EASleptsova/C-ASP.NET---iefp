using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class newRent : System.Web.UI.Page
{
    int id;  
    string station;
    string name;
    DBconnection con = new DBconnection();
   
    protected void Page_Load(object sender, EventArgs e)
    {      
        id = int.Parse(Request.QueryString["id"]);
        name = Request.QueryString["name"];       
        if (!IsPostBack)
        {
            Inisializa_dropdownList();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {       
        station = DropDownList1.SelectedValue;      
        //System.Diagnostics.Debug.WriteLine(station);      
        Response.Redirect("confirmRent.aspx?id="+id+"&station="+station + "&name=" + name, false);       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("userForm.aspx?id=" + id + "&name=" + name, false);
    }
    public void Inisializa_dropdownList()
    {       
            DropDownList1.DataSource = CreateDataSource();
            DropDownList1.DataValueField = "StationValueField";
            DropDownList1.DataTextField = "StationTextField";
            DropDownList1.DataBind();
            DropDownList1.SelectedIndex = 0;
        }
    ICollection CreateDataSource()
    {
        DataView dv = null;
        // Create a table to store data for the DropDownList control.
        DataTable dt = new DataTable();

        // Define the columns of the table.
        dt.Columns.Add(new DataColumn("StationTextField", typeof(String)));
        dt.Columns.Add(new DataColumn("StationValueField", typeof(String)));

        // Populate the table with sample values.
        try
        {
            MySqlDataReader myReader = con.listStations();          
            while (myReader.Read())
            {
                int id = Convert.ToInt32(myReader["id"]);
                string address = Convert.ToString(myReader["address"]);
                int numOfBikes = Convert.ToInt32(myReader["numOfBikes"]);               
                dt.Rows.Add(CreateRow("   ____"+address+"___   Nº of bikes available: "+numOfBikes, id.ToString(), dt));
            }
            con.CloseConnection();            
            // Create a DataView from the DataTable to act as the data source
            // for the DropDownList control.
            dv = new DataView(dt);
            return dv;
        }        
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return dv;
        }
       
    }
    DataRow CreateRow(String Text, String Value, DataTable dt)
    {

        // Create a DataRow using the DataTable defined in the 
        // CreateDataSource method.
        DataRow dr = dt.NewRow();

        // This DataRow contains the ColorTextField and ColorValueField 
        // fields, as defined in the CreateDataSource method. Set the 
        // fields with the appropriate value. Remember that column 0 
        // is defined as ColorTextField, and column 1 is defined as 
        // ColorValueField.
        dr[0] = Text;
        dr[1] = Value;

        return dr;
    }
}
