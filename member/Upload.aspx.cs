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
    //id of logged in user
    Guid id;

    //album index that user want the image to be added to
    protected int album_index
    {
        get { return (int)Session["album_index"]; }
        set { Session["album_index"] = value; }
    }

    protected string connString = ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Grabs the ID of the logged in user
        MembershipUser user = Membership.GetUser();
        id = (Guid)user.ProviderUserKey;

        if (!IsPostBack)
        {
            album_index = -1;
            loadAlbums();
        }
    }

    // User clicks this button to save the image in the database
    // after previewing the image
    protected void uploadButton_Click(object sender, EventArgs e)
    {
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
            uploadStatus.Visible = true;
            return;
        }

        // Error-checking for image title and description
        // These fields are required
        if (title.Text == "")
        {
            uploadStatus.ForeColor = Color.Red;
            uploadStatus.Text = "Error. Please give the image a title!";
            uploadStatus.Visible = true;
            return;
        }
        else if (description.Text == "")
        {
            uploadStatus.ForeColor = Color.Red;
            uploadStatus.Text = "Error. Please give a description for the image!";
            uploadStatus.Visible = true;
            return;
        }
        // Putting a limit of 48 characters for title
        else if (titleLength > 48)
        {
            uploadStatus.ForeColor = Color.Red;
            uploadStatus.Text = "Error. Title exceeds 48 characters!";
            uploadStatus.Visible = true;
            return;
        }

        // Setting an upload limit of 1 MB
        if (fileSize > 1048576)
        {
            uploadStatus.ForeColor = Color.Red;
            uploadStatus.Text = "Error uploading file. File size exceeded 1 MB!";
            uploadStatus.Visible = true;
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
            SqlConnection conn2 = new SqlConnection(connString);

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
                    + "values (@newTitle, @newDesc, @newContentType, @newData, @newFilename, @newWidth, @newHeight, @newThumbWidth, @newThumbHeight, @newThumbnail, @userId); SELECT SCOPE_IDENTITY()";
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

                // Get the Image_id of the image just uploaded                
                int image_id = Convert.ToInt32(cmd.ExecuteScalar());
                SqlCommand vote_cmd = new SqlCommand("INSERT INTO [Vote_table] (Image_id, UpVote, DownVote, User_id) VALUES (@newImageid, @newUpVote, @newDownVote, @newId)", conn2);
                vote_cmd.Parameters.Add("@newImageid", SqlDbType.Int).Value = image_id;
                vote_cmd.Parameters.Add("@newUpVote", SqlDbType.Bit).Value = false;
                vote_cmd.Parameters.Add("@newDownVote", SqlDbType.Bit).Value = false;
                vote_cmd.Parameters.Add("@newId", SqlDbType.UniqueIdentifier).Value = id;

                conn2.Open();
                vote_cmd.ExecuteNonQuery();

                //add the album as well if user specified.
                if (album_index!=-1)
                {
                    uploadStatus.Text = album_index.ToString(); //TODO:REMOVE
                    uploadStatus.Visible = true; //TODO:REMOVE
                    FuddleAlbum.addImage(album_index, image_id);
                    album_index = -1; //reset
                }

                // Let the user know the file was uploaded successfully
                uploadStatus.ForeColor = Color.Green;
                uploadStatus.Text = "File uploaded successfully!";
                title.Text = "";
                description.Text = "";
                uploadStatus.Visible = true;
                pickAlbum.CssClass = "albumButton";
            }
            catch (Exception ee)
            {
                uploadStatus.ForeColor = Color.Red;
                uploadStatus.Text = "Error uploading file." + ee;
                uploadStatus.Visible = true;
            }
            finally
            {
                conn.Close();
                conn.Dispose();

                conn2.Close();
                conn2.Dispose();
            }
        }
        else
        {
            uploadStatus.ForeColor = Color.Red;
            uploadStatus.Text = "Error uploading file. Accepted file formats: .jpg, .jpeg, .png, .gif, .bmp";
            uploadStatus.Visible = true;
        }
    }

    protected void pickAlbum_Click(object sender, EventArgs e)
    {
        lightbox.Visible = true;
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        lightbox.Visible = false;
    }

    protected void chooseButton_Click(object sender, EventArgs e)
    {

        try
        {
            ListItem item = buttonList.SelectedItem;
            album_index = Int32.Parse(item.Value);
            lightbox.Visible = false;
            pickAlbum.CssClass = "albumSelected";
            uploadStatus.Text = item.Value; //TODO:REMOVE
            uploadStatus.Visible = true; //TODO:REMOVE
        }
        catch
        {
            //bad
        }
    }

    private void loadAlbums(){
        buttonList.Items.Clear();
        List<int> albumIds = FuddleAlbum.getAllAlbums(id);
        if (albumIds.Count == 0)
        {
            noAlbumLabel.Visible = true;
            chooseButton.Visible = false;
        }
        else
        {
            foreach (int albumId in albumIds)
            {
                noAlbumLabel.Visible = false;
                chooseButton.Visible = true;
                ListItem albumSelect = new ListItem();
                albumSelect.Value = albumId.ToString();
                albumSelect.Text = FuddleAlbum.getTitle(albumId);
                buttonList.Items.Add(albumSelect);
            }
        }
    }
}
