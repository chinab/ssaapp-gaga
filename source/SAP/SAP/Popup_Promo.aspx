<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Popup_Promo.aspx.cs" Inherits="SAP.Popup_Promo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="/js/jquery-1.4.3.min.js"></script>
    <script type="text/javascript" src="/js/main.js"></script>
    <link href="skin/skin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManagerEditVendor" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="editPromoCodxeUpdatePanel" runat="server" UpdateMode="Conditional"
        ChildrenAsTriggers="True">
        <ContentTemplate>
            <asp:TextBox runat="server" ID="txtFilter"></asp:TextBox>
            <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" />
            <br />
            <asp:ListView ID="listPromoCodes" runat="server">
                <LayoutTemplate>
                    <table class="data_table" style="font-size:11px;">
                        <tr>
                            <th style="width: 25px;">
                            </th>
                            <th>
								<span>#</span>
							</th>
                            <th>
                                <span>ID<span>
                            </th>
                            <th>
                                <span>ProCode<span>
                            </th>
                            <th  style="width:120px">
                                <span>ProName<span>
                            </th>
                            <th>
                                <span>ProValue<span>
                            </th>
                            <th>
                                <span>ProQty<span>
                            </th>
                            <th>
                                <span>ProTrf<span>
                            </th>
                            <th>
                                <span>ItemCode<span>
                            </th>
                            <th>
                                <span>ItemName<span>
                            </th>
                            <th>
                                <span>Condition<span>
                            </th>
                            <th>
                                <span>IsReplace<span>
                            </th>
                            <th>
                                <span>Sole<span>
                            </th>
                            <th>
                                <span>ChkCond<span>
                            </th>
                            <th>
                                <span>HeadDscAmt<span>
                            </th>
                            <th>
                                <span>HeadDscPer<span>
                            </th>
                            <th>
                                <span>WhsCode<span>
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="margin: 0 0 0 0; padding: 0 0 0 0;">
                            <input type="radio" name="MyRadioButton" value="<%#Eval("No") %>" checked="<%#Eval("Selected") %>" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label1"><%#Eval("No")%></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label2"><%#Eval("ID")%></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label3"><%#Eval("ProCode")%></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label4"><%#Eval("ProName")%></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label5"><%#Eval("ProValue")%></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label6"><%#Eval("ProQty")%></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label7"><%#Eval("ProTrf")%></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label8"><%#Eval("ItemCode")%></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label9"><%#Eval("ItemName")%></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label10"><%#Eval("Condition")%></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label11"><%#Eval("IsReplace")%></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label12"><%#Eval("Sole")%></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label13"><%#Eval("ChkCond")%></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label14"><%#Eval("HeadDscAmt")%></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label15"><%#Eval("HeadDscPer")%></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label16"><%#Eval("WhsCode")%></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="data_table">
                        <tr>
                            <th>
                            </th>
                            <th>
								<span>#</span>
							</th>
                            <th>
                                <span>ID<span>
                            </th>
                            <th>
                                <span>ProCode<span>
                            </th>
                            <th style="width:100px">
                                <span>ProName<span>
                            </th>
                            <th>
                                <span>ProValue<span>
                            </th>
                            <th>
                                <span>ProQty<span>
                            </th>
                            <th>
                                <span>ProTrf<span>
                            </th>
                            <th>
                                <span>ItemCode<span>
                            </th>
                            <th>
                                <span>ItemName<span>
                            </th>
                            <th>
                                <span>Condition<span>
                            </th>
                            <th>
                                <span>IsReplace<span>
                            </th>
                            <th>
                                <span>Sole<span>
                            </th>
                            <th>
                                <span>ChkCond<span>
                            </th>
                            <th>
                                <span>HeadDscAmt<span>
                            </th>
                            <th>
                                <span>HeadDscPer<span>
                            </th>
                            <th>
                                <span>WhsCode<span>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <span>No Data</span>
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <div id="action-form">
                <asp:Button ID="btnAdd" Text="OK" runat="server" OnClick="btnAdd_Click" />
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClientClick="return Main.cancelDialogClick()" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFilter" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
