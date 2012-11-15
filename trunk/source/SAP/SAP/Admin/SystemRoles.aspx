<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SystemRoles.aspx.cs" Inherits="SAP.Admin.SystemRoles" %>

<asp:content id="pageContent" runat="Server" contentplaceholderid="ContentPlaceHolder1">
    <div style="padding:15px;">
        <div id="title-form" style="border-bottom: 2px solid black; padding-top: 10px;padding-left: 20px;">
                <h1 class="Repeat" style="color: blue;">
                        Administration                    
                </h1>
                <h2>
                    SYSTEM ROLE LIST
                </h2>
            <span id="ctl00_ContentPlaceHolder1_lblResults" style="background-color:Red;"></span>
        </div>
    <fieldset id="roleListFieldset">
    <legend>System Role List</legend>
    <asp:gridview id="systemRoleGridView" runat="server" 
        autogeneratecolumns="False"  onsorting="systemRoleGridView_Sorting">
        <columns>
            <asp:templatefield meta:resourcekey="RoleName" sortexpression="RoleName">
                <itemtemplate>
	                <a href="SystemRoleEdit.aspx?RoleName=<%# Eval("RoleName") %>" ><%# Eval("RoleName") %></a>
	            </itemtemplate>
                <itemstyle cssclass="AlignLeft" />
            </asp:templatefield>
        </columns>
        <emptydatatemplate>
            <strong>
                <asp:localize id="noRolesFoundLocalize" runat="server" meta:resourcekey="NoRolesFound" />
            </strong>
        </emptydatatemplate>
    </asp:gridview>
    <asp:button id="addNewButton" runat="server" cssclass="Button" onclientclick="javascript:location.href='SystemRoleEdit.aspx'; return false;"
        text="Add New" />
</fieldset>
</div>
</asp:content>

