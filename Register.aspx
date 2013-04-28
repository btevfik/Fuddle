<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Fuddle | Register</title>
    <!-- StyleSheets -->
    <link rel="stylesheet" type="text/css" href="/stylesheets/register.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center">
        <h1>Sign Up</h1>
        <br />
    </div>
    <div id="register-content">
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" OnCreatedUser="CreateUserWizard1_CreatedUser" DuplicateUserNameErrorMessage="That user name is taken." InvalidPasswordErrorMessage="Password length minimum: {0}." ContinueDestinationPageUrl="~/Default.aspx">
            <WizardSteps>
                <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                    <ContentTemplate>
                        <fieldset id="inputs">
                            Username
                            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="registerError" ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            Password
                            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="registerError" ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            Confirm Password
                            <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="registerError" ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            Email
                            <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="registerError" ID="EmailRequired" runat="server" ControlToValidate="Email" ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            Security Question
                            <asp:TextBox ID="Question" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="registerError" ID="QuestionRequired" runat="server" ControlToValidate="Question" ErrorMessage="Security question is required." ToolTip="Security question is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            Answer
                            <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="registerError" ID="AnswerRequired" runat="server" ControlToValidate="Answer" ErrorMessage="Security answer is required." ToolTip="Security answer is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                        </fieldset>
                        <div class="registerError">
                            <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ControlToValidate="Email"  Display="Dynamic" ValidationGroup="CreateUserWizard1" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="E-mail address must be in a valid format."></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="UserNameLengthValidator" runat="server" ControlToValidate="UserName" Display="Dynamic" ValidationGroup="CreateUserWizard1" ValidationExpression="^[\s\S]{0,15}$" ErrorMessage="Username can be maximum 15 characters long."></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="UserNameValidator" runat="server" Display="Dynamic" ControlToValidate="UserName" ValidationGroup="CreateUserWizard1" ErrorMessage="Username can only contain alphanumeric characters and underscore." ValidationExpression="^\w*$"></asp:RegularExpressionValidator>
                            <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="Passwords don't match." ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                            <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                        </div>
                    </ContentTemplate>
                    <CustomNavigationTemplate>
                            <asp:Button ID="StepNextButton" CssClass="submitButton" runat="server" CommandName="MoveNext" Text="Enter Awesomeness" ValidationGroup="CreateUserWizard1" />
                    </CustomNavigationTemplate>
                </asp:CreateUserWizardStep>
                <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                    <ContentTemplate>
                        <div style="text-align:center;width:260px">
                        You are almost there. <br /> An activation link has been sent to your email.
                        </div>
                        <br />
                        <asp:Button ID="ContinueButton" CssClass="submitButton" runat="server" CausesValidation="False" CommandName="Continue" Text="Cool" ValidationGroup="CreateUserWizard1" style="margin-left:80px;width:100px"/>
                     </ContentTemplate>
                </asp:CompleteWizardStep>
            </WizardSteps>
        </asp:CreateUserWizard>
    </div>
</asp:Content>

