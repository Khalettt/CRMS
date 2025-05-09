using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Rentals : System.Web.UI.Page
{
  SqlConnection con = new SqlConnection();
  DBConnection dbcon = new DBConnection();
  SqlCommand cmd;
  SqlDataReader dr;
  SqlDataAdapter da;
  DataTable dt;
  int id;
  protected void Page_Load(object sender, EventArgs e)
    {
    try
    {
      con = dbcon.GetConnection();
      if (!IsPostBack)
      {
        FillRentalData();
        LoadCustomers();
        LoadCars();
        LoadUsers();
      }
    }
    catch (Exception ex)
    {
      Response.Write("<script> alert('" + ex.Message + "') </script>");
    }
  }

  void LoadCustomers()
  {
    try
    {
      if (con.State == ConnectionState.Closed) con.Open();
      cmd = new SqlCommand("SELECT CustomerID, FullName FROM Customers", con);
      dr = cmd.ExecuteReader();
      ddlCustomer.Items.Clear();
      ddlCustomer.Items.Add(new ListItem("-- Select Customer --", ""));
      while (dr.Read())
      {
        ddlCustomer.Items.Add(new ListItem(dr["FullName"].ToString(), dr["CustomerID"].ToString()));
      }
      dr.Close();
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }

  void LoadCars()
  {
    try
    {
      if (con.State == ConnectionState.Closed) con.Open();
      cmd = new SqlCommand("SELECT car_id, make + ' ' + model + ' (' + registration_number + ')' AS CarName FROM cars WHERE status = 'available'", con);
      dr = cmd.ExecuteReader();
      ddlCar.Items.Clear();
      ddlCar.Items.Add(new ListItem("-- Select Car --", ""));
      while (dr.Read())
      {
        ddlCar.Items.Add(new ListItem(dr["CarName"].ToString(), dr["car_id"].ToString()));
      }
      dr.Close();
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }

  void LoadUsers()
  {
    try
    {
      if (con.State == ConnectionState.Closed) con.Open();
      cmd = new SqlCommand("SELECT Userid, Fullname FROM Users", con);
      dr = cmd.ExecuteReader();
      ddlUser.Items.Clear();
      ddlUser.Items.Add(new ListItem("-- Select User --", ""));
      while (dr.Read())
      {
        ddlUser.Items.Add(new ListItem(dr["Fullname"].ToString(), dr["Userid"].ToString()));
      }
      dr.Close();
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }

  void FillRentalData()
  {
    try
    {
      if (con.State == ConnectionState.Closed) con.Open();
      cmd = new SqlCommand(@"SELECT RentalID, CustomerID, car_id, UserID, 
                                   CONVERT(nvarchar(10), RentDate, 120) as RentDate, 
                                   CONVERT(nvarchar(10), ReturnDate, 120) as ReturnDate, 
                                   TotalAmount, Status 
                                   FROM Rentals", con);
      da = new SqlDataAdapter(cmd);
      dt = new DataTable();
      da.Fill(dt);
      dvgRentals.DataSource = dt;
      dvgRentals.DataBind();
    }
    catch (Exception ex)
    {
      Response.Write("<script> alert('" + ex.Message + "') </script>");
    }
  }
  protected void btnSave_Click(object sender, EventArgs e)
  {
    try
    {
      if (btnSave.Text == "Save")
      {
        if (string.IsNullOrWhiteSpace(ddlCustomer.SelectedValue) ||
            string.IsNullOrWhiteSpace(ddlCar.SelectedValue) ||
            string.IsNullOrWhiteSpace(ddlUser.SelectedValue) ||
            string.IsNullOrWhiteSpace(txtReturnDate.Text) ||
              string.IsNullOrWhiteSpace(txtRentDate.Text) ||
            string.IsNullOrWhiteSpace(txtTotalAmount.Text))
        {
          Response.Write("<script>alert('Fill all inputs.');</script>");
          return;
        }

        if (con.State == ConnectionState.Closed) con.Open();
        cmd = new SqlCommand("SELECT ISNULL(MAX(RentalID), 100)+1 FROM Rentals", con);
         id = Convert.ToInt32(cmd.ExecuteScalar());
        cmd = new SqlCommand(@"INSERT INTO Rentals ( RentalID ,CustomerID, car_id, UserID, RentDate, ReturnDate, TotalAmount, Status) 
                                       VALUES (@RentalID,@cust, @car, @user, @rent, @return, @total, @status)", con);
        cmd.Parameters.AddWithValue("@RentalID", id);
        cmd.Parameters.AddWithValue("@cust", ddlCustomer.SelectedValue);
        cmd.Parameters.AddWithValue("@car", ddlCar.SelectedValue);
        cmd.Parameters.AddWithValue("@user", ddlUser.SelectedValue);
        cmd.Parameters.AddWithValue("@rent", Convert.ToDateTime(txtRentDate.Text));
        cmd.Parameters.AddWithValue("@return", string.IsNullOrWhiteSpace(txtReturnDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtReturnDate.Text));
        cmd.Parameters.AddWithValue("@total", Convert.ToDecimal(txtTotalAmount.Text));
        cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);

        if (cmd.ExecuteNonQuery() > 0)
        {
          Response.Write("<script>alert('Rental record saved successfully');</script>");
          clearForms();
        }
        FillRentalData();
      }
      else if (btnSave.Text == "Update")
      {
        if (con.State == ConnectionState.Closed) con.Open();

        cmd = new SqlCommand(@"UPDATE Rentals SET CustomerID=@cust, car_id=@car, UserID=@user, RentDate=@rent, 
                                       ReturnDate=@return, TotalAmount=@total, Status=@status WHERE RentalID=@RentalID", con);
        cmd.Parameters.AddWithValue("@cust", ddlCustomer.SelectedValue);
        cmd.Parameters.AddWithValue("@car", ddlCar.SelectedValue);
        cmd.Parameters.AddWithValue("@user", ddlUser.SelectedValue);
        cmd.Parameters.AddWithValue("@rent", Convert.ToDateTime(txtRentDate.Text));
        cmd.Parameters.AddWithValue("@return", string.IsNullOrWhiteSpace(txtReturnDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtReturnDate.Text));
        cmd.Parameters.AddWithValue("@total", Convert.ToDecimal(txtTotalAmount.Text));
        cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
        cmd.Parameters.AddWithValue("@RentalID",id);

        if (cmd.ExecuteNonQuery() > 0)
        {
          Response.Write("<script>alert('Rental updated successfully');</script>");
        }

        FillRentalData();
        clearForms();
        btnSave.Text = "Save";
        UpdateText.Text = "Add New Rental";
      }
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }

  protected void gvRentals_SelectedIndexChanged(object sender, EventArgs e)
  {
    try
    {
          int selectedIndex = dvgRentals.SelectedIndex;
          Session["RentalID"] = dvgRentals.DataKeys[selectedIndex].Value.ToString();

          ddlCustomer.Text = dvgRentals.SelectedRow.Cells[3].Text;
          ddlCar.Text = dvgRentals.SelectedRow.Cells[4].Text;
          ddlUser.Text = dvgRentals.SelectedRow.Cells[5].Text;
          txtRentDate.Text = dvgRentals.SelectedRow.Cells[6].Text;
          txtReturnDate.Text = dvgRentals.SelectedRow.Cells[7].Text;
          txtTotalAmount.Text = dvgRentals.SelectedRow.Cells[8].Text;
          ddlStatus.SelectedValue = dvgRentals.SelectedRow.Cells[9].Text;

          btnSave.Text = "Update";
          UpdateText.Text = "Update Rental";
        }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }

  protected void gvRentals_RowDeleting(object sender, GridViewDeleteEventArgs e)
  {
    try
    {
      string id = dvgRentals.DataKeys[e.RowIndex].Value.ToString();
      if (con.State == ConnectionState.Closed) con.Open();

      cmd = new SqlCommand("DELETE FROM Rentals WHERE RentalID = @RentalID", con);
      cmd.Parameters.AddWithValue("@id", id);
      if (cmd.ExecuteNonQuery() > 0)
      {
        Response.Write("<script>alert('Rental deleted successfully');</script>");
      }
      FillRentalData();
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }

  protected void clearForms()
  {
    ddlCustomer.SelectedValue = "";
    ddlCar.SelectedValue= "";
    ddlUser.SelectedValue = "";
    txtRentDate.Text = "";
    txtReturnDate.Text = "";
    txtTotalAmount.Text = "";
    ddlStatus.SelectedIndex = 0;
  }

}
