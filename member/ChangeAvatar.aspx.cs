using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SD = System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Configuration;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;


public partial class member_ChangeAvatar : System.Web.UI.Page
{
    String path = HttpContext.Current.Request.PhysicalApplicationPath + "temp\\";
    protected string connString = ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;
    //Grabs the logged in user
    MembershipUser user;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine(path);
        user = Membership.GetUser();
        //show images
        GravatarImage.ImageUrl = "/GetAvatar.ashx?user=" + user.UserName + "&type=g" + "&size=200";
        UploadedImage.ImageUrl = "/GetAvatar.ashx?user=" + user.UserName + "&type=u";
        //create temp folder if not exists
        string pathToCreate = "/temp/";
        if (!Directory.Exists(Server.MapPath(pathToCreate)))
        {
            Directory.CreateDirectory(Server.MapPath(pathToCreate));
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Boolean FileOK = false;

        Boolean FileSaved = false;

        if (Upload.HasFile)
        {

            Session["WorkingImage"] = Upload.FileName;

            String FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();

            String[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".gif" };

            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (FileExtension == allowedExtensions[i])
                {
                    FileOK = true;

                }
            }
        }

        if (FileOK)
        {
            try
            {
                Stream fs = Upload.PostedFile.InputStream;
                // Get the image's width
                System.Drawing.Image tempImg = System.Drawing.Image.FromStream(fs);
                int fileWidth = tempImg.Width;
                tempImg.Dispose();  // manual memory cleanup - don't need the image object anymore
                if (fileWidth > 800)
                {
                    resizeImageAndSave(800);
                }
                else
                {
                    Upload.PostedFile.SaveAs(path + Session["WorkingImage"]);
                }
                FileSaved = true;
            }
            catch (Exception ex)
            {

                lblError.Text = "File could not be uploaded." + ex.Message.ToString();

                lblError.Visible = true;

                FileSaved = false;

                lightbox.Style.Add("display", "block");
            }
        }

        else
        {
            lblError.Text = "Cannot accept files of this type.";

            lblError.Visible = true;

            lightbox.Style.Add("display", "block");
        }

