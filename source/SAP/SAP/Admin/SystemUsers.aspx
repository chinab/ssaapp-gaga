<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SystemUsers.aspx.cs" Inherits="SAP.Admin.SystemUsers" %>

<asp:content id="pageContent" runat="Server" contentplaceholderid="ContentPlaceHolder1">
   <link href="../Admin/user_style.css" rel="stylesheet" type="text/css" />
     <div id="user-profile">
        <h1 class="user-profile-header">Management Your User Profile</h1>
        <div id="title-form" style="border-bottom: 2px solid black; padding-top: 10px;padding-left: 20px;">
                <h1 class="Repeat" style="color: blue;">
                        Administration                    
                </h1>
                <h2>
                    SYSTEM USERS LIST
                </h2>
            <span id="ctl00_ContentPlaceHolder1_lblResults" style="background-color:Red;"></span>
        </div>
        <fieldset id="systemUsersFilter">
            <legend> Filter</legend>
            <div class="OuterBoxFilter">
                <div class="FilterRow">
                    <div class="FilterColumnRight">
                        <asp:label id="userNameLabel" runat="server" associatedcontrolid="userNameTextBox"
                            cssclass="FilterMessageTitle" meta:resourcekey="LabelUserName"></asp:label>
                        <asp:textbox id="userNameTextBox" runat="server" cssclass="FilterInput">
                        </asp:textbox>
                    </div>
                </div>
                <div class="FilterRow">
                    <div class="FilterColumnLeft">                    
                    </div>
                    <div class="FilterColumnRight">
                        <asp:button id="filterButton" runat="server" onclick="FilterButton_Click" text="Filter" />
                    </div>
                </div>
                <div style="clear: both">
                </div>
            
            </div>
        </fieldset>
        <fieldset id="systemUserListFieldset">
            <legend>System User List</legend>
            <asp:GridView id="systemUsersGridView" runat="server" autogeneratecolumns="False" itemname="User Item" itemnamefound="User Item Found"
                allowmulticolumnsorting="false" datakeynames="UserName" defaultsortcolumnname="UserName" 
                onpageindexchanging="SystemUsersGridView_PageIndexChanging" onsorting="SystemUsersGridView_Sorting">
                <columns>
                 <asp:TemplateField meta:resourcekey="UserName">
                        <ItemTemplate>
                            <a href="<%# GetSystemUserEditUrl(Convert.ToString(Eval("UserName"))) %>"><%# HttpUtility.HtmlEncode((string)Eval("UserName")) %></a>
	                    </ItemTemplate>
                        <ItemStyle CssClass="AlignLeft" />
                    </asp:TemplateField>      
                </columns>
                <emptydatatemplate>
                    <strong>
                        <asp:localize id="noSystemUserFoundLocalize" runat="server" meta:resourcekey="NoSystemUserFound" />
                    </strong>
                </emptydatatemplate>
            </asp:GridView>
        </fieldset> 

        <fieldset>
            <asp:ImageButton id="addNewButtonImage" runat="server" cssclass="Button" onclick="AddNewButton_Click"  src="/skin/images/SAP_Add.png" />
        </fieldset>
    </div>
</asp:content>
