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
using System.Web.Security;

public partial class Upload : System.Web.UI.Page
{
    protected string connString = ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;

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
        //Grabs the ID of the logged in user
        MembershipUser user = Membership.GetUser();
        Guid id = (Guid)user.ProviderUserKey;

        // Read the file and store its name and extension
        string fileName = uploadFile.PostedFile.FileName;
        string extension = Path.GetExtension(fileName).ToLower();
        string contentType = "";
        int fileSize = uploadFile.PostedFile.ContentLength;
        int titleLength = title.Text.Length;
        int descLength = description.Text.Length;

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
            uploadStatus.Text = "Error. Please give a description for the image!";
            return;
        }
        // Putting a limit of 48 characters for title
        else if (titleLength > 48)
        {
            uploadStatus.ForeColor = Color.Red;
            uploadStatus.Text = "Error. Title exceeds 48 characters!";
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
            case ".bmp":
                contentType = "image/bmp";
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

                // Get the image's width and height (in pixels) to store in the database as well
                System.Drawing.Image tempImg = System.Drawing.Image.FromStream(fs);
                double fileWidth = tempImg.Width;
                double fileHeight = tempImg.Height;
                tempImg.Dispose();  // manual memory cleanup - don't need the image object anymore

                // Generating a thumbnail of the image to use for the search page
                // The thumbnail will be 25% of the original image's size, unless if
                // the original image is 200x200 or smaller (basically already thumbnail size)
                double fileWidthThumb = 0;
                double fileHeightThumb = 0;
                if (fileWidth > 200 && fileHeight > 200)
                {
                    fileWidthThumb = fileWidth / 4;
                    fileHeightThumb = fileHeight / 4;

                    // Store the aspect ratio of the image
                    double aspectRatio = fileWidthThumb / fileHeightThumb;

                    // Setting lower bound of 200px for thumbnail's height
                    if (fileHeightThumb < 200)
                    {
                        fileHeightThumb = 200;
                        fileWidthThumb = fileHeightThumb * aspectRatio;  // preserve the image's aspect ratio
                    }
                }
                else
                {
                    fileWidthThumb = fileWidth;
                    fileHeightThumb = fileHeight;
                }

                // Create a new image object to create the actual thumbnail data
                System.Drawing.Image img = System.Drawing.Image.FromStream(fs);                
                Bitmap tempBmp = new Bitmap(img, (int)fileWidthThumb, (int)fileHeightThumb);
                Graphics g = Graphics.FromImage(tempBmp);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(img, 0, 0, tempBmp.Width, tempBmp.Height);                
                MemoryStream ms = new MemoryStream();
                tempBmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                Byte[] bytes_thumb = ms.ToArray();

                img.Dispose();
                tempBmp.Dispose();
                g.Dispose();

                // Insert the image and its description and title into the database
                string insertQuery = "INSERT INTO [Image_table] (Image_title, Image_desc, Image_content_type, Image_data, Image_filename, Image_width, Image_height, "
                    + "Image_thumbWidth, Image_thumbHeight, Image_thumbnail, User_Id)"
                    + "values (@newTitle, @newDesc, @newContentType, @newData, @newFilename, @newWidth, @newHeight, @newThumbWidth, @newThumbHeight, @newThumbnail, @userId)";
                SqlCommand cmd = new SqlCommand(insertQuery);
                cmd.Parameters.Add("@newTitle", SqlDbType.VarChar).Value = title.Text;
                cmd.Parameters.Add("@newDesc", SqlDbType.VarChar).Value = description.Text;
                cmd.Parameters.Add("@newContentType", SqlDbType.VarChar).Value = contentType;
                cmd.Parameters.Add("@newData", SqlDbType.Binary).Value = bytes;
                cmd.Parameters.Add("@newFilename", SqlDbType.VarChar).Value = fileName;
                cmd.Parameters.Add("@newWidth", SqlDbType.Int).Value = fileWidth;
                cmd.Parameters.Add("@newHeight", SqlDbType.Int).Value = fileHeight;
                cmd.Parameters.Add("@newThumbWidth", SqlDbType.Int).Value = fileWidthThumb;
                cmd.Parameters.Add("@newThumbHeight", SqlDbType.Int).Value = fileHeightThumb;
                cmd.Parameters.Add("@newThumbnail", SqlDbType.Binary).Value = bytes_thumb;
                cmd.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = id;

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
            uploadStatus.Text = "Error uploading file. Accepted file formats: .jpg, .jpeg, .png, .gif, .bmp";
        }
    }

    // Retrieving images from the database
    /* NOT NEEDED FOR THIS PAGE
    /* CAN BE USED FOR UserPage later.
    protected void retrieve_Click(object sender, EventArgs e)
    {
        SqlDataReader rdr = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string imgTitle = "";
        int imgID = -1;

        try
        {
            conn = new SqlConnection(connString);
            imgTitle = retrieveFile.Text;
            cmd = new SqlCommand("SELECT Image_id FROM [Image_table] WHERE Image_title = '" + imgTitle + "'", conn);
            conn.Open();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                imgID = (int)rdr["Image_id"];
            }
            conn.Close();

            if (imgID == -1)
            {
                retrieveStatus.ForeColor = Color.Red;
                retrieveStatus.Text = "There was an error retrieving the image you specified.";
                Image1.ImageUrl = "/resources/wrong.jpg";
                return;
            }
            else
            {
                retrieveStatus.Text = "";
                Image1.ImageUrl = "~/ShowThumbnail.ashx?imgid=" + imgID.ToString();
            }
        }
        catch
        {
            retrieveStatus.ForeColor = Color.Red;
            retrieveStatus.Text = "There was an error retrieving the image you specified.";
        }
        finally
        {
            conn.Dispose();
        }
    }   */  
}