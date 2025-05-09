<%@ WebHandler Language="C#" Class="ShowImage" %>


using System;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;

  public class ShowImage : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        int userId;

        if (context.Request.QueryString["id"] != null && int.TryParse(context.Request.QueryString["id"], out userId))
        {
            context.Response.ContentType = "image/jpeg";
            Stream strm = GetUserImage(userId);

            if (strm != null)
            {
                byte[] buffer = new byte[4096];
                int bytesRead = strm.Read(buffer, 0, buffer.Length);

                while (bytesRead > 0)
                {
                    context.Response.OutputStream.Write(buffer, 0, bytesRead);
                    bytesRead = strm.Read(buffer, 0, buffer.Length);
                }

                strm.Close();
            }
            else
            {
                context.Response.Write("No image found.");
            }
        }
        else
        {
            context.Response.Write("Invalid or missing ID.");
        }
    }

    public Stream GetUserImage(int userId)
    {
        DBConnection dbcon = new DBConnection();
        SqlConnection con = dbcon.GetConnection();

        string query = "SELECT Photo FROM Users WHERE Userid = @ID";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@ID", userId);

        try
        {
            con.Open();
            object imgObj = cmd.ExecuteScalar();

            if (imgObj != null && imgObj != DBNull.Value)
            {
                return new MemoryStream((byte[])imgObj);
            }
            return null;
        }
        catch
        {
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
