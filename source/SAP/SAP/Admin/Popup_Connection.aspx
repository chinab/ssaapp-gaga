<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Popup_Connection.aspx.cs" Inherits="SAP.Popup_Connection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="../js/jquery-1.4.3.min.js"></script>
    <script type="text/javascript" src="../js/main.js"></script>
    <link href="../skin/skin.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .left
        {
            width: 308px;
        }
        .right
        {
            width: 381px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManagerEditWareHouse" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="editWareHouseUpdatePanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">       
        <ContentTemplate>
            <br />
            <div id="header-form">
			<div class="left">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>SQL Server</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSQLServer" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>SQL User</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSQLUser" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>SQL Password</span>
                                </td>
                                <td>
                                   <asp:TextBox ID="txtSQLPwd" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                               <td class="detail_table_td_100">
                                    <span>SQL Database</span>
                                </td>
                                <td>
                                   <asp:TextBox ID="txtSQLDB" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
            <div class="right">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>SAP License Server</span>
                                </td>
                                <td>
                                    <asp:TextBox Enabled="false" ID="txtSAPLicense" Text="" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>SAP User</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSAPUser" runat="server" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>SAP Password</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSAPPwd" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>SQL Type</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSQLType" runat="server" Width="100px" 
                                        AutoPostBack="true">
                                        <asp:ListItem Text="SQL 2005" Value="2005"></asp:ListItem>
                                        <asp:ListItem Text="SQL 2008" Value="2008"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
            </div>
            <asp:Label ID="lblTest" runat ="server" Text="" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="action-form">
        <asp:Button ID="btnTest" Text="Test Connection" runat="server" 
            OnClick="btnTest_Click" />
        <asp:Button ID="btnAdd" Text="Save" runat="server" OnClick="btnAdd_Click" />
        <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClientClick="return Main.cancelDialogClick()" />
    </div>
    </form>
</body>
</html>