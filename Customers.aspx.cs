using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.ConstrainedExecution;

public partial class Customers : System.Web.UI.Page
{
  SqlConnection con = new SqlConnection();
  DBConnection dbcon = new DBConnection();
  SqlCommand cmd;
  SqlDataReader dr;
  SqlDataAdapter da;
  DataTable dt;
  string registrationDate;
  string CustomerID;
  int id;
  protected void Page_Load(object sender, EventArgs e)
    {
    try
    {
      con = dbcon.GetConnection();
      if (!IsPostBack)
      {
        FillCustomerData();
      }
    }
    catch (Exception ex)
    {
      Response.Write("<script> alert('" + ex.Message + "') </script>");
    }
  }

  void FillCustomerData()
  {
    try
    {
      if (con.State == ConnectionState.Closed) con.Open();
      cmd = new SqlCommand("SELECT CustomerID, FullName, UserName, Email, PhoneNumber,Address, CAST(RegistrationDate AS nvarchar(10)) as DataRegister FROM Customers", con);
      da = new SqlDataAdapter(cmd);
      dt = new DataTable();
      da.Fill(dt);
      dvgCustomers.DataSource = dt;
      dvgCustomers.DataBind();
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
      if (btnSave.Text == "Save")
      {
        if (string.IsNullOrWhiteSpace(txtFullname.Text) ||
            string.IsNullOrWhiteSpace(txtUsername.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(txtPhone.Text) ||
            string.IsNullOrWhiteSpace(txtAddress.Text))
        {
          Response.Write("<script>alert('Fill all inputs.');</script>");
          return;
        }

        if (con.State == ConnectionState.Closed) con.Open();
        cmd = new SqlCommand("SELECT PhoneNumber FROM Customers WHERE PhoneNumber = @phone", con);
        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
          Response.Write("<script> alert('This Phone Has Already Exist!') </script>");
          ClearForm();
          dr.Close();
          return;
        }
        dr.Close();


        registrationDate = System.DateTime.Today.ToShortDateString();

        cmd = new SqlCommand("SELECT ISNULL(MAX(CustomerID),100)+1 FROM Customers", con);
         id = Convert.ToInt32(cmd.ExecuteScalar());
        cmd = new SqlCommand("INSERT INTO Customers (CustomerID, FullName, UserName, Email, PhoneNumber, Address, RegistrationDate) VALUES (@id, @fullname, @username, @email, @phone, @address, @date)", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@fullname", txtFullname.Text);
        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
        cmd.Parameters.AddWithValue("@date", registrationDate);

        if (cmd.ExecuteNonQuery() > 0)
        {
          Response.Write("<script> alert('Customer information has been saved successfully') </script>");
          ClearForm();
        }
        FillCustomerData();
      }
      else if (btnSave.Text == "Update")
      {
        if (con.State == ConnectionState.Closed) con.Open();

        cmd = new SqlCommand("UPDATE Customers SET FullName = @fullname, Email = @email,Address = @address WHERE CustomerID = @id", con);
        cmd.Parameters.AddWithValue("@fullname", txtFullname.Text);
        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
        cmd.Parameters.AddWithValue("@id", id);

        if (cmd.ExecuteNonQuery() > 0)
        {
          Response.Write("<script>alert('Customer information has been updated successfully');</script>");
        }

        FillCustomerData();
        ClearForm();
        UpdateText.Text = "Add New Customer";
        btnSave.Text = "Save";
        txtUsername.ReadOnly = false;
        txtPhone.ReadOnly = false;
      }

    }
    catch (Exception ex)
    {
      Response.Write("<script> alert('" + ex.Message + "') </script>");
    }

  }
  protected void ClearForm()
  {
    txtFullname.Text = "";
    txtUsername.Text = "";
    txtEmail.Text = "";
    txtPhone.Text = "";
    txtAddress.Text = "";
  }

  protected void dvgCustomers_SelectedIndexChanged(object sender, EventArgs e)
  {
    try
    {
      txtFullname.Text = dvgCustomers.SelectedRow.Cells[3].Text;
      txtUsername.Text = dvgCustomers.SelectedRow.Cells[4].Text;
      txtEmail.Text = dvgCustomers.SelectedRow.Cells[5].Text;
      txtPhone.Text = dvgCustomers.SelectedRow.Cells[6].Text;
      txtAddress.Text = dvgCustomers.SelectedRow.Cells[7].Text;
      txtUsername.ReadOnly = true;
      txtPhone.ReadOnly = true;
  
      UpdateText.Text = "Update this Customer";
      btnSave.Text = "Update";
    }
    catch (Exception ex)
    {
      Response.Write("<script> alert('" + ex.Message + "') </script>");
    }
  }
  protected void dvgCustomers_RowDeleting(object sender, GridViewDeleteEventArgs e)
  {
    try
    {
       CustomerID = dvgCustomers.DataKeys[e.RowIndex].Value.ToString();
      if (con.State == ConnectionState.Closed)
        con.Open();

      cmd = new SqlCommand("DELETE FROM Customers WHERE CustomerID = @id", con);
      cmd.Parameters.AddWithValue("@id", CustomerID);

      if (cmd.ExecuteNonQuery() > 0)
      {
        Response.Write("<script>alert('User deleted successfully.');</script>");
      }

      FillCustomerData();
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }
}


