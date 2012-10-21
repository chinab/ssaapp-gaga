<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserProfiles.aspx.cs"
    Inherits="SAP.Admin.UserProfiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Admin/user_style.css" rel="stylesheet" type="text/css" />
    <div id="user-profile">
        <h1 class="user-profile-header">Management Your User Profile</h1>
        <div id = "user-profile-left">     
            <table>
                <tbody>
                    <tr class="user-attribute">
                        <td>
                            <asp:Label runat="server" ID="lblUserId">User Id:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox Width="300px" runat="server" type="text" ID="txtUserId"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="user-attribute">
                        <td>
                            <asp:Label ID="lblUserName" runat="server">User Name:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox Width="300px" runat="server" type="text" ID="txtUserName" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="user-attribute">
                        <td>
                            <asp:Label ID="lblEmail" runat="server">Email:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox Width="300px" runat="server" type="text" ID="txtEmail" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="user-attribute">
                        <td>
                            <asp:Label ID="lblPassword" runat="server">Password:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox Width="300px" runat="server" type="text" ID="txtPassword" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="user-attribute">
                        <td>
                            <asp:Label ID="lblPasswordConfirmation" runat="server">Confirm Password:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox Width="300px" runat="server" type="text" ID="txtPasswordConfirmation" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id = "user-profile-right">     
            <table>
                <tbody>
                    <tr class="user-attribute">
                        <td>
                            <asp:Label runat="server" ID="lblPhone">Phone No:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox Width="300px" runat="server" type="text" ID="txtPhone" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="user-attribute">
                        <td>
                            <asp:Label ID="lblRef1" runat="server">Ref 1:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox Width="300px" runat="server" type="text" ID="txtRef1" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="user-attribute">
                        <td>
                            <asp:Label ID="lblRef2" runat="server">Ref 2:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox Width="300px" runat="server" type="text" ID="txtRef2" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="user-attribute">
                        <td>
                            <asp:Label ID="lblRef3" runat="server">Ref 3:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox Width="300px" runat="server" type="text" ID="txtRef3" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="user-attribute">
                        <td>
                            <asp:Label ID="lblRef4" runat="server">Ref 4:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox Width="300px" runat="server" type="text" ID="txtRef4" ></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="user-profiles-roles">
            <asp:ListView ID="listUserRoles" runat="server">
                <LayoutTemplate>
                    <table class="data_table">
                        <tr>
                            <th style="width: 100px">
                                <span>Role No.</span>
                            </th>
                            <th style="width: 100px">
                                <span>Role Name</span>
                            </th>
                            <th style="width: 200px">
                                <span>Role Description</span>
                            </th>                            
                        </tr>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chkRoleSelected"></asp:CheckBox>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="LabelRoleName" Text='<%#Eval("RoleName")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="LabelRoleDescription" Text='<%#Eval("RoleDescription")%>'></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
         <div id="user-profile-default">
            <asp:ListView ID="listUserDefault" runat="server">
                <LayoutTemplate>
                    <table class="data_table">
                        <tr>
                            <th style="width: 100px">
                                <span>Default Code</span>
                            </th>
                            <th style="width: 100px">
                                <span>Default Value</span>
                            </th>
                            <th style="width: 200px">
                                <span>Default By Query</span>
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
                            <asp:Label runat="server" ID="LabelRoleDescription" Text='<%#Eval("DefaultByQuery")%>'></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>

        <div id="user-profiles-submit">
            <asp:Button ID="btnSaveProfiles" OnClick="btnSave_Click" runat="server" Text="Save" AlternateText="Save" CssClass="g-button g-button-submit"/>            
            <asp:Button ID="btnAddProfiles" OnClick="btnAdd_Click" runat="server" Text="Add" AlternateText="Add" CssClass="g-button g-button-submit"/>            
        </div>
    </div>
</asp:Content>
