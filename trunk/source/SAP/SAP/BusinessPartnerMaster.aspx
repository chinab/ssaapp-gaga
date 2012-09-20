<%@ Page Title="AR Invoice" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="BusinessPartnerMaster.aspx.cs" Inherits="SAP.BusinessPartnerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Main.myUpdatePanelId = '<%= purchaseOrderUpdatePanel.ClientID %>';
    </script>
    <asp:UpdatePanel ID="purchaseOrderUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="contentData" style="padding-left: 15px;">
                <div id="title-form" style="border-bottom: 2px solid black;">
                    <h2>
                        Business Partner Master</h2>
                    <asp:Label BackColor="Red" ID="lblResults" runat="server" />
                </div>
                <div id="header-form">
                    <div class="left">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Code</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVendor" runat="server" Width="103px"></asp:TextBox>
                                    <asp:DropDownList ID="ddlBPType" runat="server" Width="82px">
                                        <asp:ListItem Text="Customer" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="Vendor" Value="S"></asp:ListItem>
                                        <asp:ListItem Text="Lead" Value="L"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Name</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName0" runat="server" Width="189px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Foreign Name</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" Width="188px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Group</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlContactPerson" runat="server" Width="193px" 
                                        onselectedindexchanged="ddlContactPerson_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Federal Tax ID</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRefNo" runat="server" Width="187px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Currency</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCurrencyDetail" runat="server"  Width="193px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="right">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_150">
                                    &nbsp;</td>
                                <td>
                                    <asp:DropDownList ID="ddlContactPerson0" runat="server">
                                        <asp:ListItem Text="Local Currency" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="System Curreny" Value="S"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_150" style="width: 98px">
                                    <span>Account Balance</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStatus0" runat="server" Enabled="false" Text=""></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="width: 98px">
                                    <span>Deliveries</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStatus" runat="server" Enabled="false" Text=""></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="width: 98px">
                                    <span>Orders</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPostingDate" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="width: 98px">
                                    <span>Opportunities</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDeliveryDate" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="width: 98px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="clear">
                </div>
                <div id="content-form">
                    <div id="tabs">
                        <ul>
                            <li><a href="#tabs-1">General</a></li>
                            <li><a href="#tabs-2">Contact Person</a></li>
                            <li><a href="#tabs-3">Address</a></li>
                            <li><a href="#tabs-4">Payment Terms</a></li>
                            <li><a href="#tabs-5">Accounting</a></li>
                            <li><a href="#tabs-6">Properties</a></li>
                            <li><a href="#tabs-7">Remark</a></li>
                        </ul>
                        <div id="tabs-1">
                            <table class="detail_table">
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Tel 1</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark0" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="width: 131px">
                                        <span>Contact Person</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject0" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Tel 2</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark1" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="width: 131px">
                                        <span>ID No. 2</span></td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject1" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Mobile Phone</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark2" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="width: 131px">
                                        <span>Unified Federal Tax</span></td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject2" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Fax</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark3" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="width: 131px">
                                        <span>Buyer</span></td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject3" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Email</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark4" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="width: 131px">
                                        <span>BP Channel Code</span></td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject4" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Website</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark5" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="width: 131px">
                                        <span>Technician</span></td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject5" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Shipping Type</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark6" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="width: 131px">
                                        <span>Territory</span></td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject6" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Password</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark7" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="width: 131px">
                                        <span>Alias Name</span></td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject7" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Project</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark8" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="width: 131px">
                                        <span>BP </span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject8" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Industry</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark9" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="width: 131px">
                                        <span>BP </span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject9" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </div>
                        <div id="tabs-2">
                            <table class="detail_table">
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Ship to</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtShipTo" runat="server" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                        <input type="button" name="btnBrowingShipTo" value="..." />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="cbxSplitPurchaseOrder" Text="Split Purchase Order" runat="server" /><br />
                                    </td>
                                </tr>
                            </table>
                            <div class="clear">
                            </div>
                        </div>
                        <div id="tabs-3">
                            <table class="detail_table">
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Journal Remark</span><span>BP Project</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100">
                                        &nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="detail_table_td_100">
                                        <span>Cancellation Date</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCancellationDate" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="detail_table_td_100">
                                        <span>Required Date</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRequiredDate" runat="server" CssClass="txtDate"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="detail_table_td_100">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Payment Terms</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPaymentTerm" runat="server">
                                            <asp:ListItem Text="---"></asp:ListItem>
                                            <asp:ListItem Text="Cash Basic"></asp:ListItem>
                                            <asp:ListItem Text="2P10Net30"></asp:ListItem>
                                            <asp:ListItem Text="Net30"></asp:ListItem>
                                            <asp:ListItem Text="Defined View"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="detail_table_td_100">
                                        <span>Indicator</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlIndicator" runat="server">
                                            <asp:ListItem Text="---"></asp:ListItem>
                                            <asp:ListItem Text="Define view"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Payment Method</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPaymentMethod" runat="server">
                                            <asp:ListItem Text="---"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="detail_table_td_100">
                                        <span>Federal Tax ID</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ddlFederalTaxID" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Central Bank Incl.</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="txtCentralBankIncl" runat="server">
                                            <asp:ListItem Text="---"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="detail_table_td_100">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="detail_table_td_100">
                                        <span>Order Number</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOrderNumber" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <br />
                        </div>
                        <div ID="tabs-5">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Consolidating BP</span></td>
                                <td>
                                    <asp:TextBox ID="txtJournalRemark13" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:CheckBox ID="cbxSplitPurchaseOrder2" runat="server" 
                                        Text="Split Purchase Order" />
                                    &nbsp;<asp:CheckBox ID="cbxSplitPurchaseOrder3" runat="server" 
                                        Text="Split Purchase Order" />
                                </td>
                                <td class="detail_table_td_100" style="width: 131px">
                                    <span>Contact Person</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBPProject13" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Accounts Receivable</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtJournalRemark14" runat="server"></asp:TextBox>
                                </td>
                                <td class="detail_table_td_100" style="width: 131px">
                                    <span>ID No. 2</span></td>
                                <td>
                                    <asp:TextBox ID="txtBPProject14" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Mobile Phone</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtJournalRemark15" runat="server"></asp:TextBox>
                                </td>
                                <td class="detail_table_td_100" style="width: 131px">
                                    <span>Unified Federal Tax</span></td>
                                <td>
                                    <asp:TextBox ID="txtBPProject15" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </div>
                    </div>
                    
                    <br />
                    <div class="left">
                    </div>
                    <div class="right">
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
                        <asp:Button ID="btnAdd" Text="Add" runat="server"/>
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" />
                    </div>
                    <div class="right">
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
