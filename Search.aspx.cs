using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //get the query string
        string query = Request.QueryString["q"];
        
        //get the option
        string opt = Request.QueryString["o"];

        //get the panel where results go
        LiteralControl panelText = searchresults.Controls[0] as LiteralControl;
        
        //display the query string
        searchedQuery.Text = "You have searched for: <b>"+query+"</b><br><br>Fuddle found you these.";

        //if nothing is submitted clear the area, display message
        if (query == "" || query == null)
        {
            panelText.Text = "";
            searchedQuery.Text = "Use the box in the header to search for cute elephants.";
            return;
        }

        //SEARCH BELOW HERE

        //search for images
        if(opt=="img"){
            //uptade css classes according to option
            imageListItem.Attributes.Add("class", "activated");
            albumListItem.Attributes.Remove("class");
            userListItem.Attributes.Remove("class");

            /////////SEARCHING GOES HERE

            //some dummy images for now
            StringWriter myWriter = new StringWriter();
            // Decode the encoded string.
            HttpUtility.HtmlDecode("<p>These are dummy images<p>&lt;img src=&quot;http://placekitten.com/288/345&quot;/&gt;&lt;img src=&quot;http://placekitten.com/373/164&quot;/&gt;&lt;img src=&quot;http://placekitten.com/164/356&quot;/&gt;&lt;img src=&quot;http://placekitten.com/73/286&quot;/&gt;&lt;img src=&quot;http://placekitten.com/159/259&quot;/&gt;&lt;img src=&quot;http://placekitten.com/181/262&quot;/&gt;&lt;img src=&quot;http://placekitten.com/351/162&quot;/&gt;&lt;img src=&quot;http://placekitten.com/116/194&quot;/&gt;&lt;img src=&quot;http://placekitten.com/227/306&quot;/&gt;&lt;img src=&quot;http://placekitten.com/95/165&quot;/&gt;&lt;img src=&quot;http://placekitten.com/333/86&quot;/&gt;&lt;img src=&quot;http://placekitten.com/70/90&quot;/&gt;&lt;img src=&quot;http://placekitten.com/181/266&quot;/&gt;&lt;img src=&quot;http://placekitten.com/277/384&quot;/&gt;&lt;img src=&quot;http://placekitten.com/385/141&quot;/&gt;&lt;img src=&quot;http://placekitten.com/58/253&quot;/&gt;&lt;img src=&quot;http://placekitten.com/288/384&quot;/&gt;&lt;img src=&quot;http://placekitten.com/185/190&quot;/&gt;&lt;img src=&quot;http://placekitten.com/196/125&quot;/&gt;&lt;img src=&quot;http://placekitten.com/324/107&quot;/&gt;&lt;img src=&quot;http://placekitten.com/80/393&quot;/&gt;&lt;img src=&quot;http://placekitten.com/142/277&quot;/&gt;&lt;img src=&quot;http://placekitten.com/110/368&quot;/&gt;&lt;img src=&quot;http://placekitten.com/193/323&quot;/&gt;&lt;img src=&quot;http://placekitten.com/121/359&quot;/&gt;&lt;img src=&quot;http://placekitten.com/356/218&quot;/&gt;&lt;img src=&quot;http://placekitten.com/351/106&quot;/&gt;&lt;img src=&quot;http://placekitten.com/372/258&quot;/&gt;&lt;img src=&quot;http://placekitten.com/206/81&quot;/&gt;&lt;img src=&quot;http://placekitten.com/79/104&quot;/&gt;", myWriter);
            panelText.Text = myWriter.ToString();

            //display number of results
            numresult.Text = "30 Images Found";
        }
        //search for albums
        else if (opt == "alb")
        {
            //uptade css classes according to option
            albumListItem.Attributes.Add("class", "activated");
            userListItem.Attributes.Remove("class");
            imageListItem.Attributes.Remove("class");

            /////////SEARCHING GOES HERE

            //display number of results
            numresult.Text = "2 Albums Found";
            panelText.Text = "<p>Found Albums will go here</p>";
        }
        //search for users
        else if(opt=="usr"){
            //update css classes according to option
            userListItem.Attributes.Add("class", "activated");
            albumListItem.Attributes.Remove("class");
            imageListItem.Attributes.Remove("class");

            ////////////SEARCHING GOES HERE

            //display number of results
            numresult.Text = "5 Users Found";
            panelText.Text = "<p>Found Users will go here</p>";
        }

        //update tab links
        imagesLink.NavigateUrl = "/Search.aspx?q=" + query + "&o=img";
        albumsLink.NavigateUrl = "/Search.aspx?q=" + query + "&o=alb";
        usersLink.NavigateUrl = "/Search.aspx?q=" + query + "&o=usr";
    }
}