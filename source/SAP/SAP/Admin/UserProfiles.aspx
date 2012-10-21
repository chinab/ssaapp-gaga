<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserProfiles.aspx.cs"
    Inherits="SAP.Admin.UserProfiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .user-profile-header 
        {
            font-weight: normal;
            line-height: 24px;
            font-size: 2.24em;
            color: #DD4B39;
            font: arial, helvetica, sans-serif;
            margin: 0 0 .92em;
            border-bottom: 1px solid #B6B0CC;
        }
        #user-profile
        {
            padding-bottom: 1em;
            margin-bottom: 1em;
            width: 930px;
            margin: 10px 0px 0px 50px;
            padding-top: 23px;
            padding-bottom: 100px;
            border-bottom: 1px solid #B6B0CC;
            height:170px;
        }    
        
        #user-profile-left
        {
            width: 460px;
            float:left;
        }   
        #user-profile-right
        {
            width: 460px;
            float:right;
        }   
        #user-profiles-roles
        {            
            width:100%;
            padding-top:230px;
            padding-bottom:10px;
            border-bottom: 1px solid #B6B0CC;
        }  
         #user-profiles-default
        {            
            width:100%;
            padding-top:230px;
            padding-bottom:10px;
            border-bottom: 1px solid #B6B0CC;
        }  
        #user-profiles-submit
        {            
            width:100%;
            padding-top:10px;
        }  
        .user-attribute 
        {
            line-height: 40px;
        }
        
        .user-attribute td:first-child {
            width: 25%;
        }
        
        .user-profile input {
            display: inline-block;
            height: 29px;
            margin: 0;
            padding: 0 8px;
            background: white;
            border: 1px solid #D9D9D9;
            border-top: 1px solid silver;
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
            -webkit-border-radius: 1px;
            -moz-border-radius: 1px;
            border-radius: 1px;
        }
        
        .g-button-submit {
            border: 1px solid #3079ED;
            color: white;
            text-shadow: 0 1px rgba(0, 0, 0, 0.1);
            background-color: #4D90FE;
            background-image: -webkit-gradient(linear,left top,left bottom,from(#4D90FE),to(#4787ED));
            background-image: -webkit-linear-gradient(top,#4D90FE,#4787ED);
            background-image: -moz-linear-gradient(top,#4D90FE,#4787ED);
            background-image: -ms-linear-gradient(top,#4D90FE,#4787ED);
            background-image: -o-linear-gradient(top,#4D90FE,#4787ED);
            background-image: linear-gradient(top,#4D90FE,#4787ED);
        }
       .g-button {
            margin: 1.5em 0;
            min-width: 75px;
        }
        
        button.g-button, input[type="submit"].g-button {
            height: 29px;
            line-height: 29px;
            vertical-align: bottom;
            margin: 0;
        }
        
    </style>
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
                            <asp:Label runat="server" ID="lblNo" Text='<%#Eval("No")%>'></asp:Label>
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
        </div>
    </div>
</asp:Content>
