<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="SAP.ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="/js/jquery-1.4.3.min.js"></script>
    <script type="text/javascript" src="/js/main.js"></script>
    <link href="skin/skin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <h1>ERROR POPUP</h1>
        <span>Error Details:</br></span>
        <span>XML Request:</br></span>
        <span><%=Session["errorMessages"]%></span>
    
    </form>
</body>
</html>
