﻿<%@ Page Title="Purchase Order" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
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
                                    <asp:HyperLink ID="linkVendorsLoad" NavigateUrl="javascript:Main.openDialog('Popup_EditVendor.aspx','');"
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
                                    <asp:DropDownList ID="ddlContactPerson" Width="88%" runat="server">
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
                        </ul>
                        <div id="tabs-1" style="overflow:auto; height:300px;">
                            <asp:ListView ID="lvContents" runat="server">
                                <LayoutTemplate>
                                    <table class="data_table">                                        
                                        <tr> 
                                            <th></th>
                                            <th style="width:25px">
                                                <span>#</span>
                                            </th>                                            
                                            <th  style="width:100px">
                                                <span>Item No.</span>
                                            </th>                                            
                                             <th  style="width:200px">
                                                <span>Description</span>
                                            </th>
                                            <th style="width:100px">
                                                <span>Quantity</span>
                                            </th>
                                            <th style="width:100px">
                                                <span>Unit Price</span>
                                            </th>
                                            <th style="width:100px">
                                                <span>Discount %</span>
                                            </th>
                                            <th style="width:100px">
                                                <span>Price after Discount</span>
                                            </th>                                            
                                            <th style="width:100px">
                                                <span>Total(LC)</span>
                                            </th>
                                            <th style="width:100px">
                                                <span>Taxcode %</span>
                                            </th>
                                            <th style="display:none">
                                                <span>Tax Rate %</span>
                                            </th>
                                            <th  style="width:100px">
                                                <span>Whse</span>
                                            </th>
                                            <th style="width:100px">
                                                <span>Profit center</span>
                                            </th>
                                            <th style="width:100px">
                                                <span>Dimesion 1</span>
                                            </th>
                                            <th style="width:100px">
                                                <span>Dimesion 2</span>
                                            </th>
                                            <th style="width:100px">
                                                <span>Dimesion 3</span>
                                            </th>
                                            <th style="width:100px">
                                                <span>Dimesion 4</span>
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgbEdit" runat="server" 
                                               CommandName="Edit" Text="Edit" 
                                               ImageUrl="~/skin/icon/edit_icon_mono.gif" />
                                            <asp:ImageButton ID="imgbDelete" runat="server" 
                                               CommandName="Delete" Text="Delete" 
                                               ImageUrl="~/skin/icon/delete_icon_mono.gif" 
                                               OnClientClick="return confirm('Are you sure you want to delete this row?');" 
                                               ToolTip="Delete"/>
                                        </td>  
                                        <td>
                                            <asp:Label runat="server" ID="lblNo"><%#Eval("No")%></asp:Label>
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
                                            <asp:Label runat="server" ID="lblUnitPrice"><%#Eval("UnitPrice")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDiscount"><%#Eval("ContractDiscount")%></asp:Label>
                                        </td>
                                         <td>
                                            <asp:Label runat="server" ID="lblPriceAfterDiscount"><%#Eval("PriceAfterDiscount")%></asp:Label>
                                        </td>
                                         <td>
                                            <asp:Label runat="server" ID="lblTotal"><%#Eval("Total")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblTaxcode"><%#Eval("Taxcode")%></asp:Label>
                                            <asp:HyperLink ID="linkTaxCodeLoad" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditTaxCode_IN.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
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
                                         <td>
                                            <asp:Label runat="server" ID="Label2"><%#Eval("ProfitCode")%></asp:Label>
                                            <asp:HyperLink ID="HyperLink2" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                         <td >
                                            <asp:Label runat="server" ID="Label3"><%#Eval("CC1")%></asp:Label>
                                            <asp:HyperLink ID="HyperLink3" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="Image5" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                         <td >
                                            <asp:Label runat="server" ID="Label4"><%#Eval("CC2")%></asp:Label>
                                            <asp:HyperLink ID="HyperLink4" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="Image6" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                         <td >
                                            <asp:Label runat="server" ID="Label5"><%#Eval("CC3")%></asp:Label>
                                            <asp:HyperLink ID="HyperLink5" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="Image7" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                         <td>
                                            <asp:Label runat="server" ID="Label6"><%#Eval("CC4")%></asp:Label>
                                            <asp:HyperLink ID="HyperLink6" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="Image8" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgbUpdate" runat="server" 
                                                CommandName="Update" Text="Update" 
                                                ImageUrl="~/skin/icon/save_icon_mono.gif" 
                                                CausesValidation="true" 
                                                ValidationGroup="vgrpSaveContact"/>
                                            <asp:ImageButton ID="imgbCancel" runat="server" 
                                                CommandName="Cancel" Text="Cancel" 
                                                ImageUrl="~/skin/icon/undo_icon_mono.gif" 
                                                CausesValidation="false"/>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblNo"><%#Eval("No")%></asp:Label>
                                        </td>  
                                         <td >                                            
                                            <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_Promo.aspx\",{0})", "\"id=" + Eval("No").ToString()+"&itemCode=" + Eval("Code").ToString()+"&cardCode=" + Eval("CardCode").ToString()+"&quantity=" + Eval("Quantity").ToString()+"&amount=" + Eval("Total").ToString()+"\"")%>'
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
                                            <asp:Label runat="server" ID="lblUnitPrice"><%#Eval("UnitPrice")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDiscount"><%#Eval("ContractDiscount")%></asp:Label>
                                        </td>
                                         <td>
                                            <asp:Label runat="server" ID="lblPriceAfterDiscount"><%#Eval("PriceAfterDiscount")%></asp:Label>
                                        </td>
                                         <td>
                                            <asp:Label runat="server" ID="lblTotal"><%#Eval("Total")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblTaxcode"><%#Eval("Taxcode")%></asp:Label>
                                            <asp:HyperLink ID="linkTaxCodeLoad" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditTaxCode_IN.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
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
                                       <td>
                                            <asp:Label runat="server" ID="Label2"><%#Eval("ProfitCode")%></asp:Label>
                                            <asp:HyperLink ID="HyperLink7" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="Image9" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                         <td>
                                            <asp:Label runat="server" ID="Label3"><%#Eval("CC1")%></asp:Label>
                                            <asp:HyperLink ID="HyperLink8" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="Image10" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                         <td>
                                            <asp:Label runat="server" ID="Label4"><%#Eval("CC2")%></asp:Label>
                                            <asp:HyperLink ID="HyperLink9" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="Image11" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                         <td>
                                            <asp:Label runat="server" ID="Label5"><%#Eval("CC3")%></asp:Label>
                                            <asp:HyperLink ID="HyperLink10" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="Image12" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                         <td>
                                            <asp:Label runat="server" ID="Label6"><%#Eval("CC4")%></asp:Label>
                                            <asp:HyperLink ID="HyperLink11" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="Image13" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                </EditItemTemplate>
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
                                                <span>Item Description</span>
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
                                                <span>Price after Discount</span>
                                            </th>                                            
                                            <th>
                                                <span>Total(LC)</span>
                                            </th>
                                            <th>
                                                <span>Taxcode %</span>
                                            </th>
                                             <th style="display:none">
                                                <span>Tax Rate</span>
                                            </th>
                                            <th>
                                                <span>Whse</span>
                                            </th>
                                            <th>
                                                <span>Profit center</span>
                                            </th>
                                            <th>
                                                <span>Dimesion 1</span>
                                            </th>
                                            <th>
                                                <span>Dimesion 2</span>
                                            </th>
                                            <th>
                                                <span>Dimesion 3</span>
                                            </th>
                                            <th>
                                                <span>Dimesion 4</span>
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
                                        <asp:CheckBox ID="cbxApproved" Text="Approved" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Pay to</span>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtPayTo" runat="server" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                        <input type="button" name="btnBrowingPayTo" value="..." />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Shipping Type</span>
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlShippingType" runat="server">
                                            <asp:ListItem Text=""></asp:ListItem>                                            
                                            <asp:ListItem Text="Define New"></asp:ListItem>
                                        </asp:DropDownList>
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
                                        <span>Journal Remark</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100">
                                        <span>BP Project</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBPProject" runat="server"></asp:TextBox>
                                        <asp:HyperLink ID="linkOwner0" runat="server" 
                                            NavigateUrl="javascript:Main.openDialog('Popup_EditProject.aspx','');">
                                        <asp:Image ID="Image3" runat="server" 
                                            ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
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
                                            <asp:ListItem Text=""></asp:ListItem>                                           
                                            <asp:ListItem Text="Defined New"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="detail_table_td_100">
                                        <span>Indicator</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlIndicator" runat="server">
                                            <asp:ListItem Text=""></asp:ListItem> 
                                            <asp:ListItem Text="Define New"></asp:ListItem>
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
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="left">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Buyer</span>
                                </td>
                                <td class="detail_table_td_100">
                                    <asp:DropDownList ID="ddlBuyer" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Owner</span>
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
                                     <asp:TextBox Enabled="false" ID="txtTotalDiscount" runat="server"></asp:TextBox>
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
                                                <asp:TextBox  Enabled="false" ID="txtDiscountPercent" runat="server" Width="30px"></asp:TextBox><span>%</span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:TextBox  Enabled="false" ID="txtDiscount" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Freight</span>
                                </td>
                                <td>
                                    <asp:TextBox  Enabled="false" ID="txtFreight" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Rounding</span>
                                </td>
                                <td>
                                    <asp:TextBox Enabled="false" ID="txtRounding" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Tax</span>
                                </td>
                                <td>
                                    <asp:TextBox Enabled="false" ID="txtTax" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Total Payment Due</span>
                                </td>
                                <td>
                                    <asp:TextBox Enabled="false" ID="txtTotalPayment" runat="server"></asp:TextBox>
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
