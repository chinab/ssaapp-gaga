<%@ Page Title="Business Partner Master" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="BusinessPartnerMaster.aspx.cs" Inherits="SAP.BusinessPartnerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Main.myUpdatePanelId = '<%= purchaseOrderUpdatePanel.ClientID %>';
    </script>
    <asp:UpdatePanel ID="purchaseOrderUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="contentData" style="padding-left: 15px;">
            <div id="menu-action" style="margin-left: -15px;">
                    <ul>
                        <li><a href="#">
                            <img alt="" src="/skin/icon/preview.png" /></a></li>
                        <li><a href="#">
                            <img alt="" src="/skin/icon/print.png" /></a></li>
                        <li><a href="#">
                            <img alt="" src="/skin/icon/email.png" /></a></li>
                        <li><a href="#">
                            <img alt="" src="/skin/icon/excel.png" /></a></li>
                        <li><a href="#">
                            <img alt="" src="/skin/icon/pdf.png" /></a></li>
                        <li><a href="#">
                            <img alt="" src="/skin/icon/word.png" /></a></li>
                        
                         <li>
                            <asp:HyperLink ID="linkNew" runat="server">
                                <asp:Image ID="Image4" runat="server" ImageUrl="~/skin/icon/addnew.png" />
                            </asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="linkFirst" runat="server">
                                <asp:Image ID="Image5" runat="server" ImageUrl="~/skin/icon/first.png" />
                            </asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="linkPrevious" runat="server">
                                <asp:Image ID="Image6" runat="server" ImageUrl="~/skin/icon/previous.png" />
                            </asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="linkNext" runat="server">
                                <asp:Image ID="Image7" runat="server" ImageUrl="~/skin/icon/next.png" />
                            </asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="linkLast" runat="server">
                                <asp:Image ID="Image8" runat="server" ImageUrl="~/skin/icon/last.png" />
                            </asp:HyperLink>
                        </li>
                 </ul>                    
                </div>
                <div id="title-form" style="border-bottom: 2px solid black;">
                    <h2>
                        Business Partner Master</h2>
                    <asp:Label BackColor="Red" ID="lblResults" runat="server" />
                </div>
                <div id="header-form">
                    <div class="left">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Code</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCardCode" runat="server" Width="103px"></asp:TextBox>
                                    <asp:DropDownList ID="ddlCardType" runat="server" Width="82px" 
                                        onselectedindexchanged="ddlCardType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Customer" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="Vendor" Value="S"></asp:ListItem>
                                        <asp:ListItem Text="Lead" Value="L"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Name</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCardName" runat="server" Width="189px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Foreign Name</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFName" runat="server" Width="188px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Group</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBPGroup" runat="server" Width="193px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Federal Tax ID</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLicTradNum" runat="server" Width="187px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Currency</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCurrency" runat="server"  Width="193px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="right">
                        
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_150" style="width: 98px;border-bottom: dotted 1px #808080;">
                                    <span></span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem Text="Local Currency" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="System Curreny" Value="S"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="width: 98px;border-bottom: dotted 1px #808080;">
                                    <span>Account Balance</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAcctBalance" runat="server" Enabled="false" Text=""></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="width: 98px;border-bottom: dotted 1px #808080;">
                                    <span>Deliveries</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDelivery" runat="server" Enabled="false" Text=""></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="width: 98px;border-bottom: dotted 1px #808080;">
                                    <span>Orders</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrder" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="width: 98px;border-bottom: dotted 1px #808080;">
                                    <span>Opportunities</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOpportunity" runat="server" Enabled="false"></asp:TextBox>
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
                            <li><a href="#tabs-1">General</a></li>
                           <%-- <li><a href="#tabs-2">Contact Person</a></li>
                            <li><a href="#tabs-3">Address</a></li>
                            <li><a href="#tabs-4">Payment Terms</a></li>
                            <li><a href="#tabs-5">Accounting</a></li>
                            <li><a href="#tabs-6">Properties</a></li>
                            <li><a href="#tabs-7">Remark</a></li>--%>
                        </ul>
                        <div id="tabs-1">
                            <table class="detail_table">
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Tel 1</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTel1" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Contact Person</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactPerson" runat="server" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Tel 2</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTel2" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>ID No. 2</span></td>
                                    <td>
                                        <asp:TextBox ID="txtIDNo2" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Mobile Phone</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Unified Federal Tax</span></td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject2" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Fax</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Buyer</span></td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject3" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Email</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>BP Channel Code</span></td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject4" runat="server"></asp:TextBox>
                                        <asp:HyperLink ID="HyperLink1" NavigateUrl="javascript:Main.openDialog('Popup_EditVendor.aspx','');"
                                        runat="server">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Website</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWebsite" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Technician</span></td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject5" runat="server"></asp:TextBox>
                                        <asp:HyperLink ID="HyperLink2" NavigateUrl="javascript:Main.openDialog('Popup_EditVendor.aspx','');"
                                        runat="server">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Shipping Type</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlShipping" runat="server" Width="130px" >
                                        </asp:DropDownList>
                                    </td>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Territory</span></td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject6" runat="server"></asp:TextBox>
                                        <asp:HyperLink ID="HyperLink3" NavigateUrl="javascript:Main.openDialog('Popup_EditVendor.aspx','');"
                                        runat="server">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Password</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Alias Name</span></td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject7" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Project</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtProject" runat="server"></asp:TextBox>
                                        <asp:HyperLink ID="linkVendorsLoad" NavigateUrl="javascript:Main.openDialog('Popup_EditVendor.aspx','');"
                                        runat="server">
                                        <asp:Image ID="imgVendorsLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
                                    </td>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>BP </span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject8" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                       <%-- <div id="tabs-2">
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
                        </table>
                    </div>--%>
                    </div>
                    
                    <br />
                </div>
                <div id="footer-form">
                    <div class="left">
                        <asp:ImageButton ID="btnAdd" runat="server" AlternateText="button" OnClientClick="Dialog.showLoader();"
                            CommandName="Login" Height="19px" Width="65px" ImageUrl="~/skin/images/SAP_Add.png"
                            OnClick="btnAdd_Click" />
                        <asp:ImageButton ID="btnUpdate" runat="server" AlternateText="button" 
                             Height="19px" Width="65px" 
                            ImageUrl="~/skin/images/SAP_update.png" onclick="btnAdd_Click" OnClientClick="Dialog.showLoader();"/>
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
