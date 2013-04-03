using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Upload : System.Web.UI.Page
{
    protected string connString = System.Configuration.ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if user is not logged in redirect to login page.
        if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("/Login.aspx");
        }
    }

    // User clicks this button to save the image in the database
    // after previewing the image
    protected void uploadButton_Click(object sender, EventArgs e)
    {
        // Read the file and store its name and extension
        string fileName = uploadFile.PostedFile.FileName;
        string extension = Path.GetExtension(fileName);
        string contentType = "";
        int fileSize = uploadFile.PostedFile.ContentLength;        

        // Error-checking to see if the user has selected a file or not
        if (!uploadFile.HasFile)
        {
            uploadStatus.ForeColor = Color.Red;
            uploadStatus.Text = "Error. No file selected for upload!";
            return;
        }

        // Error-checking for image title and description
        // These fields are required
        if (title.Text == "")
        {
            uploadStatus.ForeColor = Color.Red;
            uploadStatus.Text = "Error. Please give the image a title!";
            return;
        }
        else if (description.Text == "")
        {
            uploadStatus.ForeColor = Color.Red;
            uploadStatus.Text = "Error. Please give a description for the image.";
            return;
        }

        // Setting an upload limit of 1 MB
        if (fileSize > 1048576)
        {
            uploadStatus.ForeColor = Color.Red;
            uploadStatus.Text = "Error uploading file. File size exceeded 1 MB!";
            return;
        }

        // Set the contentType based on the extension
        switch (extension)
        {
            case ".jpg":
                contentType = "image/jpg";
                break;
            case ".jpeg":
                contentType = "image/jpg";
                break;
            case ".png":
                contentType = "image/png";
                break;
            case ".gif":
                contentType = "image/gif";
                break;
            case ".pdf":
                contentType = "application/pdf";
                break;
        }

        // Convert the file to a byte array to store in the database
        if (contentType != "")
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                Stream fs = uploadFile.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes(Convert.ToInt32(fs.Length));

                // Insert the image and its description and title into the database
                string insertQuery = "INSERT INTO [Image_table] (Image_title, Image_desc, Image_content_type, Image_data, Image_filename)"
                    + "values (@newTitle, @newDesc, @newContentType, @newData, @newFilename)";
                SqlCommand cmd = new SqlCommand(insertQuery);
                cmd.Parameters.Add("@newTitle", SqlDbType.VarChar).Value = title.Text;
                cmd.Parameters.Add("@newDesc", SqlDbType.VarChar).Value = description.Text;
                cmd.Parameters.Add("@newContentType", SqlDbType.VarChar).Value = contentType;
                cmd.Parameters.Add("@newData", SqlDbType.Binary).Value = bytes;
                cmd.Parameters.Add("@newFilename", SqlDbType.VarChar).Value = fileName;

                // Execute the sql command                
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();

                // Let the user know the file was uploaded successfully
                uploadStatus.ForeColor = Color.Green;
                uploadStatus.Text = "File uploaded successfully!";
                title.Text = "";
                description.Text = "";
            }
            catch (Exception ee)
            {
                uploadStatus.ForeColor = Color.Red;
                uploadStatus.Text = "Error uploading file." + ee;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        else
        {
            uploadStatus.ForeColor = Color.Red;
            uploadStatus.Text = "Error uploading file. Accepted file formats: .jpg, .jpeg, .png, .gif, .pdf";
        }
    }    
}