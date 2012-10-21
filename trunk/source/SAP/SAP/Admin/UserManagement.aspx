<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs"
    Inherits="SAP.Admin.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Admin/user_style.css" rel="stylesheet" type="text/css" />
     <div id="user-management">
        <h1 class="user-management-header">User Management</h1>
        <div id="user-management-list">
            <asp:ListView ID="listUsers" runat="server">
                <LayoutTemplate>
                    <table class="data_table">
                        <tr>
                            <th style="width: 100px">
                                <span>User No.</span>
                            </th>
                            <th style="width: 100px">
                                <span>User Id</span>
                            </th>
                            <th style="width: 200px">
                                <span>User Name</span>
                            </th>  
                            <th style="width: 200px">
                                <span>Email</span>
                            </th>
                             <th style="width: 200px">
                                <span>View</span>
                            </th> 
                        </tr>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblNo" Text='<%#Eval("No")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="LabelRoleName" Text='<%#Eval("UserId")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="LabelRoleDescription" Text='<%#Eval("UserName")%>'></asp:Label>
                        </td>
                         <td>
                            <asp:Label runat="server" ID="Label1" Text='<%#Eval("Email")%>'></asp:Label>
                        </td>
                         <td>
                            <asp:HyperLink ID="linkUserLoad" NavigateUrl='<%#Eval("View")%>' runat="server">
                                <asp:Label runat="server" ID="Label2" Text='View'></asp:Label>
                            </asp:HyperLink>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div id="user-management-button">
            <asp:Button ID="btnNewProfiles" OnClick="btnNew_Click" runat="server" Text="New" AlternateText="New" CssClass="g-button g-button-submit"/>            
        </div>
     </div>
</asp:Content>