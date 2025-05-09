using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
/// <summary>
/// Summary description for DBConnection
/// </summary>
public class DBConnection
{
    public SqlConnection GetConnection()
  {
    SqlConnection con = new SqlConnection("Server=DESKTOP-91MEANG\\SQLEXPRESS;database=CRMS;integrated Security=true;");
    return con; 
  }
    public DBConnection()
    {
    }
}
