using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
  SqlConnection con = new SqlConnection();
  DBConnection dbcon = new DBConnection();
  SqlCommand cmd;
  SqlDataReader dr;
  protected void Page_Load(object sender, EventArgs e)
    {
      con = dbcon.GetConnection();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {

    string username = txtUsername.Text.Trim();
    string password = txtPassword.Text.Trim();

    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
    {
      Response.Write("<script>alert('Please enter both username and password.');</script>");
      return;
    }

    try
    {
      if (con.State == System.Data.ConnectionState.Closed)
      {
        con.Open();
      }

      string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
      cmd = new SqlCommand(query, con);
      cmd.Parameters.AddWithValue("@Username", username);
      cmd.Parameters.AddWithValue("@Password", password);

      dr = cmd.ExecuteReader();
      if (dr.HasRows)
      {
        dr.Read();
        string status = dr["Status"].ToString().ToLower();

        if (status == "active")
        {
          Session["username"] = dr["Username"].ToString();
          Session["Username"] = txtUsername.Text.Trim();
          Session["UserId"] = dr["Userid"].ToString();

          Response.Redirect("Dashboard.aspx");
        }
        else
        {
          Response.Write("<script>alert('Your account is not active.');</script>");
        }
      }
      else
      {
        Response.Write("<script>alert('Invalid username or password.');</script>");
      }

      dr.Close();
      con.Close();
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
    }
  }

}
