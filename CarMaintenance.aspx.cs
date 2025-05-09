using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CarMaintenance : System.Web.UI.Page
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
    con = dbcon.GetConnection();
    if (!IsPostBack)
    {
      LoadCars();
      LoadMaintenanceData();
    }

  }
  void LoadCars()
  {
    try
    {
      if (con.State == ConnectionState.Closed) con.Open();
      cmd = new SqlCommand("SELECT car_id FROM Cars", con);
      dr = cmd.ExecuteReader();
      ddlCar.Items.Clear();
      ddlCar.Items.Add(new ListItem("-- Select Car --", ""));
      while (dr.Read())
      {
        ddlCar.Items.Add(new ListItem(dr["car_id"].ToString()));
      }
      dr.Close();
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }
  void LoadMaintenanceData()
  {
    try
    {
      if (con.State == ConnectionState.Closed) con.Open();
      cmd = new SqlCommand("SELECT * FROM CarMaintenance", con);
      da = new SqlDataAdapter(cmd);
      dt = new DataTable();
      da.Fill(dt);
      dvgMaintenance.DataSource = dt;
      dvgMaintenance.DataBind();
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }
  protected void btnSave_Click(object sender, EventArgs e)
    {
    try
    {
      if (string.IsNullOrWhiteSpace(ddlCar.SelectedValue) || string.IsNullOrWhiteSpace(txtDate.Text) || string.IsNullOrWhiteSpace(txtCost.Text))
      {
        Response.Write("<script>alert('Please fill all required fields.');</script>");
        return;
      }

      if (btnSave.Text == "Save")
      {
        if (con.State == ConnectionState.Closed) con.Open();

        cmd = new SqlCommand("SELECT ISNULL(MAX(MaintenanceID), 100) + 1 FROM CarMaintenance", con);
        id = Convert.ToInt32(cmd.ExecuteScalar());

        cmd = new SqlCommand(@"INSERT INTO CarMaintenance 
                        (MaintenanceID, car_id, MaintenanceDate, Description, Cost) 
                        VALUES (@id, @car_id, @date, @desc, @cost)", con);

        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@car_id", ddlCar.SelectedValue);
        cmd.Parameters.AddWithValue("@date", txtDate.Text);
        cmd.Parameters.AddWithValue("@desc", txtDescription.Text);
        cmd.Parameters.AddWithValue("@cost", txtCost.Text);

        cmd.ExecuteNonQuery();
        Response.Write("<script>alert('Maintenance record saved successfully');</script>");
      }
      else if (btnSave.Text == "Update")
      {
        if (Session["MaintenanceID"] == null)
        {
          Response.Write("<script>alert('No maintenance selected.');</script>");
          return;
        }

        id = Convert.ToInt32(Session["MaintenanceID"]);

        cmd = new SqlCommand(@"UPDATE CarMaintenance 
                      SET car_id = @car_id,
                          MaintenanceDate = @date,
                          Description = @desc,
                          Cost = @cost 
                      WHERE MaintenanceID = @id", con);

        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@car_id", ddlCar.SelectedValue);
        cmd.Parameters.AddWithValue("@date", txtDate.Text);
        cmd.Parameters.AddWithValue("@desc", txtDescription.Text);
        cmd.Parameters.AddWithValue("@cost", txtCost.Text);

        cmd.ExecuteNonQuery();
        Response.Write("<script>alert('Maintenance updated successfully');</script>");
        btnSave.Text = "Save";
        Session["MaintenanceID"] = null;
      }

      ClearForm();
      LoadMaintenanceData();
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }
  void ClearForm()
  {
    ddlCar.SelectedIndex = 0;
    txtDate.Text = "";
    txtDescription.Text = "";
    txtCost.Text = "";
  }
  protected void dvgMaintenance_SelectedIndexChanged(object sender, EventArgs e)
  {
    try
    {
      GridViewRow row = dvgMaintenance.SelectedRow;

      Session["MaintenanceID"] = row.Cells[2].Text;
      ddlCar.SelectedValue = row.Cells[3].Text;
      txtDate.Text = row.Cells[4].Text;
      txtDescription.Text = row.Cells[5].Text;
      txtCost.Text = row.Cells[6].Text;

      btnSave.Text = "Update";
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }

  protected void dvgMaintenance_RowDeleting(object sender, GridViewDeleteEventArgs e)
  {
    try
    {
      string id = dvgMaintenance.DataKeys[e.RowIndex].Value.ToString();
      if (con.State == ConnectionState.Closed) con.Open();

      cmd = new SqlCommand("DELETE FROM CarMaintenance WHERE MaintenanceID = @id", con);
      cmd.Parameters.AddWithValue("@id", id);
      cmd.ExecuteNonQuery();

      Response.Write("<script>alert('Maintenance record deleted.');</script>");
      LoadMaintenanceData();
    }
    catch (Exception ex)
    {
      Response.Write("<script>alert('" + ex.Message + "');</script>");
    }
  }
}

