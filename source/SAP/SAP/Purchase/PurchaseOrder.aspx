<%@ Page Title="Purchase Order" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="PurchaseOrder.aspx.cs" Inherits="SAP.PurchaseOrder" %>

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
                            <asp:ImageButton ImageUrl="/skin/icon/email.png" runat="server" ID="btnEmail"  CssClass="imagebutton"
                                Height="22px" onclick="btnEmail_Click"  OnClientClick="Dialog.showLoader();" /> </a></li>
                        <li><a href="#">
                            <img alt="" src="/skin/icon/excel.png" /></a></li>
                        <li><a href="#">
                            <img alt="" src="/skin/icon/pdf.png" /></a></li>
                        <li><a href="#">
                            <img alt="" src="/skin/icon/word.png" /></a></li>
                         
                        <li>
                            <asp:HyperLink ID="linkNew" runat="server"> <asp:Image ID="Image8" runat="server" ImageUrl="~/skin/icon/addnew.png" /></asp:HyperLink>
                        </li>

                        <li>
                            <asp:HyperLink ID="linkFirst" runat="server"> <asp:Image ID="Image4" runat="server" ImageUrl="~/skin/icon/first.png" /></asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="linkPrevious" runat="server"> <asp:Image ID="Image5" runat="server" ImageUrl="~/skin/icon/previous.png" /></asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="linkNext" runat="server"> <asp:Image ID="Image6" runat="server" ImageUrl="~/skin/icon/next.png" /></asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="linkLast" runat="server"> <asp:Image ID="Image7" runat="server" ImageUrl="~/skin/icon/last.png" /></asp:HyperLink>
                        </li>
                 </ul>                    
                </div>
                <div class="clear">
                </div>
                <div id="title-form" style="border-bottom: 2px solid black;">
                    <h2>
                        PURCHASE ORDER</h2>
                    <asp:Label BackColor="Red" ID="lblResults" runat="server" />
                </div>
                <div id="header-form">
                    <div class="left">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Vendor</span>
                                    <asp:HyperLink ID="lBP" runat="server"> <asp:Image ID="Image2" runat="server" ImageUrl="~/skin/icon/link.png"/></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVendor" runat="server" Enabled="False"></asp:TextBox>
                                    <asp:HyperLink ID="linkVendorsLoad" NavigateUrl="javascript:Main.openDialog('../Popup_EditBusinessPartner.aspx?CardType=S','');"
                                        runat="server"> <asp:Image ID="imgVendorsLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Name</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Contact Person</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlContactPerson" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Vendor Ref No.</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRefNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </div>
                    <div class="right">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_150" style="border-bottom: dotted 1px #808080;">
                                    <span>No</span>&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtNo" runat="server" Enabled="false" Text=""></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="border-bottom: dotted 1px #808080;">
                                    <span>Status</span>
                                </td>
                                <td>
                                    <asp:TextBox Enabled="false" ID="txtStatus" Text="" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="border-bottom: dotted 1px #808080;">
                                    <span>Posting Date</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPostingDate" runat="server" CssClass="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="border-bottom: dotted 1px #808080;">
                                    <span>Due Date</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDueDate" runat="server" CssClass="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="border-bottom: dotted 1px #808080;">
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
                        <div id="tabs-1" style="overflow: auto; height: 220px; margin-top: 0px;">
                            <asp:Button ID="btnAddRecord" Text="Add" runat="server" OnClick="_btnAddRecord_Click" />
                            <br />
                            <asp:ListView ID="lvContents" runat="server" OnItemInserted="lvContents_ItemInserted"
                                OnItemCommand="lvContents_ItemCommand" OnItemUpdating="lvContents_ItemUpdating"
                                ViewStateMode="Enabled" OnItemEditing="lvContents_ItemEditing">
                                <LayoutTemplate>
                                    <table class="data_table">
                                        <tr>
                                            <th id="thButtons" runat="server" style="width: 78px">
                                            </th>
                                            <th style="width: 25px">
                                                <span>#</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Item No.</span>
                                            </th>
                                            <th style="width: 200px">
                                                <span>Description</span>
                                            </th>
                                            <th style="width: 50px">
                                                <span>Quantity</span>
                                            </th>
                                            <th style="width: 80px">
                                                <span>Unit Price</span>
                                            </th>
                                            <th style="width: 60px">
                                                <span>Discount %</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Price after Discount</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Total(LC)</span>
                                            </th>
                                            <th style="width: 70px">
                                                <span>Tax</span>
                                            </th>
                                            <th style="display: none">
                                                <span>Tax Rate %</span>
                                            </th>
                                            <th style="width: 80px">
                                                <span>Whse</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Profit center</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Dimesion 1</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Dimesion 2</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Dimesion 3</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Dimesion 4</span>
                                            </th>
                                            <th style="display: none">
                                                <span>BaseEntry</span>
                                            </th>
                                            <th style="display: none">
                                                <span>BaseLine</span>
                                            </th>
                                            <th style="display: none">
                                                <span>BaseType</span>
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td Style="text-align: left">
                                            <asp:LinkButton ID="imgbEdit" runat="server" CommandName="Edit" Text="Edit" ImageUrl="~/skin/icon/edit_icon_mono.gif" />
                                            <asp:LinkButton ID="imgbDelete" runat="server" CommandName="DeleteItem" Text="Delete"
                                                ImageUrl="~/skin/icon/delete_icon_mono.gif" OnClientClick="return confirm('Are you sure you want to delete this row?');"
                                                ToolTip="Delete" />
                                        </td>
                                        <td Style="text-align: left">
                                            <asp:Label runat="server" ID="lblNo" Text='<%#Eval("No")%>'></asp:Label>
                                        </td>
                                        <td Style="text-align: left">
                                            <asp:HyperLink ID="litem" runat="server" NavigateUrl='<%# "../Inventory/ItemMasterData.aspx?itemcode=" + Eval("ItemCode").ToString() %>' >
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/skin/icon/link.png"/>
                                            </asp:HyperLink>
                                            <asp:Label runat="server" ID="lblCode"><%#Eval("ItemCode") %></asp:Label>
                                        </td>
                                        <td Style="text-align: left">
                                            <asp:Label runat="server" ID="lblDescription" ><%#Eval("Dscription")%></asp:Label>
                                        </td>
                                        <td Style="text-align: right">
                                            <asp:Label runat="server" ID="lblQuantity" Text='<%# Eval("Quantity") %>'></asp:Label>
                                        </td>
                                        <td Style="text-align: right">
                                            <asp:Label runat="server" ID="lblUnitPrice"><%#Eval("PriceBefDi")%></asp:Label>
                                        </td>
                                        <td Style="text-align: right">
                                            <asp:Label runat="server" ID="lblDiscount"><%#Eval("DiscPrcnt")%></asp:Label>
                                        </td>
                                        <td Style="text-align: right">
                                            <asp:Label runat="server" ID="lblPriceAfterDiscount"><%#Eval("Price")%></asp:Label>
                                        </td>
                                        <td Style="text-align: right">
                                            <asp:Label runat="server" ID="lblTotal"><%#Eval("LineTotal")%></asp:Label>
                                        </td>
                                        <td Style="text-align: left">
                                            <asp:Label runat="server" ID="lblTaxcode"><%#Eval("Taxcode")%></asp:Label>
                                        </td>
                                        <td Style="text-align: left">
                                            <asp:Label runat="server" ID="lblWhse"><%#Eval("WhsCode")%></asp:Label>
                                        </td>
                                        <td style="display: none">
                                            <asp:Label runat="server" ID="Label1"><%#Eval("VatPrcnt")%></asp:Label>
                                        </td>
                                        <td Style="text-align: left">
                                            <asp:Label runat="server" ID="Label2"><%#Eval("OcrCode")%></asp:Label>
                                        </td>
                                        <td Style="text-align: left">
                                            <asp:Label runat="server" ID="Label3"><%#Eval("OcrCode2")%></asp:Label>
                                        </td>
                                        <td Style="text-align: left">
                                            <asp:Label runat="server" ID="Label4"><%#Eval("OcrCode3")%></asp:Label>
                                        </td>
                                        <td Style="text-align: left">
                                            <asp:Label runat="server" ID="Label5"><%#Eval("OcrCode4")%></asp:Label>
                                        </td>
                                        <td Style="text-align: left">
                                            <asp:Label runat="server" ID="Label6"><%#Eval("OcrCode5")%></asp:Label>
                                        </td>
                                        <td style="display: none">
                                            <asp:Label runat="server" ID="Label7"><%#Eval("BaseEntry")%></asp:Label>
                                        </td>
                                        <td style="display: none">
                                            <asp:Label runat="server" ID="Label8"><%#Eval("BaseLine")%></asp:Label>
                                        </td>
                                        <td style="display: none">
                                            <asp:Label runat="server" ID="Label9"><%#Eval("BaseType")%></asp:Label>
                                        </td>
                                       
                                    </tr>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="imgbUpdate" runat="server" CommandName="Update" Text="Update"
                                                ImageUrl="~/skin/icon/save_icon_mono.gif" CausesValidation="true" ValidationGroup="vgrpSaveContact" />
                                            <asp:LinkButton ID="imgbCancel" runat="server" CommandName="CancelUpdate" Text="Cancel"
                                                ImageUrl="~/skin/icon/undo_icon_mono.gif" CausesValidation="false" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Bind("No") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("ItemCode") %>' />
                                            <asp:HyperLink ID="linkItems" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"../Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server"> <asp:Image ID="imgItems" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Dscription")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQuantity" Style="text-align: right" runat="server" Text='<%#Eval("Quantity")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUnitPrice" Style="text-align: right" runat="server" Text='<%# Bind("PriceBefDi")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtDiscount" Style="text-align: right" Text='<%#Bind("DiscPrcnt")%>' />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblPriceAfterDiscount" Text='<%#Bind("Price")%>' />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblTotal" Text='<%#Bind("LineTotal")%>' />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblTaxcode" Text='<%#Bind("Taxcode")%>' />
                                            <asp:HyperLink ID="linkTaxCodeLoad" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"../Popup_EditTaxCode.aspx\",{0})", "\"id=" + Eval("No").ToString()+"&TaxType=I"+"\"")%>'
                                                runat="server"> <asp:Image ID="imgTaxCodeLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblWhse" Text='<%#Bind("WhsCode")%>' />
                                            <asp:HyperLink ID="linkWarehouseLoad" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"../Popup_EditWareHouse.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server"> <asp:Image ID="imgWarehouseLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                        </td>
                                        <td style="display: none">
                                            <asp:Label runat="server" ID="lblTaxRate" Text='<%#Bind("VatPrcnt")%>' />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblProfitCode" Text='<%#Bind("OcrCode")%>' />
                                            <asp:HyperLink ID="HyperLink7" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"../Popup_EditCostCenter.aspx\",{0})", "\"id=" + Eval("No").ToString()+"&Dimension=1\"")%>'
                                                runat="server"> <asp:Image ID="Image9" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblCC1" Text='<%#Bind("OcrCode2")%>' />
                                            <asp:HyperLink ID="HyperLink8" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"../Popup_EditCostCenter.aspx\",{0})", "\"id=" + Eval("No").ToString()+"&Dimension=2\"")%>'
                                                runat="server"> <asp:Image ID="Image10" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblCC2" Text='<%#Bind("OcrCode3")%>' />
                                            <asp:HyperLink ID="HyperLink9" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"../Popup_EditCostCenter.aspx\",{0})", "\"id=" + Eval("No").ToString()+"&Dimension=3\"")%>'
                                                runat="server"> <asp:Image ID="Image11" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblCC3" Text='<%#Bind("OcrCode4")%>' />
                                            <asp:HyperLink ID="HyperLink10" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"../Popup_EditCostCenter.aspx\",{0})", "\"id=" + Eval("No").ToString()+"&Dimension=4\"")%>'
                                                runat="server"> <asp:Image ID="Image12" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblCC4" Text='<%#Eval("OcrCode5")%>' />
                                            <asp:HyperLink ID="HyperLink11" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"../Popup_EditCostCenter.aspx\",{0})", "\"id=" + Eval("No").ToString()+"&Dimension=5\"")%>'
                                                runat="server"> <asp:Image ID="Image13" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                        </td>
                                        <td style="display: none">
                                            <asp:Label runat="server" ID="lblBaseEntry"><%#Eval("BaseEntry")%></asp:Label>
                                        </td>
                                        <td style="display: none">
                                            <asp:Label runat="server" ID="lblBaseLine"><%#Eval("BaseLine")%></asp:Label>
                                        </td>
                                        <td style="display: none">
                                            <asp:Label runat="server" ID="lblBaseType"><%#Eval("BaseType")%></asp:Label>
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
                                            <th style="display: none">
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
                                            <th style="display: none">
                                                <span>BaseEntry</span>
                                            </th>
                                            <th style="display: none">
                                                <span>BaseLine</span>
                                            </th>
                                            <th style="display: none">
                                                <span>BaseType</span>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="15">
                                                <span>No Data</span>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                            <asp:DataPager ID="ProductListPagerCombo" runat="server" PagedControlID="lvContents"
                                PageSize="7" OnPreRender="ProductListPagerCombo_PreRender">
                                <Fields>
                                    <asp:NextPreviousPagerField FirstPageText="&lt;&lt;" ShowFirstPageButton="True" ShowNextPageButton="False"
                                        ShowPreviousPageButton="False" />
                                    <asp:NumericPagerField />
                                    <asp:NextPreviousPagerField LastPageText="&gt;&gt;" ShowLastPageButton="True" ShowNextPageButton="False"
                                        ShowPreviousPageButton="False" />
                                </Fields>
                            </asp:DataPager>
                        </div>
                        <div id="tabs-2">
                            <table class="detail_table">
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Ship to</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtShipTo" runat="server" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Pay to</span>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtPayTo" runat="server" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Shipping Type</span>
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlShippingType" runat="server">
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
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Journal Remark</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>BP Project</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtProjectCode" runat="server" Enabled="False"></asp:TextBox>
                                        <asp:HyperLink ID="linkOwner0" runat="server" NavigateUrl="javascript:Main.openDialog('../Popup_EditProject.aspx','');"> <asp:Image ID="Image3" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Cancellation Date</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCancellationDate" runat="server"  CssClass="txtDate"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
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
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Payment Terms</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPaymentTerm" runat="server">
                                            <asp:ListItem Text=""></asp:ListItem>
                                            <asp:ListItem Text="Defined New"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Indicator</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlIndicator" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                   
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Federal Tax ID</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFederalTaxID" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    
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
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
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
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Buyer</span>
                                </td>
                                <td class="detail_table_td_100">
                                    <asp:DropDownList ID="ddlBuyer" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Owner</span>
                                </td>
                                <td class="detail_table_td_100">
                                    <asp:TextBox ID="txtOwner" runat="server" Enabled="False"></asp:TextBox>
                                    <asp:TextBox ID="txtOwnerCode" runat="server" Visible="False"></asp:TextBox>
                                    <asp:HyperLink ID="linkOwner" NavigateUrl="javascript:Main.openDialog('../Popup_EditEmployee.aspx','');"
                                        runat="server"> <asp:Image ID="Image1" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Remark</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemarks" runat="server" Height="40px" TextMode="MultiLine" Width="420px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="right">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_150" style="border-bottom: dotted 1px #808080;">
                                    <span>Total before discount</span>
                                </td>
                                <td>
                                    <asp:TextBox Enabled="false" ID="txtTotalDiscount" Style="text-align: right" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="border-bottom: dotted 1px #808080;">
                                    <span>Tax</span>
                                </td>
                                <td>
                                    <asp:TextBox Enabled="false" ID="txtTax" Style="text-align: right" runat="server"
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="border-bottom: dotted 1px #808080;">
                                    <span>Total Payment Due</span>
                                </td>
                                <td>
                                    <asp:TextBox Enabled="false" ID="txtTotalPayment" Style="text-align: right" runat="server"></asp:TextBox>
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
                        <asp:ImageButton ID="btnAdd" runat="server" AlternateText="button" OnClientClick="Dialog.showLoader();"
                            CommandName="Login" Height="19px" Width="65px" ImageUrl="~/skin/images/SAP_Add.png"
                            OnClick="btnAdd_Click" />
                        <asp:ImageButton ID="btnUpdate" runat="server" AlternateText="button" 
                             Height="19px" Width="65px" 
                            ImageUrl="~/skin/images/SAP_update.png" onclick="btnAdd_Click" OnClientClick="Dialog.showLoader();"/>
                        <asp:ImageButton ID="btnCopyTo" runat="server" AlternateText="button" OnClientClick="Dialog.showLoader();"
                                CommandName="Login" Height="19px" Width="100px" 
                            ImageUrl="~/skin/images/CopyTo.png" onclick="btnCopyTo_Click"/>
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
