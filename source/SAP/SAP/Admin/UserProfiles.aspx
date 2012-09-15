<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserProfiles.aspx.cs"
    Inherits="SAP.Admin.UserProfiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">Your Profiles</a></li>
            <li><a href="#tabs-2">Change Passwords</a></li>
            <li><a href="#tabs-2">Create New User</a></li>
        </ul>
        <div id="tabs-1" style="overflow: auto; height: 300px;">
            <table width="370px" cellspacing="0" cellpadding="1" border="0" style="background-color: Transparent;
                border-color: #CCCC99; border-width: 1px; border-style: Solid; border-collapse: collapse;">
                <tbody>
                    <tr>
                        <td>
                            <table width="370px" cellpadding="0" border="0" style="font-family: Verdana; font-size: 10pt;">
                                <tbody>
                                    <tr>
                                        <td align="center" style="color: White; background-color: #6B696B; font-weight: bold;"
                                            colspan="2">
                                            User Profiles
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label runat="server" ID="lblCountry">Country:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox Width="300px" runat="server" type="text" ID="txtCountry" name="Country"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblGender" runat="server">Gender:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox  Width="300px"  runat="server" type="text" ID="txtGender" name="Gender"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblAge" runat="server">Age:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox  Width="300px"  runat="server" type="text" ID="txtAge" name="Age"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblRole" runat="server" for="lblRole">Roles:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:ListBox  Width="300px"  ID="RoleList" runat="server" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnSave" Text="Save" runat="server" OnClientClick="Dialog.showLoader();" OnClick="btnSave_Click" />
                            <asp:Button ID="btnClear" Text="Clear" runat="server" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="tabs-2">
            <asp:ChangePassword ID="ChangePassword1" runat="server" BackColor="Transparent" BorderColor="#CCCC99"
                BorderStyle="Solid" BorderWidth="1px" CancelDestinationPageUrl="~/login.aspx"
                Font-Names="Verdana" Font-Size="10pt" SuccessPageUrl="~/Homepage.aspx">
                <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
            </asp:ChangePassword>
        </div>
        <div id="tabs-3">

        </div>
    </div>
</asp:Content>
