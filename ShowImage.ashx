<%@ WebHandler Language="C#" Class="ShowImage" %>

using System;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class ShowImage : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        SqlDataReader rdr = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;
            conn = new SqlConnection(connStr);

            cmd = new SqlCommand("SELECT Image_data FROM [Image_table] WHERE Image_id = " + context.Request.QueryString["imgID"], conn);
            conn.Open();
            Object result = cmd.ExecuteScalar();
            //if nothing found throw exception
            if (result == null)
            {
                throw new Exception();
            }
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                context.Response.ContentType = "image/jpg";
                context.Response.BinaryWrite((byte[])rdr["Image_data"]);
            }

            if (rdr != null)
                rdr.Close();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
