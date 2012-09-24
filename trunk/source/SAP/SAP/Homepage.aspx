<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="SAP.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class=clear></div>
 <div id="welcome" class="left">
    <td class="detail_table_td_100" style="width: 132px">
        <span>  Welcome,</span>
        <asp:LoginName ID="LoginName1" runat="server" />
        <span> </span>
    </td>     
 </div>
 <div id="Search" class="right">
    <asp:TextBox ID="txtSearch" runat="server" Width=70%></asp:TextBox>
 </div>
<div style="position: relative;">
<div style="position: fixed;right:0; bottom:50;">
<asp:Image ID = "ABEOLOGO" ImageUrl="~/skin/images/Abeo Logo.png" runat="server" />
</div>
</div>

</asp:Content>
