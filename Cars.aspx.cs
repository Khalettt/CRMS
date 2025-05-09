using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cars : System.Web.UI.Page
{
  SqlConnection con = new SqlConnection();
  DBConnection dbcon = new DBConnection();
  SqlCommand cmd;
  SqlDataReader dr;
  SqlDataAdapter da;
  DataTable dt;
  string CarID;
  protected void Page_Load(object sender, EventArgs e)
  {
    try
    {
      con = dbcon.GetConnection();
      if (!IsPostBack)
      {
        FillCarData();
      }
    }
    catch (Exception ex)
    {
      Response.Write("<script> alert('" + ex.Message + "') </script>");
    }

  }

  void FillCarData()
  {
    try
    {
      if (con.State == ConnectionState.Closed) con.Open();
      cmd = new SqlCommand("SELECT car_id, make, model, year, registration_number, status, rental_price_per_day FROM cars", con);
      da = new SqlDataAdapter(cmd);
      dt = new DataTable();
      da.Fill(dt);
      dvgCars.DataSource = dt;
      dvgCars.DataBind();
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
        if (string.IsNullOrWhiteSpace(txtMake.Text) ||
            string.IsNullOrWhiteSpace(txtModel.Text) ||
            string.IsNullOrWhiteSpace(txtYear.Text) ||
            string.IsNullOrWhiteSpace(txtRegistrationNumber.Text) ||
            string.IsNullOrWhiteSpace(txtRentalPrice.Text))
        {
          Response.Write("<script>alert('Fill all inputs.');</script>");
          return;
        }

        if (con.State == ConnectionState.Closed) con.Open();
        cmd = new SqlCommand("SELECT registration_number FROM cars WHERE registration_number = @reg", con);
        cmd.Parameters.AddWithValue("@reg", txtRegistrationNumber.Text);
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
          Response.Write("<script> alert('This Registration Number Already Exists!') </script>");
          ClearForm();
          dr.Close();
          return;
        }
        dr.Close();

        cmd = new SqlCommand("SELECT ISNULL(MAX(car_id), 100)+1 FROM cars", con);
        int id = Convert.ToInt32(cmd.ExecuteScalar());

        cmd = new SqlCommand("INSERT INTO cars (car_id, make, model, year, registration_number, status, rental_price_per_day) VALUES (@id, @make, @model, @year, @reg, @status, @price)", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@make", txtMake.Text);
        cmd.Parameters.AddWithValue("@model", txtModel.Text);
        cmd.Parameters.AddWithValue("@year", int.Parse(txtYear.Text));
        cmd.Parameters.AddWithValue("@reg", txtRegistrationNumber.Text);
        cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
        cmd.Parameters.AddWithValue("@price", decimal.Parse(txtRentalPrice.Text));

        if (cmd.ExecuteNonQuery() > 0)
        {
          Response.Write("<script> alert('Car information has been saved successfully') </script>");
          ClearForm();
        }
        FillCarData();
      }
      else if (btnSave.Text == "Update")
      {
        if (con.State == ConnectionState.Closed) con.Open();
        cmd = new SqlCommand("UPDATE cars SET make = @make, model = @model, year = @year, status = @status, rental_price_per_day = @price WHERE car_id = @carid", con);
        cmd.Parameters.AddWithValue("@make", txtMake.Text);
        cmd.Parameters.AddWithValue("@model", txtModel.Text);
        cmd.Parameters.AddWithValue("@year", int.Parse(txtYear.Text));
        cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
        cmd.Parameters.AddWithValue("@price", decimal.Parse(txtRentalPrice.Text));
        cmd.Parameters.AddWithValue("@carid", Session["CarID"]);

        if (cmd.ExecuteNonQuery() > 0)
        {
          Response.Write("<script>alert('Car information has been updated successfully');</script>");
        }

        FillCarData();
        ClearForm();
        UpdateText.Text = "Add New Car";
        btnSave.Text = "Save";
        txtRegistrationNumber.ReadOnly = false;
      }
    }
    catch (Exception ex)
    {
      Response.Write("<script> alert('" + ex.Message + "') </script>");
    }
  }
  protected void dvgCars_RowDeleting(object sender, GridViewDeleteEventArgs e)
  {
    try
    {
      CarID = dvgCars.DataKeys[e.RowIndex].Value.ToString();
      if (con.State == ConnectionState.Closed)
        con.Open();

      cmd = new SqlCommand("DELETE FROM cars WHERE car_id = @CarID", con);
      cmd.Parameters.AddWithValue("@CarID", CarID);

      if (cmd.ExecuteNonQuery() > 0)
      {
        Response.Write("<script>alert('Car deleted successfully.');</script>");
      }

      FillCarData();
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }

  }
  protected void ClearForm()
  {
    txtMake.Text = "";
    txtModel.Text = "";
    txtYear.Text = "";
    txtRegistrationNumber.Text = "";
    txtRentalPrice.Text = "";
    ddlStatus.SelectedIndex = 0;
  }

  protected void dvgCars_SelectedIndexChanged(object sender, EventArgs e)
  {
    try
    {
      int selectedIndex = dvgCars.SelectedIndex;
      Session["CarID"] = dvgCars.DataKeys[selectedIndex].Value.ToString();

      txtMake.Text = dvgCars.SelectedRow.Cells[3].Text;
      txtModel.Text = dvgCars.SelectedRow.Cells[4].Text;
      txtYear.Text = dvgCars.SelectedRow.Cells[5].Text;
      txtRegistrationNumber.Text = dvgCars.SelectedRow.Cells[6].Text;
      ddlStatus.SelectedValue = dvgCars.SelectedRow.Cells[7].Text;
      txtRentalPrice.Text = dvgCars.SelectedRow.Cells[8].Text;
      txtRegistrationNumber.ReadOnly = true;
      UpdateText.Text = "Update this Car";
      btnSave.Text = "Update";
    }
    catch (Exception ex)
    {
      Response.Write("<script> alert('" + ex.Message + "') </script>");
    }
  }

}
