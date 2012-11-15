<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SystemRoles.aspx.cs" Inherits="SAP.Admin.SystemRoles" %>

<asp:content id="pageContent" runat="Server" contentplaceholderid="ContentPlaceHolder1">
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
</asp:content>

