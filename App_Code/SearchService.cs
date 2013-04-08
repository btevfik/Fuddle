using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

public class Image
{
    public string id; //id of the image
    public string link; //link to src
    public string thumbLink; //link to thumbnail
    public int width; //width of the image
    public int height; //height of the image
}

public class User
{
    public string name; //username
    public string avatar; //link to avatar
    public string link; //link to user profile
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
        for (int i = 0; i < 200; i++)
        {
            int width = rnd.Next(800, 1000);
            int height = rnd.Next(400, 1200);
            Image newImage = new Image{link="http://placekitten.com/"+width+"/"+height, id="id"+i, width=width, height=height};
            Images.Add(newImage);
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
