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
            HttpUtility.HtmlDecode("&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/654/785&quot; data-width=&quot;654&quot; data-height=&quot;785&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/772/357&quot; data-width=&quot;772&quot; data-height=&quot;357&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/545/486&quot; data-width=&quot;545&quot; data-height=&quot;486&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/512/794&quot; data-width=&quot;512&quot; data-height=&quot;794&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/503/669&quot; data-width=&quot;503&quot; data-height=&quot;669&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/432/538&quot; data-width=&quot;432&quot; data-height=&quot;538&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/626/769&quot; data-width=&quot;626&quot; data-height=&quot;769&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/707/440&quot; data-width=&quot;707&quot; data-height=&quot;440&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/798/718&quot; data-width=&quot;798&quot; data-height=&quot;718&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/414/692&quot; data-width=&quot;414&quot; data-height=&quot;692&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/737/370&quot; data-width=&quot;737&quot; data-height=&quot;370&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/488/730&quot; data-width=&quot;488&quot; data-height=&quot;730&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/687/754&quot; data-width=&quot;687&quot; data-height=&quot;754&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/626/469&quot; data-width=&quot;626&quot; data-height=&quot;469&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/341/389&quot; data-width=&quot;341&quot; data-height=&quot;389&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/457/672&quot; data-width=&quot;457&quot; data-height=&quot;672&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/642/493&quot; data-width=&quot;642&quot; data-height=&quot;493&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/566/716&quot; data-width=&quot;566&quot; data-height=&quot;716&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/466/533&quot; data-width=&quot;466&quot; data-height=&quot;533&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/618/589&quot; data-width=&quot;618&quot; data-height=&quot;589&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/477/609&quot; data-width=&quot;477&quot; data-height=&quot;609&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/775/397&quot; data-width=&quot;775&quot; data-height=&quot;397&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/521/730&quot; data-width=&quot;521&quot; data-height=&quot;730&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/498/752&quot; data-width=&quot;498&quot; data-height=&quot;752&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/420/597&quot; data-width=&quot;420&quot; data-height=&quot;597&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/499/735&quot; data-width=&quot;499&quot; data-height=&quot;735&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/326/515&quot; data-width=&quot;326&quot; data-height=&quot;515&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/710/733&quot; data-width=&quot;710&quot; data-height=&quot;733&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/520/584&quot; data-width=&quot;520&quot; data-height=&quot;584&quot;/&gt;&lt;img class=&quot;search-img&quot; src=&quot;http://placekitten.com/528/781&quot; data-width=&quot;528&quot; data-height=&quot;781&quot;/&gt;", myWriter);
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
            numresult.Text = "0 Albums Found";
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
            numresult.Text = "0 Users Found";
            panelText.Text = "<p>Found Users will go here</p>";
        }

        //update tab links
        imagesLink.NavigateUrl = "/Search.aspx?q=" + query + "&o=img";
        albumsLink.NavigateUrl = "/Search.aspx?q=" + query + "&o=alb";
        usersLink.NavigateUrl = "/Search.aspx?q=" + query + "&o=usr";
       
    }    
}