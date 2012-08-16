<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SAP.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="skin/AdminLogin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="login-form">
        <asp:Login ID="Login1" runat="server">
            <LayoutTemplate>
                <div id="adminlogin">
                    <div class="loginrow">
                        <div class="label4login">
                            <span>Username</span>
                        </div>
                        <asp:TextBox ID="UserName" runat="server" size="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                            ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="loginrow">
                        <div class="label4login">
                            <span>Password</span>
                        </div>
                        <asp:TextBox ID="Password" runat="server" TextMode="Password" size="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                            ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="loginrow">
                        <div id="login_button">
                            <asp:LinkButton ID="btnLogin" runat="server" CommandName="Login" ValidationGroup="Login1">Login</asp:LinkButton>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="loginrow">
                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></div>
                </div>
            </LayoutTemplate>
        </asp:Login>
    </div>
    </form>
</body>
</html>
