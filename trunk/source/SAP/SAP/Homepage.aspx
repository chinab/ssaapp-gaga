﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="SAP.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class=clear></div>
 <div id="welcome" class="left">
    <td class="detail_table_td_100" style="width: 132px">
        <span>  Welcome,</span>
        <asp:LoginName ID="LoginName1" runat="server" />
        <span> </span>
    </td>     
 </div>
<table style="width:100%; height: 90%;">
 <tr>
    <td style="width: 100%;" align="right" valign="bottom">
        <asp:Image ID = "Image1" ImageUrl="~/skin/images/Abeo Logo.png" runat="server" />
    </td>
 </tr>    
</table>

</asp:Content>
