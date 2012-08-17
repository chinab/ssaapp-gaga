<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="TestXML.aspx.cs" Inherits="SAP.TestXML" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Test xml generation" 
        Font-Overline="True"></asp:Label>
    </br>
    <asp:Button ID="btnGenXML" runat="server" Text="Gen" 
        onclick="btnGenXML_Click" />
    <br />
    <asp:TextBox ID="TextBox1" runat="server" Height="232px" TextMode="MultiLine" 
        Width="393px"></asp:TextBox>
</asp:Content>

