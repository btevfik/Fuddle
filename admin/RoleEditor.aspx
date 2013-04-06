<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Site.Master" AutoEventWireup="true" CodeFile="RoleEditor.aspx.cs" Inherits="admin_RoleEditor" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
    <div>
        <asp:Button ID="AddButton" runat="server" Text="Add Role" OnClick="AddButton_OnClick"/>
        <asp:Button ID="RemoveButton" runat="server" Text="Remove Role" OnClick="RemoveButton_OnClick"/>
        

        <table style="border-style: none; vertical-align:top; border-color: inherit; border-width: 10px; width:100%;">
            <tr style="width:100%;">
                <td style="width:30%; vertical-align:top;">
                    <p><b><u>Current Roles</u></b></p>
                    <div runat="server" id="RoleBox" />
                    <br />
                    <asp:Button ID="hiddenButton" runat="server" style="display:none;" />
                    <asp:ModalPopupExtender ID="AddButton_ModalPopupExtender" runat="server" 
                        TargetControlID="hiddenButton" PopupControlID="AddButPopUp" PopupDragHandleControlID="AddButPopUp">
                    </asp:ModalPopupExtender>
                    <asp:ModalPopupExtender ID="RemoveButton_ModalPopupExtender" runat="server" 
                        TargetControlID="hiddenButton" PopupControlID="RemoveButPopUp" PopupDragHandleControlID="RemoveButPopUp">
                    </asp:ModalPopupExtender>
                </td>
                <td style="width:70%">
                    <table style="border-style: none; border-color: inherit; border-width: 10px; width:100%;">
                        <tr style="width:100%;">
                            <td style="width:45%">
                                <p><b><u>All Users in Site</u></b></p>
                                <asp:ListBox ID="AllUsersList" runat="server" Width="100%" Height="212px" 
                                    SelectionMode="Multiple" />
                            </td>
                            <td style="width:20%; height:100%; margin:auto 0 auto 0; text-align:center;">
                                <asp:Button runat="server" ID="leftButton" Text="<" Width="50%" OnClick="DeleteFromRole_OnClick"/>
                                <asp:Button runat="server" ID="rightButton" Text=">"  Width="50%" OnClick="MoveToRole_OnClick"/>
                            </td>
                            <td style="width:45%">
                                <p><b><u>All Users in Selected Role</u></b></p>
                                <asp:ListBox ID="RoleUsersList" runat="server" Width="100%" Height="212px" 
                                    SelectionMode="Multiple"/>
                            </td>
                        </tr>
                    </table>
                    
                </td>  
            </tr>
        </table>
        

        
                        
    
    <div id="AddButPopUp" runat="server" style="width:284px; height:127px; text-align:center; background-color:White; border: 2px solid black; display:none;">
        <p><b><u>Enter in a New Role</u></b></p>
        <span>
            <asp:Label runat="server" Text="Role Name:" />
            <asp:TextBox runat="server" ID="RoleNameText" style="margin: 0 auto 0 10px; width:200px;"/>
        </span>
        <br />
        <br />
        <span>
            <asp:Button ID="AddOkBut" runat="server" Text="Ok" Width="50px" OnClick="AddRole_OnClick"/>
            <asp:Button ID="AddCancelBut" runat="server" Text="Cancel" Width="50px" style="margin:0 auto 0 10px" />
        </span>
    </div>

    <div id="RemoveButPopUp" runat="server" 
            style="width:181px; background-color:White; border: 2px solid black; text-align:center; display:none;">
        <p><b><u>Remove a Role</u></b></p>
        <div runat="server">
            <p> Are you sure you want to remove this role?</p>
        </div>
        <br />
        <div style="width:131px; margin: 0 auto 0 auto">
            <asp:Button ID="Button1" runat="server" Text="Ok" Width="50px" OnClick="RemoveRole_OnClick"/>
            <asp:Button ID="Button2" runat="server" Text="Cancel" Width="50px" style="margin:0 auto 0 10px" />
        </div>
    </div>


    
</asp:Content>
