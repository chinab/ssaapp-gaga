<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrder_EditItem.aspx.cs"
    Inherits="SAP.PurchaseOrder_EditItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/js/jquery-1.4.3.min.js"></script>
    <script type="text/javascript" src="/js/main.js"></script>
    <link href="skin/skin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblTest" Text="text" runat="server" />
    </div>
    <div id="action-form">
        <asp:Button ID="btnAdd" Text="OK" runat="server" onclick="btnAdd_Click" />
        <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClientClick="return Main.cancelEditItemClick()" />
    </div>
    </form>
</body>
</html>
