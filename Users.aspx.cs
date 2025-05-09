using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users : System.Web.UI.Page
{
  SqlConnection con = new SqlConnection();
  DBConnection dbcon = new DBConnection();
  SqlCommand cmd;
  SqlDataReader dr;
  SqlDataAdapter da;
  DataTable dt;
  string DateTime;

  void FillUserData()
  {
    try
    {
      if (con.State == ConnectionState.Closed) con.Open();
      cmd = new SqlCommand("SELECT Userid, Fullname, Username, Email, Phone, Password, Status, cast(Date as nvarchar(10)) as DataRegister from Users", con);
      da = new SqlDataAdapter(cmd);
      dt = new DataTable();
      da.Fill(dt);
      dvgUsers.DataSource = dt;
      dvgUsers.DataBind();
    }
    catch (Exception ex)
    {
      Response.Write("<script> alert('" + ex.Message + "') </script>");
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    try
    {
      con = dbcon.GetConnection();
      if (!IsPostBack)
      {
        FillUserData();
      }
    }
    catch (Exception ex)
    {
      Response.Write("<script> alert('" + ex.Message + "') </script>");
    }
  }

  protected void Button1_Click(object sender, EventArgs e)
  {
    try
    {
      if (btn.Text == "Save")
      {
        if (string.IsNullOrWhiteSpace(txtFullname.Text) ||
            string.IsNullOrWhiteSpace(txtUsername.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(txtPhone.Text) ||
            string.IsNullOrWhiteSpace(txtPassword.Text) ||
            string.IsNullOrWhiteSpace(txtConfirm.Text))
        {
          Response.Write("<script>alert('Fill all inputs.');</script>");
          return;
        }
        if (con.State == ConnectionState.Closed)
          con.Open();
        cmd = new SqlCommand("Select Phone from Users where Phone = @phone", con);
        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
          Response.Write("<script> alert('This Phone Has Already Exist!') </script>");
          clearForms();
          dr.Close();
          return;
        }
        dr.Close();

        if (txtPassword.Text != txtConfirm.Text)
        {
          Response.Write("<script> alert('Passwords do not match!') </script>");
          return;
        }

        if (con.State == ConnectionState.Closed)
          con.Open();

        string dateToday = System.DateTime.Today.ToShortDateString();
        Byte[] imgByte = null;
        if (FuUsersPhoto.HasFile)
        {
          using (System.IO.BinaryReader br = new System.IO.BinaryReader(FuUsersPhoto.PostedFile.InputStream))
          {
            imgByte = br.ReadBytes(FuUsersPhoto.PostedFile.ContentLength);
          }
        }


        cmd = new SqlCommand("SELECT ISNULL(MAX(Userid),100)+1 FROM Users", con);
        int id = Convert.ToInt32(cmd.ExecuteScalar());

        cmd = new SqlCommand("INSERT INTO Users (Userid, Fullname, Username, Email, Phone, Password, Status, Date,Photo) VALUES (@id, @fullname, @username, @email, @phone, @password, 'Active', @date, @imgPhoto)", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@imgPhoto", imgByte);
        cmd.Parameters.AddWithValue("@fullname", txtFullname.Text);
        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
        cmd.Parameters.AddWithValue("@password", txtPassword.Text);
        cmd.Parameters.AddWithValue("@date", dateToday);

        if (cmd.ExecuteNonQuery() > 0)
        {
          Response.Write("<script> alert('User information has been saved successfully') </script>");
          clearForms();
        }
        FillUserData();
      }
      else if (btn.Text == "Update")
      {
        if (con.State == ConnectionState.Closed)
          con.Open();
        cmd = new SqlCommand("UPDATE Users SET Fullname = @fullname, Email = @email, Phone = @phone WHERE Userid = @userid", con);
        cmd.Parameters.AddWithValue("@fullname", txtFullname.Text);
        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
        cmd.Parameters.AddWithValue("@userid", Session["Userid"]);

        if (cmd.ExecuteNonQuery() > 0)
        {
          Response.Write("<script>alert('User information has been updated successfully');</script>");
        }

        FillUserData();
        clearForms();
        UpdateText.Text = "Add New User Registration";
        btn.Text = "Save";
        txtUsername.ReadOnly = false;
        txtPassword.ReadOnly = false;
        txtConfirm.ReadOnly = false;
      }
    }
    catch (Exception ex)
    {
      Response.Write("<script> alert('" + ex.Message + "') </script>");
    }
  }

  protected void clearForms()
  {
    txtFullname.Text = "";
    txtUsername.Text = "";
    txtEmail.Text = "";
    txtPhone.Text = "";
    txtPassword.Text = "";
    txtConfirm.Text = "";
  }

  protected void dvgUsers_SelectedIndexChanged(object sender, EventArgs e)
  {
    try
    {
      GridViewRow row = dvgUsers.SelectedRow;

      Session["Userid"] = row.Cells[1].Text;
      txtFullname.Text = row.Cells[3].Text;
      txtUsername.Text = row.Cells[4].Text;
      txtEmail.Text = row.Cells[5].Text;
      txtPhone.Text = row.Cells[6].Text;
      txtPassword.Text = row.Cells[7].Text;

      txtUsername.ReadOnly = true;
      txtPassword.ReadOnly = true;
      txtConfirm.ReadOnly = true;
      UpdateText.Text = "Update this User";
      btn.Text = "Update";
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "')</script>");
    }
  }
  protected void dvgUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
  {
    try
    {
      string userId = dvgUsers.DataKeys[e.RowIndex].Value.ToString();
      if (con.State == ConnectionState.Closed)
        con.Open();

      cmd = new SqlCommand("DELETE FROM Users WHERE Userid = @userid", con);
      cmd.Parameters.AddWithValue("@userid", userId);

      if (cmd.ExecuteNonQuery() > 0)
      {
        Response.Write("<script>alert('User deleted successfully.');</script>");
      }

      FillUserData();
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }

}
