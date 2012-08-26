<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="SAP.ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="/js/jquery-1.4.3.min.js"></script>
    <script type="text/javascript" src="/js/main.js"></script>
    <link href="skin/skin.css" rel="stylesheet" type="text/css" />
    <style>
    body{
        font-family:Arial, Helvetica, sans-serif; 
        font-size:13px;
    }
    .info, .success, .warning, .error, .validation {
        border: 1px solid;
        margin: 10px 0px;
        padding:15px 10px 15px 50px;
        background-repeat: no-repeat;
        background-position: 10px center;
    }
    .info {
        color: #00529B;
        background-color: #BDE5F8;
        background-image: url('skin/icon/Info.png');
    }
    .success {
        color: #4F8A10;
        background-color: #DFF2BF;
        background-image:url('skin/icon/Success.png');
    }
    .warning {
        color: #9F6000;
        background-color: #FEEFB3;
        background-image: url('skin/icon/Warning.png');
    }
    .error {
        color: #D8000C;
        background-color: #FFBABA;
        background-image: url('skin/icon/Error.png');
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <% if (Session["errorMessage"] != null)
           { %>
                <div class="error">TRANSACTION COMMIT FAILED !!!</div>
                <div class="warning"><h4>Errors Details: </h4><%=Session["errorMessage"]%></div>
        <% }
           else if (Session["successMessage"] != null)
           {%>
                <div class="success">TRANSACTION COMMIT SUCCESSFUL !!!</div>                
        <% }%>

    </form>
</body>
</html>
