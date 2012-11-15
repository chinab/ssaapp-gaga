<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    Codebehind="SystemUserEdit.aspx.cs" Inherits="SAP.Admin.SystemUserEdit" %>

<asp:Content ID="pageContentContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            <label>
                &nbsp;</label>
            <asp:Button ID="saveButton" runat="server" CssClass="Button" OnClick="SaveButton_Click"
                Text="Save" />
            <input id="resetButton" runat="server" class="Button" type="reset" value="Reset" />
            <asp:Button ID="deleteButton" runat="server" CssClass="Button" OnClick="DeleteButton_Click"
                Text="Delete" />
            <asp:Button ID="cancelButton" runat="server" CssClass="Button" Text="Cancel"
                OnClientClick="javascript:location.href='SystemUsers.aspx'; return false;" />
        </div>
    </div>
</asp:Content>
