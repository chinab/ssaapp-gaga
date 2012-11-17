<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    Codebehind="SystemUserEdit.aspx.cs" Inherits="SAP.Admin.SystemUserEdit" %>

<asp:Content ID="pageContentContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Admin/user_style.css" rel="stylesheet" type="text/css" />
    <link href="/skin/Admin/edit.css" rel="stylesheet" type="text/css" />
    <div id="user-main">
        <h1 class="user-header">User Information</h1>
    <div class="EditRegion">       
        <div id="roleNameDiv" runat="server">
            <label>
                <asp:Localize ID="roleNameLocalize" runat="server" meta:resourcekey="LabelRoleName">
                </asp:Localize>
                <span id="requriedSymbolSpan" runat="server" class="RequiredField">*</span>
            </label>
            <asp:DropDownList ID="rolesDropDownList" runat="server">
            </asp:DropDownList>
        </div>
        <div id="userNameDiv" runat="server">
            <label>
                <asp:Localize ID="userNameLocalize" runat="server" meta:resourcekey="LabelUserName">
                </asp:Localize>
                <span id="Span1" runat="server" class="RequiredField">*</span>
            </label>
            <asp:TextBox ID="userNameTextbox" runat="server" CssClass="InputBox" MaxLength="200" />
            <asp:RequiredFieldValidator ID="rqfUserName" runat="server" Display="Dynamic"
                ErrorMessage="Required" ControlToValidate="userNameTextbox" 
                CssClass="RequiredField" SetFocusOnError="True"></asp:RequiredFieldValidator>
        </div>
        <div id="passwordDiv" runat="server">
            <label>
                <asp:Localize ID="passwordLocalize" runat="server" meta:resourcekey="LabelPassword">
                </asp:Localize>
                <span id="Span2" runat="server" class="RequiredField">*</span>
            </label>
            <asp:TextBox ID="passwordTextbox" runat="server" CssClass="InputBox" TextMode="Password"></asp:TextBox>
            <asp:Label ID="passwordRequiredLabel" runat="server" ForeColor="Red" 
                Text="Required" Visible="False"></asp:Label>
            <asp:RegularExpressionValidator ID="passwordRegularExpressionValidator" CssClass="RequiredField" runat="server"
                ControlToValidate="passwordTextbox" ErrorMessage="Password Length must be greater than 5 !!!"
                ValidationExpression="^.{5,20}$" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
        </div>
        <div id="confirmPasswordDiv" runat="server">
            <label>
                <asp:Localize ID="confirmPasswordLocalize" runat="server" meta:resourcekey="LabelConfirmPassword">
                </asp:Localize>
                <span id="Span3" runat="server" class="RequiredField">*</span>
            </label>
            <asp:TextBox ID="confirmPasswordTextbox" runat="server" CssClass="InputBox" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="cpvRePassword" runat="server" ErrorMessage="Password confirm is not match." Display="Dynamic"
                Text="Password confirm is not match." ControlToCompare="passwordTextbox" ControlToValidate="confirmPasswordTextbox" 
                CssClass="RequiredField" SetFocusOnError="True"></asp:CompareValidator>
        </div> 
        <div id="emailDiv" runat="server">
            <label>
                <asp:Localize ID="emailLocalize" runat="server" meta:resourcekey="LabelEmail">
                </asp:Localize>
            </label>
            <asp:TextBox ID="emailTextbox" runat="server" CssClass="InputBox"></asp:TextBox>
            <asp:RegularExpressionValidator ID="emailRegularExpressionValidator" CssClass="RequiredField" runat="server"
                ControlToValidate="emailTextbox" ErrorMessage="Email is not invalid"
                ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
        </div>
        <div id="lastActivityDateDiv" runat="server">
            <label>
                <asp:Localize ID="lastActivityDateLocalize" runat="server" meta:resourcekey="LabelLastActivityDate">
                </asp:Localize>
            </label>
            <asp:TextBox ID="lastActivityDateTextBox" runat="server" CssClass="InputBox" ReadOnly="true"></asp:TextBox>
        </div>
        <div id="activeDiv">
            <label>
                <asp:Localize ID="activeLocalize" runat="server" meta:resourcekey="LabelActive">
                </asp:Localize>
            </label>
            <asp:CheckBox ID='chkActive' runat="server" />
        </div>
        <div>
            <h4 class="user-default-header">User Default Value</h4>
        </div>
        <div id="user-default">
            <asp:ListView ID="listUserDefault" runat="server">
                <LayoutTemplate>
                    <table style="width: 100%;">
                        <tr>
                            <th style="width: 100px">
                                <span>&nbsp;Default Code</span>
                            </th>
                            <th style="width: 100px">
                                <span>&nbsp;Default Value</span>
                            </th>
                            <th style="width: 200px">
                                <span>&nbsp;Default By Query</span>
                            </th>                            
                        </tr>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblNo" Text='<%#Eval("DefaultCode")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="LabelRoleName" Text='<%#Eval("DefaultValue")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="LabelRoleDescription" Text='<%#Eval("DefaultType")%>'></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
        
        <div id="user-submit">
            <asp:Button ID="saveButton" OnClick="SaveButton_Click" runat="server" Text="Save" AlternateText="Save" CssClass="g-button g-button-submit"/>            
            <asp:Button ID="deleteButton" OnClick="DeleteButton_Click" runat="server" Text="Delete" AlternateText="Delete" CssClass="g-button g-button-submit"/>            
            <asp:Button ID="cancelButton" OnClientClick="javascript:location.href='SystemUsers.aspx'; return false;" runat="server" Text="Cancel" AlternateText="Cancel" CssClass="g-button g-button-submit"/>            
        </div>
    </div>
    </div>
</asp:Content>
