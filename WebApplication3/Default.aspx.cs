using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Default : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["restaurantConnectionString"].ConnectionString);
            String s = " ";
            for (int i = 0; i < CheckBoxList1.Items.Count-1; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    if (s == " ")
                    {
                        s = CheckBoxList1.Items[i].Text +" ";
                    }
                    else
                    {
                        s += "- " + CheckBoxList1.Items[i].Text;
                    }
                }
            }
            con.Open();
            SqlCommand cmd = new SqlCommand
          ("insert into cinfo values('" + fname.Text + "','" + ln.Text + "','" + city.Text + "','" + pc.Text + "','" + pn.Text + "','" + d1.SelectedItem.Text + "','" + comment.Text  + "','" + s + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlDataReader reader = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["restaurantConnectionString"].ConnectionString);
            SqlCommand selectCmd = new SqlCommand("select city,postalcode,pnumber,province,comments from cinfo where fname=@firstName and lname=@lastName", con);
            selectCmd.Parameters.AddWithValue("@firstName", fname.Text);
            selectCmd.Parameters.AddWithValue("@lastName", ln.Text);
            con.Open();
            reader = selectCmd.ExecuteReader();
            if (reader.Read())
            {
                Label1.Visible = true;
                Label1.Text = "found";               
                pn.Text = Convert.ToString(reader["pnumber"]);
                d1.SelectedItem.Text = Convert.ToString(reader["province"]);
                city.Text = Convert.ToString(reader["city"]);
                pc.Text = Convert.ToString(reader["postalcode"]);
                comment.Text = Convert.ToString(reader["comments"]);
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "No user Found";         
                         
                CheckBoxList1.Text = String.Empty;
                RadioButtonList1.Text = String.Empty;
                city.Text = String.Empty;
                pc.Text = String.Empty;
                d1.SelectedItem.Text = String.Empty;
                pn.Text = String.Empty;
                comment.Text = String.Empty;
            }
        }
        }
}