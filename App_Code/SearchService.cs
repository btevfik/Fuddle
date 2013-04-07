using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

public class Image
{
    public string id;
    public string link;
    public int width;
    public int height;
}

public class User
{
    public string name;
}

public class Album
{
    public string name;
}

/// <summary>
/// SearchService is used to retrieve images, albums and users from the database with a given query
/// </summary>
[WebService(Namespace = "http://fuddle.apphb.com")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

[System.Web.Script.Services.ScriptService]
public class SearchService : System.Web.Services.WebService {

    //images list
    List<Image> Images = new List<Image>();
    //albums list
    List<Album> Albums = new List<Album>();
    //users list
    List<User> Users = new List<User>();

    public SearchService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    ///return images with the given search query
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
   [WebMethod]
    public List<Image> GetImages(string query)
    {
        if (query == "")
        {
            return null;
        }
        //just create some images
        //we should actually search in database.
        createImages();
        return Images; 
    }

   /// <summary>
   ///return albums with the given search query
   /// </summary>
   /// <param name="qery"></param>
   /// <returns></returns>
   [WebMethod]
   public List<Album> GetAlbums(string query)
   {
       if (query == "")
       {
           return null;
       }
       //just create some albums
       //we should actually search in database.
       createAlbums(query);
       return Albums;
   }

   /// <summary>
   ///return users with the given search query
   /// </summary>
   /// <param name="query"></param>
   /// <returns></returns>
   [WebMethod]
   public List<User> GetUsers(string query)
   {
       if (query == "")
       {
           return null;
       }
       //just create some users
       //we should actually search in database.
       createUsers(query);
       return Users;
   }

    //for now just create some dummy images
    private void createImages(){
        Random rnd = new Random();
        for (int i = 0; i < 60; i++)
        {
            int width = rnd.Next(400, 600);
            int height = rnd.Next(200, 300);
            Image newImage = new Image{link="http://placekitten.com/"+width+"/"+height, id="id"+i, width=width, height=height};
            Images.Add(newImage);
        }
    }

    //for now just create some dummy albums
    private void createAlbums(string query)
    {
        for (int i = 0; i < 50; i++)
        {
            Album newAlbum = new Album{name=query+" Album "+i};
            Albums.Add(newAlbum);
        }
    }


    //for now just create some dummy images
    private void createUsers(string query)
    {
        for (int i = 0; i < 50; i++)
        {
            User newUser = new User { name = query+" User "+i };
            Users.Add(newUser);
        }
    }

}
