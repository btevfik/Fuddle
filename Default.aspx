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

        figcaption:hover {
            display:block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align:center;width:100%;background-color: rgba(0, 0, 0, 0.1);color:#0D98BA;padding:5px">The most liked fuddles by our members.</div>
    <asp:Panel ID="container" ClientIDMode="Static" runat="server"></asp:Panel>
</asp:Content>

