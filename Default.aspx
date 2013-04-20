<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Fuddle | Main Page</title>
    <!-- Scripts -->
    <script src="/scripts/jquery.freetile.js"></script>
    <script>
        $(document).ready(function () {
            $('#container').freetile({
                animate: true,
                elementDelay: 10,
                forceWidth: true,
                containerWidthStep: 220
            });
        });
    </script>
    <!-- Styles -->
    <style>
        #container {
            margin-left: auto;
            margin-right: auto;
        }

            #container img {
                padding: 5px;
                background-color: white;
                margin: 5px;
                box-shadow: 0px 1px 3px #434141;
            }

        figure {
            position: relative;
            display: inline-block;
            margin: 0;
        }

        figcaption {
            padding: 5px;
            margin: 5px;
            font-size: 12px;
            top: 0;
            left: 0;
            right: 0;
            position: absolute;
            display: none;
            background-color: rgba(0, 0, 0, 0.80);
            color: white;
            text-decoration: none;
            overflow: hidden;
        }

        img:hover ~ figcaption {
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align:center;width:100%;background-color: rgba(0, 0, 0, 0.1);color:#0D98BA;padding:5px">The most liked fuddles by our members.</div>
    <div id="container">
        <figure><img src="http://placekitten.com/209/343" width="200"/><figcaption>30 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/309/375" width="200"/><figcaption>29 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/332/229" width="200"/><figcaption>28 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/312/293" width="200"/><figcaption>27 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/375/319" width="200"/><figcaption>26 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/366/249" width="200"/><figcaption>25 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/267/336" width="200"/><figcaption>24 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/268/288" width="200"/><figcaption>23 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/241/263" width="200"/><figcaption>22 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/267/316" width="200"/><figcaption>21 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/379/361" width="200"/><figcaption>20 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/217/265" width="200"/><figcaption>19 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/291/238" width="200"/><figcaption>18 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/378/367" width="200"/><figcaption>17 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/384/342" width="200"/><figcaption>16 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/360/261" width="200"/><figcaption>15 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/349/322" width="200"/><figcaption>14 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/234/297" width="200"/><figcaption>13 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/311/253" width="200"/><figcaption>12 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/295/375" width="200"/><figcaption>11 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/221/338" width="200"/><figcaption>10 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/337/303" width="200"/><figcaption>9 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/341/272" width="200"/><figcaption>8 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/335/280" width="200"/><figcaption>7 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/200/301" width="200"/><figcaption>6 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/208/371" width="200"/><figcaption>5 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/263/322" width="200"/><figcaption>4 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/282/373" width="200"/><figcaption>3 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/379/362" width="200"/><figcaption>2 Upvotes</figcaption></figure><figure><img src="http://placekitten.com/244/254" width="200"/><figcaption>1 Upvotes</figcaption></figure>
      </div>
</asp:Content>

