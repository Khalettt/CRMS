using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Payments : System.Web.UI.Page
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
        FillPaymentData();
        LoadRentals();
      }
    }
    catch (Exception ex)
    {
      Response.Write("<script> alert('" + ex.Message + "') </script>");
    }
  }

  void LoadRentals()
  {
    try
    {
      if (con.State == ConnectionState.Closed) con.Open();
      cmd = new SqlCommand("SELECT RentalID FROM Rentals", con);
      dr = cmd.ExecuteReader();
      ddlReantal.Items.Clear();
      ddlReantal.Items.Add(new ListItem("-- Select Rentals --", ""));
      while (dr.Read())
      {
        ddlReantal.Items.Add(new ListItem(dr["RentalID"].ToString()));
      }
      dr.Close();
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }

  void FillPaymentData()
  {
    try
    {
      if (con.State == ConnectionState.Closed) con.Open();
      cmd = new SqlCommand("SELECT PaymentId, RentalID, PaymentDate, AmountPaid, PaymentMethod FROM Payments", con);
      da = new SqlDataAdapter(cmd);
      dt = new DataTable();
      da.Fill(dt);
      dvgPayments.DataSource = dt;
      dvgPayments.DataBind();
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
        if (string.IsNullOrWhiteSpace(ddlReantal.SelectedValue) ||
          string.IsNullOrWhiteSpace(txtAmount.Text) ||
          string.IsNullOrWhiteSpace(ddlMethod.SelectedValue))
      {
        Response.Write("<script>alert('Fill all inputs.');</script>");
        return;
      }

        if (con.State == ConnectionState.Closed) con.Open();

        cmd = new SqlCommand("SELECT ISNULL(MAX(PaymentId),100) + 1 FROM Payments", con);
        id = Convert.ToInt32(cmd.ExecuteScalar());

        cmd = new SqlCommand(@"INSERT INTO Payments 
          (PaymentId, RentalID, PaymentDate, AmountPaid, PaymentMethod) 
          VALUES (@PaymentId, @RentalID, @date, @amount ,@PaymentMethod)", con);

        cmd.Parameters.AddWithValue("@PaymentId",id);
        cmd.Parameters.AddWithValue("@RentalID", ddlReantal.SelectedValue);
        cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(txtPaymentate.Text));
        cmd.Parameters.AddWithValue("@amount", txtAmount.Text);
        cmd.Parameters.AddWithValue("@PaymentMethod", ddlMethod.SelectedValue);

        if (cmd.ExecuteNonQuery() > 0)
        {
          Response.Write("<script> alert('Payment has been recorded successfully') </script>");
        }

        clearForms();
        FillPaymentData();
      }
      else if (btnSave.Text == "Update")
      {

        if (con.State == ConnectionState.Closed) con.Open();

        cmd = new SqlCommand(@"UPDATE Payments 
          SET RentalID = @RentalID,
              PaymentDate = @date,
              AmountPaid = @amount,
              PaymentMethod = @PaymentMethod 
          WHERE PaymentId = @PaymentId", con);

        cmd.Parameters.AddWithValue("@PaymentId",id);
        cmd.Parameters.AddWithValue("@RentalID", ddlReantal.SelectedValue);
        cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(txtPaymentate.Text));
        cmd.Parameters.AddWithValue("@amount", txtAmount.Text);
        cmd.Parameters.AddWithValue("@PaymentMethod", ddlMethod.SelectedValue);

        if (cmd.ExecuteNonQuery() > 0)
        {
          Response.Write("<script>alert('Payment updated successfully');</script>");
        }

        FillPaymentData();
        clearForms();
        btnSave.Text = "Save";
        //Session["PaymentId"] = null;
      }
    }
    catch (Exception ex)
    {
      Response.Write("<script> alert('" + ex.Message + "') </script>");
    }
  }

  protected void clearForms()
  {
    ddlReantal.SelectedIndex = 0;
    ddlMethod.SelectedIndex = 0;
    txtAmount.Text = "";
    btnSave.Text = "Save";
  }

  protected void dvgPayments_SelectedIndexChanged(object sender, EventArgs e)
  {
    try
    {
      GridViewRow row = dvgPayments.SelectedRow;

      Session["PaymentId"] = row.Cells[1].Text;
      ddlReantal.SelectedValue = row.Cells[2].Text;
      txtAmount.Text = row.Cells[4].Text;
      ddlMethod.SelectedValue = row.Cells[5].Text;
      btnSave.Text = "Update";
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "')</script>");
    }
  }

  protected void dvgPayments_RowDeleting(object sender, GridViewDeleteEventArgs e)
  {
    try
    {
      string paymentId = dvgPayments.DataKeys[e.RowIndex].Value.ToString();

      if (con.State == ConnectionState.Closed)
        con.Open();

      cmd = new SqlCommand("DELETE FROM Payments WHERE PaymentId = @paymentId", con);
      cmd.Parameters.AddWithValue("@paymentId", id);

      if (cmd.ExecuteNonQuery() > 0)
      {
        Response.Write("<script>alert('Payment deleted successfully.');</script>");
      }

      FillPaymentData();
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }
}
