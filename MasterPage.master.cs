using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!IsPostBack)
    {
      if (Session["UserId"] != null)
      {
        lblUsername.Text = Session["Username"].ToString();
        int userId;
        if (int.TryParse(Session["UserId"].ToString(), out userId))
        {
          UsersImage.ImageUrl = "~/ShowImage.ashx?id=" + userId;
        }
        else
        {
          Response.Write("❌ Session[\"UserId\"] is not a valid integer.");
        }
      }

    }
  }
}
