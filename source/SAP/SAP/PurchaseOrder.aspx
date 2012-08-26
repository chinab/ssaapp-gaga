<%@ Page Title="Purchase Order" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="PurchaseOrder.aspx.cs" Inherits="SAP.PurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Main.myUpdatePanelId = '<%= purchaseOrderUpdatePanel.ClientID %>';
    </script>
    <asp:UpdatePanel ID="purchaseOrderUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="contentData" style="padding-left: 15px;">
                <div id="title-form" style="border-bottom: 2px solid black;">
                    <h2>
                        PURCHASE ORDER</h2>
                    <asp:Label BackColor="Red" ID="lblResults" runat="server" />
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
                                    <asp:DropDownList ID="ddlContactPerson" Width="91%" runat="server">
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
                                    <asp:DropDownList ID="ddlCurency" runat="server" AutoPostBack="true" OnSelectedIndexChanged="_ddlCurency_SelectedIndexChanged">
                                        <asp:ListItem Text="Local Currency" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="System Currency" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="BP Currency" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCurrencyDetail" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="right">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_150" style="padding: 0 0 0 0;">
                                    <table style="padding: 0 0 0 0;">
                                        <tr>
                                            <td class="detail_table_td_100">
                                                <span>No</span>
                                            </td>
                                            <td class="detail_table_td_50">
                                                <asp:DropDownList ID="ddlNo" runat="server">
                                                    <asp:ListItem Text="Primary"></asp:ListItem>
                                                    <asp:ListItem Text="Alternative"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="padding: 0 0 0 0;">
                                        <tr>
                                            <td style="width: 50%" align="left">
                                                <asp:TextBox Width="50px" ID="txtNoFrom" Text="" runat="server"></asp:TextBox>
                                                <span>-</span>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:TextBox Width="60px" ID="txtNoTo" Text="" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Status</span>
                                </td>
                                <td>
                                    <asp:TextBox Enabled="false" ID="txtStatus" Text="" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Posting Date</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPostingDate" runat="server" CssClass="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Vendor Ref No.</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
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
                                            <th style="display: none">
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
                                            <asp:Label runat="server" ID="lblNo"><%#Eval("No")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblCode"><%#Eval("Code") %></asp:Label>
                                            <asp:HyperLink ID="linkItems" NavigateUrl='<%# String.Format("javascript:Main.openEditItem({0})", Eval("No"))%>'
                                                runat="server">
                                                <asp:Image ID="imgItems" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblQuantity"><%#Eval("Quantity")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblPrice"><%#Eval("UnitPrice")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDiscount"><%#Eval("Discount")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblTaxcode"><%#Eval("Taxcode")%></asp:Label>
                                            <asp:HyperLink ID="linkTaxCodeLoad" NavigateUrl='<%# String.Format("javascript:Main.openEditTaxCode({0})", Eval("No"))%>'
                                                runat="server">
                                                <asp:Image ID="imgTaxCodeLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblTotal"><%#Eval("Total")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblWhse"><%#Eval("Whse")%></asp:Label>
                                            <asp:HyperLink ID="linkWarehouseLoad" NavigateUrl='<%# String.Format("javascript:Main.openEditWareHouse({0})", Eval("No"))%>'
                                                runat="server">
                                                <asp:Image ID="imgWarehouseLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                        <td style="display: none">
                                            <asp:Label runat="server" ID="lblBlanketAgreement"><%#Eval("BlanketAgreement")%></asp:Label>
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
                                            <th style="display: none">
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
                                    <asp:TextBox ID="txtRemark" runat="server" Height="85px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="right">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Total before discount</span>
                                </td>
                                <td>
                                    <asp:Label ID="txtTotalDiscount" Text="AUD 250.00" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="padding: 0 0 0 0;">
                                    <table style="padding: 0 0 0 0;">
                                        <tr>
                                            <td class="detail_table_td_100">
                                                <span>Discount </span>
                                            </td>
                                            <td class="detail_table_td_50">
                                                <asp:TextBox ID="txtDiscountPercent" runat="server" Width="30px"></asp:TextBox><span>%</span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDiscount" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Freight</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblFreight" Text="" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Rounding</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblRounding" Text="" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Tax</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblTax" Text="" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
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
                        <asp:Button ID="btnAdd" Text="Add" runat="server" OnClick="btnAdd_Click" />
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
                <div id="dialogEditTaxCode">
                    <iframe id="iframeEditTaxCode" width="100%" height="100%" frameborder="0" marginwidth="0"
                        marginheight="0"></iframe>
                </div>
                <div id="dialogError">
                    <iframe id="iframeError" width="100%" height="100%" frameborder="0" marginwidth="0"
                        marginheight="0"></iframe>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