        if (FileSaved)
        {
            pnlCrop.Visible = true;

            lblError.Visible = false;

            imgCrop.ImageUrl = "/temp/" + Session["WorkingImage"].ToString();

            lightbox.Style.Add("display", "none");
        }
    }


    protected void btnCrop_Click(object sender, EventArgs e)
    {
        string ImageName = Session["WorkingImage"].ToString();

        int w = Convert.ToInt32(W.Value);

        int h = Convert.ToInt32(H.Value);

        int x = Convert.ToInt32(X.Value);

        int y = Convert.ToInt32(Y.Value);

        byte[] CropImage = Crop(path + ImageName, w, h, x, y);

        //upload to database
        uploadToDatabase(CropImage);

        //hide panel
        pnlCrop.Visible = false;

        //delete content in temp file
        try
        {
            System.IO.File.Delete(Server.MapPath("/temp/" + ImageName));
        }

        catch (System.IO.IOException ex)
        {
            lblError2.Text = "Error deleting temporary file: " + ex.Message;
            lblError2.Visible = true;
        }
    }

    static byte[] Crop(string Img, int Width, int Height, int X, int Y)
    {

        try
        {

            using (SD.Image OriginalImage = SD.Image.FromFile(Img))
            {

                using (SD.Bitmap bmp = new SD.Bitmap(Width, Height))
                {

                    bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);

                    using (SD.Graphics Graphic = SD.Graphics.FromImage(bmp))
                    {

                        Graphic.SmoothingMode = SmoothingMode.AntiAlias;

                        Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        Graphic.DrawImage(OriginalImage, new SD.Rectangle(0, 0, Width, Height), X, Y, Width, Height, SD.GraphicsUnit.Pixel);

                        MemoryStream ms = new MemoryStream();

                        bmp.Save(ms, OriginalImage.RawFormat);

                        return ms.GetBuffer();

                    }

                }

            }

        }

        catch (Exception Ex)
        {

            throw (Ex);

        }

    }

    //save cropped image to user_info table and set do not use gravatar
    public void uploadToDatabase(byte[] croppedImage)
    {
        //user id.
        Guid id = (Guid)user.ProviderUserKey;
        //connection
        SqlConnection conn = new SqlConnection(connString);
        try
        {
            // Insert the image into the database
            string insertQuery = "UPDATE [User_info] SET User_avatar = @newData, Use_gravatar = @useGravatar WHERE User_id = @userId";
            SqlCommand cmd = new SqlCommand(insertQuery);
            cmd.Parameters.Add("@newData", SqlDbType.Binary).Value = croppedImage;
            cmd.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = id;
            cmd.Parameters.Add("@useGravatar", SqlDbType.Bit).Value = 0;

            // Execute the sql command                
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ee)
        {
            lblError2.Text = "Error uploading file." + ee;
            lblError2.Visible = true;
        }
        finally
        {
            lblError.Visible = true;
            conn.Close();
            conn.Dispose();
        }
    }

    protected void PickGravatar_CheckedChanged(object sender, EventArgs e)
    {
        //user id.
        Guid id = (Guid)user.ProviderUserKey;
        //connection
        SqlConnection conn = new SqlConnection(connString);
        try
        {
            // Insert the image into the database
            string insertQuery = "UPDATE [User_info] SET Use_gravatar = 1 WHERE User_id = @userId";
            SqlCommand cmd = new SqlCommand(insertQuery);
            cmd.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = id;

            // Execute the sql command                
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ee)
        {
            lblError2.Text = "Error changing picture type." + ee;
            lblError2.Visible = true;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }

    protected void PickUpload_CheckedChanged(object sender, EventArgs e)
    {
        //user id.
        Guid id = (Guid)user.ProviderUserKey;
        //connection
        SqlConnection conn = new SqlConnection(connString);
        try
        {
            // Insert the image into the database
            string insertQuery = "UPDATE [User_info] SET Use_gravatar = 0 WHERE User_id = @userId";
            SqlCommand cmd = new SqlCommand(insertQuery);
            cmd.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = id;

            // Execute the sql command                
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ee)
        {
            lblError2.Text = "Error changing picture type." + ee;
            lblError2.Visible = true;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        string ImageName = Session["WorkingImage"].ToString();

        //delete content in temp file
        try
        {
            System.IO.File.Delete(Server.MapPath("/temp/" + ImageName));
        }

        catch (System.IO.IOException ex)
        {
            lblError2.Text = "Error deleting temporary file: " + ex.Message;
            lblError2.Visible = true;
        }

        Response.Redirect("/member/ChangeAvatar.aspx");
    }

    protected void resizeImageAndSave(int thumbnailSize)
    {
        // Create a bitmap of the content of the fileUpload control in memory
        SD.Bitmap originalBMP = new SD.Bitmap(Upload.FileContent);

        int newWidth, newHeight;

        if (originalBMP.Width > originalBMP.Height)
        {

            newWidth = thumbnailSize;

            newHeight = originalBMP.Height * thumbnailSize / originalBMP.Width;

        }

        else
        {
            newWidth = originalBMP.Width * thumbnailSize / originalBMP.Height;

            newHeight = thumbnailSize;
        }

        // Create a new bitmap which will hold the previous resized bitmap
        SD.Bitmap newBMP = new SD.Bitmap(originalBMP, newWidth, newHeight);
        // Create a graphic based on the new bitmap
        SD.Graphics oGraphics = SD.Graphics.FromImage(newBMP);

        // Set the properties for the new graphic file
        oGraphics.SmoothingMode = SmoothingMode.AntiAlias; oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        // Draw the new graphic based on the resized bitmap
        oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

        // Save the new graphic file to the server
        newBMP.Save(path + Session["WorkingImage"]);

        // Once finished with the bitmap objects, we deallocate them.
        originalBMP.Dispose();
        newBMP.Dispose();
        oGraphics.Dispose();
    }
}