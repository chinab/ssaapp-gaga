<%@ Page Title="AR Invoice" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ARInvoice_VHF.aspx.cs" Inherits="SAP.ARInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Main.myUpdatePanelId = '<%= purchaseOrderUpdatePanel.ClientID %>';
    </script>
    <asp:UpdatePanel ID="purchaseOrderUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="contentData" style="padding-left: 15px;">
                <div id="title-form" style="border-bottom: 2px solid black;">
                    <h2>
                        AR Invoice</h2>
                    <asp:Label BackColor="Red" ID="lblResults" runat="server" />
                </div>
                <div id="header-form">
                    <div class="left">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Outlet</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVendor" runat="server"></asp:TextBox>
                                    <asp:HyperLink ID="linkVendorsLoad" NavigateUrl="javascript:Main.openDialog('Popup_EditCustomer.aspx','');"
                                        runat="server">
                                        <asp:Image ID="imgVendorsLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Tên</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Người liên hệ</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlContactPerson" Width="88%" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Số đăng ký.</span>
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
                                    <span>Tình trạng</span>
                                </td>
                                <td>
                                    <asp:TextBox Enabled="false" ID="txtStatus" Text="" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Ngày ghi sổ</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPostingDate" runat="server" CssClass="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Ngày thanh toán</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Ngày hóa đơn</span>
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
                            <li><a href="#tabs-1">Sản phẩm</a></li>
                            <li><a href="#">Giao hàng</a></li>
                            <li><a href="#">Kế toán</a></li>
                        </ul>
                        <div id="tabs-1">
                            <asp:ListView ID="lvContents" runat="server">
                                <LayoutTemplate>
                                    <table class="data_table">
                                        <tr>
                                            <th style="width:25px">
                                                <span>#</span>
                                            </th>
                                             <th style="width:50px">
                                                <span>Áp dụng KM</span>
                                            </th>
                                            <th  style="width:100px">
                                                <span>Mã hàng</span>
                                            </th>                                            
                                             <th>
                                                <span>Tên hàng</span>
                                            </th>
                                            <th>
                                                <span>Số lượng</span>
                                            </th>
                                             <th>
                                                <span>Đơn giá</span>
                                            </th>
                                            <th style="display:none">
                                                <span>Chiết khấu</span>
                                            </th>  
                                            <th style="display:none">
                                                <span>Unit Price</span>
                                            </th>
                                            <th style="display:none">
                                                <span>Discount By Contract</span>
                                            </th>
                                            <th style="display:none">
                                                <span>Price after Discount</span>
                                            </th>   
                                            <th>
                                                <span>Chiết khấu</span>
                                            </th>                                          
                                            <th>
                                                <span>Tổng tiền</span>
                                            </th>
                                            <th style="display:none">
                                                <span>Taxcode %</span>
                                            </th>
                                            <th style="display:none">
                                                <span>Tax Rate %</span>
                                            </th>
                                            <th >
                                                <span>Kho</span>
                                            </th>
                                            <th style="display:none">
                                                <span>Promotion ID</span>
                                            </th >
                                            <th style="display:none">
                                                <span>Promotion Line</span>
                                            </th>
                                             <th style="display:none">
                                                <span>Sole</span>                                                
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
                                        <td >                                            
                                            <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_Promo.aspx\",{0})", "\"id=" + Eval("No").ToString()+"&itemCode=" + Eval("Code").ToString()+"&cardCode=" + Eval("CardCode").ToString()+"&quantity=" + Eval("Quantity").ToString()+"&UnitPrice=" + Eval("UnitPrice").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image Visible='<%#"N".Equals(Eval("PromoEnable")) ? false : true%>' ID="Image2" runat="server" ImageUrl="~/skin/images/promocode.png" Width="16px" />
                                            </asp:HyperLink>
                                        </td>                                    
                                        <td>
                                            <asp:Label runat="server" ID="lblCode"><%#Eval("Code") %></asp:Label>
                                            <asp:HyperLink ID="linkItems" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="imgItems" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>                                        
                                         <td>
                                            <asp:Label runat="server" ID="lblDescription"><%#Eval("Description")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox  Enabled='<%#"N".Equals(Eval("QuantityEnable")) ? false : true%>' ID="txtQuantity" runat="server" Text='<%#Eval("Quantity")%>' onkeypress="return Main.integer_textbox_keypress(event);" />
                                            <asp:LinkButton Enabled='<%#"N".Equals(Eval("QuantityEnable")) ? false : true%>' ID="btnQuantityUpdate" Text="u" runat="server" OnClick='btnQuantityUpdate_click' />
                                        </td>
                                         <td>
                                            <asp:Label runat="server" ID="lblOrgPrice"><%#Eval("OrgPrice")%></asp:Label>
                                        </td>
                                        <td style="display:none">
                                            <asp:Label runat="server" ID="lblPromoDiscount"><%#Eval("PromoDiscount")%></asp:Label>
                                        </td>
                                        <td style="display:none">
                                            <asp:Label runat="server" ID="lblUnitPrice"><%#Eval("UnitPrice")%></asp:Label>
                                        </td>
                                        <td style="display:none">
                                            <asp:Label runat="server" ID="lblDiscount"><%#Eval("ContractDiscount")%></asp:Label>
                                        </td>
                                         <td style="display:none">
                                            <asp:Label runat="server" ID="lblPriceAfterDiscount"><%#Eval("PriceAfterDiscount")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblTotalDiscount"><%#Eval("TotalDiscount")%></asp:Label>
                                        </td>
                                         <td>
                                            <asp:Label runat="server" ID="lblTotal"><%#Eval("Total")%></asp:Label>
                                        </td>
                                        <td style="display:none">
                                            <asp:Label runat="server" ID="lblTaxcode"><%#Eval("Taxcode")%></asp:Label>
                                            <asp:HyperLink ID="linkTaxCodeLoad" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditTaxCode_OUT.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="imgTaxCodeLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>                                       
                                        <td>
                                            <asp:Label runat="server" ID="lblWhse"><%#Eval("Whse")%></asp:Label>
                                            <asp:HyperLink ID="linkWarehouseLoad" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditWareHouse.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="imgWarehouseLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                        <td style="display:none">
                                            <asp:Label runat="server" ID="Label1"><%#Eval("TaxRate")%></asp:Label>
                                        </td>
                                        <td style="display:none">
                                            <asp:Label runat="server" ID="lblPromotionId"><%#Eval("PromotionId")%></asp:Label>
                                        </td>
                                          <td style="display:none">
                                            <asp:Label runat="server" ID="lblPromotionLine"><%#Eval("PromotionLine")%></asp:Label>
                                        </td>
                                          <td style="display:none">
                                            <asp:Label runat="server" ID="lblSole"><%#Eval("Sole")%></asp:Label>  
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblNo"><%#Eval("No")%></asp:Label>
                                        </td>  
                                         <td >                                            
                                            <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_Promo.aspx\",{0})", "\"id=" + Eval("No").ToString()+"&itemCode=" + Eval("Code").ToString()+"&cardCode=" + Eval("CardCode").ToString()+"&quantity=" + Eval("Quantity").ToString()+"&UnitPrice=" + Eval("UnitPrice").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image Visible='<%#"N".Equals(Eval("PromoEnable")) ? false : true%>' ID="Image2" runat="server" ImageUrl="~/skin/images/promocode.png" Width="16px" />
                                            </asp:HyperLink>
                                        </td>                                      
                                        <td>
                                            <asp:Label runat="server" ID="lblCode"><%#Eval("Code") %></asp:Label>
                                            <asp:HyperLink ID="linkItems" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="imgItems" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>                                       
                                         <td>
                                            <asp:Label runat="server" ID="lblDescription"><%#Eval("Description")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblQuantity" Text='<%# Bind("Quantity") %>'></asp:TextBox>                                            
                                        </td>
                                         <td>
                                            <asp:Label runat="server" ID="lblOrgPrice"><%#Eval("OrgPrice")%></asp:Label>
                                        </td>
                                        <td style="display:none">
                                            <asp:Label runat="server" ID="lblPromoDiscount"><%#Eval("PromoDiscount")%></asp:Label>
                                        </td>
                                        <td style="display:none">
                                            <asp:Label runat="server" ID="lblUnitPrice"><%#Eval("UnitPrice")%></asp:Label>
                                        </td>
                                        <td style="display:none">
                                            <asp:Label runat="server" ID="lblDiscount"><%#Eval("ContractDiscount")%></asp:Label>
                                        </td>
                                         <td style="display:none">
                                            <asp:Label runat="server" ID="lblPriceAfterDiscount"><%#Eval("PriceAfterDiscount")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblTotalDiscount"><%#Eval("TotalDiscount")%></asp:Label>
                                        </td>
                                         <td>
                                            <asp:Label runat="server" ID="lblTotal"><%#Eval("Total")%></asp:Label>
                                        </td>
                                        <td style="display:none">
                                            <asp:Label runat="server" ID="lblTaxcode"><%#Eval("Taxcode")%></asp:Label>
                                            <asp:HyperLink ID="linkTaxCodeLoad" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditTaxCode_Out.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="imgTaxCodeLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>                                       
                                        <td>
                                            <asp:Label runat="server" ID="lblWhse"><%#Eval("Whse")%></asp:Label>
                                            <asp:HyperLink ID="linkWarehouseLoad" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditWareHouse.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="imgWarehouseLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                        <td style="display:none">
                                            <asp:Label runat="server" ID="Label1"><%#Eval("TaxRate")%></asp:Label>
                                        </td>
                                        <td style="display:none">
                                            <asp:Label runat="server" ID="lblPromotionId"><%#Eval("PromotionId")%></asp:Label>
                                        </td >
                                          <td style="display:none">
                                            <asp:Label runat="server" ID="lblPromotionLine"><%#Eval("PromotionLine")%></asp:Label>
                                        </td>
                                          <td style="display:none">
                                            <asp:Label runat="server" ID="lblSole"><%#Eval("Sole")%></asp:Label>                                          
                                        </td>
                                    </tr>
                                </EditItemTemplate>
                                <EmptyDataTemplate>
                                    <table class="data_table">
                                        <tr>
                                            <th style="width:25px">
                                                <span>#</span>
                                            </th>
                                             <th style="width:50px">
                                                <span>Áp dụng KM</span>
                                            </th>
                                            <th  style="width:100px">
                                                <span>Mã hàng</span>
                                            </th>                                            
                                             <th>
                                                <span>Tên hàng</span>
                                            </th>
                                            <th>
                                                <span>Số lượng</span>
                                            </th>
                                             <th>
                                                <span>Đơn giá</span>
                                            </th>
                                            <th  style="display:none">
                                                <span>Chiết khấu</span>
                                            </th>  
                                            <th style="display:none">
                                                <span>Unit Price</span>
                                            </th>
                                            <th style="display:none">
                                                <span>Discount By Contract</span>
                                            </th>
                                            <th style="display:none">
                                                <span>Price after Discount</span>
                                            </th>  
                                             <th>
                                                <span>Chiết khấu</span>
                                            </th>                                            
                                            <th>
                                                <span>Tổng tiền</span>
                                            </th>
                                            <th style="display:none">
                                                <span>Taxcode %</span>
                                            </th>
                                            <th style="display:none">
                                                <span>Tax Rate %</span>
                                            </th>
                                            <th >
                                                <span>Kho</span>
                                            </th>
                                            <th style="display:none">
                                                <span>Promotion ID</span>
                                            </th >
                                            <th style="display:none">
                                                <span>Promotion Line</span>
                                            </th>
                                             <th style="display:none">
                                                <span>Sole</span>                                                
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
                     
                    </div>
                    <br />
                    <div class="left">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Nhân viên bán hàng</span>
                                </td>
                                <td class="detail_table_td_100">
                                    <asp:DropDownList ID="ddlBuyer" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Người dùng</span>
                                </td>
                                <td class="detail_table_td_100">
                                    <asp:TextBox ID="txtOwner" runat="server"></asp:TextBox>
                                     <asp:HyperLink ID="linkOwner" NavigateUrl="javascript:Main.openDialog('Popup_EditEmployee.aspx','');"
                                        runat="server">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Ghi chú</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemark" runat="server" Height="35px" TextMode="MultiLine" 
                                        Width="265px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="right">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Tổng tiền trước thuế</span>
                                </td>
                                <td>
                                     <asp:TextBox Enabled="false" ID="txtTotalDiscount" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                           
                           
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Thuế</span>
                                </td>
                                <td>
                                    <asp:TextBox Enabled="false" ID="txtTax" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Tổng thanh toán</span>
                                </td>
                                <td>
                                    <asp:TextBox Enabled="false" ID="txtTotalPayment" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="clear">
                </div>
                <div id="footer-form">
                    <div class="left">
                        <asp:Button ID="btnAdd" Text="Add" runat="server" OnClientClick="Dialog.showLoader();" OnClick="btnAdd_Click" />
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
