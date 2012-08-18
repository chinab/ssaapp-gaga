<%@ Page Title="Purchase Order" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="PurchaseOrder.aspx.cs" Inherits="SAP.PurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Main.myUpdatePanelId = '<%= purchaseOrderUpdatePanel.ClientID %>';
    </script>
    <asp:UpdatePanel ID="purchaseOrderUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="title-form">
                <h2>
                    Purchase Order</h2>
            </div>
            <div id="header-form">
                <div class="left">
                    <table class="detail_table">
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Vendor</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtVendor" runat="server"></asp:TextBox>
                                <asp:HyperLink ID="linkVendorsLoad" NavigateUrl="javascript:Main.openEditVendor()"
                                    runat="server">
                                    <asp:Image ID="imgVendorsLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                </asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Name</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Contact Person</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlContactPerson" runat="server">
                                    <asp:ListItem Text="Riahk"></asp:ListItem>
                                    <asp:ListItem Text="Nguyen"></asp:ListItem>
                                    <asp:ListItem Text="Sum"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Vendor Ref No.</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRefNo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100">
                                <asp:DropDownList ID="ddlCurency" runat="server">
                                    <asp:ListItem Text="VND"></asp:ListItem>
                                    <asp:ListItem Text="AUD"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <span>AUD</span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="right">
                    <table class="detail_table">
                        <tr>
                            <td class="detail_table_td_100">
                                <span>No</span>
                                <asp:DropDownList ID="ddlNo" runat="server">
                                    <asp:ListItem Text="1"></asp:ListItem>
                                    <asp:ListItem Text="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblNoFrom" Text="227" runat="server" />
                                <span>-</span>
                                <asp:Label ID="lblNoTo" Text="0" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Status</span>
                            </td>
                            <td>
                                <asp:Label ID="lblStatus" Text="" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Posting Date</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPostingDate" runat="server" CssClass="txtDate"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Vendor Ref No.</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="txtDate"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Document Date</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDocumentDate" runat="server" CssClass="txtDate"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="clear">
            </div>
            <div id="content-form">
                <div id="tabs">
                    <ul>
                        <li><a href="#tabs-1">Contents</a></li>
                        <li><a href="#tabs-2">Logistics</a></li>
                        <li><a href="#tabs-3">Accounting</a></li>
                        <li><a href="#tabs-4">Attachment</a></li>
                    </ul>
                    <div id="tabs-1">
                        <asp:ListView ID="lvContents" runat="server">
                            <LayoutTemplate>
                                <table class="data_table">
                                    <tr>
                                        <th style="width: 25px;">
                                            <span>#</span>
                                        </th>
                                        <th>
                                            <span>Item No.</span>
                                        </th>
                                        <th>
                                            <span>Quantity</span>
                                        </th>
                                        <th>
                                            <span>Unit Price</span>
                                        </th>
                                        <th>
                                            <span>Discount %</span>
                                        </th>
                                        <th>
                                            <span>Taxcode %</span>
                                        </th>
                                        <th>
                                            <span>Total(LC)</span>
                                        </th>
                                        <th>
                                            <span>Whse</span>
                                        </th>
                                        <th>
                                            <span>Blanket Agreement</span>
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="Label9"><%#Eval("No")%></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label1"><%#Eval("Code") %></asp:Label>
                                        <asp:HyperLink ID="linkItems" NavigateUrl='<%# String.Format("javascript:Main.openEditItem({0})", Eval("No"))%>'
                                            runat="server">
                                            <asp:Image ID="imgItems" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label2"><%#Eval("Quantity")%></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label3"><%#Eval("UnitPrice")%></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label4"><%#Eval("Discount")%></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label5"><%#Eval("Taxcode")%></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label6"><%#Eval("Total")%></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label7"><%#Eval("Whse")%></asp:Label>
                                        <asp:HyperLink ID="linkWarehouseLoad" NavigateUrl='<%# String.Format("javascript:Main.openEditWareHouse({0})", Eval("No"))%>'
                                            runat="server">
                                            <asp:Image ID="imgWarehouseLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="Label8"><%#Eval("BlanketAgreement")%></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table class="data_table">
                                    <tr>
                                        <th>
                                            <span>#</span>
                                        </th>
                                        <th>
                                            <span>Item No.</span>
                                        </th>
                                        <th>
                                            <span>Quantity</span>
                                        </th>
                                        <th>
                                            <span>Unit Price</span>
                                        </th>
                                        <th>
                                            <span>Discount %</span>
                                        </th>
                                        <th>
                                            <span>Taxcode %</span>
                                        </th>
                                        <th>
                                            <span>Total(LC)</span>
                                        </th>
                                        <th>
                                            <span>Whse</span>
                                        </th>
                                        <th>
                                            <span>Blanket Agreement</span>
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
                    </div>
                    <div id="tabs-2">
                        TBD.</div>
                    <div id="tabs-3">
                        TBD.</div>
                    <div id="tabs-4">
                        TBD.</div>
                </div>
                <div class="left">
                    <table class="detail_table">
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Buyer</span>
                            </td>
                            <td class="detail_table_td_100">
                                <asp:DropDownList ID="ddlBuyer" runat="server">
                                    <asp:ListItem Text="Riahk"></asp:ListItem>
                                    <asp:ListItem Text="Nguyen"></asp:ListItem>
                                    <asp:ListItem Text="Sum"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Owner</span>
                            </td>
                            <td class="detail_table_td_100">
                                <asp:TextBox ID="txtOwner" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Remark</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemark" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="right">
                    <table class="detail_table">
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Total before discount</span>
                            </td>
                            <td>
                                <asp:Label ID="txtTotalDiscount" Text="AUD 250.00" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Discount </span>
                                <asp:TextBox ID="txtDiscountPercent" runat="server" Width="30px"></asp:TextBox><span>%</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDiscount" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Freight</span>
                            </td>
                            <td>
                                <asp:Label ID="lblFreight" Text="" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Rounding</span>
                            </td>
                            <td>
                                <asp:Label ID="lblRounding" Text="" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Tax</span>
                            </td>
                            <td>
                                <asp:Label ID="lblTax" Text="" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100">
                                <span>Total Payment Due</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalPayment" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="clear">
                </div>
                <div class="left">
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="clear">
            </div>
            <div id="footer-form">
                <div class="left">
                    <asp:Button ID="btnAdd" Text="Add" runat="server" />
                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" />
                </div>
                <div class="right">
                    <asp:Button ID="btnCopyFrom" Text="Copy From" runat="server" />
                    <asp:Button ID="btnCopyTo" Text="Copy To" runat="server" />
                </div>
                <div class="clear">
                </div>
            </div>
            <div id="dialogEditItem">
                <iframe id="iframeEditItem" width="100%" height="100%" frameborder="0" marginwidth="0"
                    marginheight="0"></iframe>
            </div>
            <div id="dialogEditVendor">
                <iframe id="iframeEditVendor" width="100%" height="100%" frameborder="0" marginwidth="0"
                    marginheight="0"></iframe>
            </div>
            <div id="dialogEditWareHouse">
                <iframe id="iframeEditWareHouse" width="100%" height="100%" frameborder="0" marginwidth="0"
                    marginheight="0"></iframe>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
