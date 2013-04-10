<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="member_Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Fuddle | Settings</title>
    <style type="text/css">
        .auto-style1 {
            width: 143px;
        }
        .auto-style2 {
            width: 114px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ChangePassword ID="ChangePassword1" runat="server">
        <SuccessTemplate>
            <table cellpadding="4" cellspacing="0" style="border-collapse:collapse;">
                <tr>
                    <td>
                        <table cellpadding="0">
                            <tr>
                                <td align="center" colspan="2" style="color:White;background-color:#5D7B9D;font-size:0.9em;font-weight:bold;">Change Password Complete</td>
                            </tr>
                            <tr>
                                <td>Your password has been changed!</td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </SuccessTemplate>
    </asp:ChangePassword>

    <br />

    Update Security Q & A
    <table style="width: 371px">
        <tr>
            <td class="auto-style2">New Question</td>
            <td class="auto-style1">
    <asp:TextBox ID="NewQuestion" runat="server"></asp:TextBox>
            </td>
            <td>
    <asp:RequiredFieldValidator ID="QuestionRequiredValidator" runat="server" ControlToValidate="NewQuestion"
        ForeColor="red" Display="Static" ErrorMessage="Required" />
            </td>
        </tr>
        <tr>
            <td class="auto-style2">New Answer</td>
            <td class="auto-style1">
    <asp:TextBox ID="NewAnswer" runat="server"></asp:TextBox>
            </td>
            <td>
    <asp:RequiredFieldValidator ID="AnswerRequiredValidator" runat="server" ControlToValidate="NewAnswer"
        ForeColor="red" Display="Static" ErrorMessage="Required" />
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Password</td>
            <td class="auto-style1">
    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td>
    <asp:RequiredFieldValidator ID="OldPasswordRequiredValidator" runat="server" ControlToValidate="Password"
        ForeColor="red" Display="Static" ErrorMessage="Required" />
            </td>
        </tr>
    </table>
    <asp:Button ID="ChangeQA" OnClick="ChangeQA_Click" runat="server" Text="Submit" />
    <asp:Label ID="Msg" runat="server"></asp:Label>

    <br /><br />

    Update Email
    <asp:TextBox ID="NewEmail" runat="server"></asp:TextBox>
    <asp:Button ID="ChangeEmail" ValidationGroup="EmailChange" runat="server" OnClick="ChangeEmail_Click" Text="Submit"/>
    <asp:RegularExpressionValidator style="color:red" ID="EmailValidator" runat="server" ControlToValidate="NewEmail"  ValidationGroup="EmailChange" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="E-mail address must be in a valid format."></asp:RegularExpressionValidator>                          
    <asp:Label ID="Msg1" runat="server"></asp:Label>
</asp:Content>

