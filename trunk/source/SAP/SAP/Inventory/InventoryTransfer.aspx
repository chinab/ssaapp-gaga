<%@ Page Title="Inventory Transfer" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="InventoryTransfer.aspx.cs" Inherits="SAP.InventoryTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Main.myUpdatePanelId = '<%= SalesOpportunityUpdatePanel.ClientID %>';
    </script>
    <asp:UpdatePanel ID="SalesOpportunityUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="contentData" style="padding-left: 15px;">
                <div id="title-form" style="border-bottom: 2px solid black;">
                    <h2>Inventory Transfer</h2>
                </div>
                <div id="header-form">
                    <div class="left">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Business Partner</span>
                                    <asp:HyperLink ID="lBP" runat="server">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/skin/icon/link.png"/>
                                    </asp:HyperLink>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBP" runat="server" Enabled="False"></asp:TextBox>
                                    <asp:HyperLink ID="linkVendorsLoad" NavigateUrl="javascript:Main.openDialog('../Popup_EditCustomer.aspx','');"
                                        runat="server">
                                        <asp:Image ID="imgVendorsLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Name</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBPName" runat="server" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Contact Person</span>
                                </td>
                                    <td>
                                         <asp:DropDownList ID="ddlContactPerson" runat="server" Width="120px" 
                                        Enabled="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Ship To</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddress" runat="server" Width="204px" Height="65px" 
                                        TextMode="MultiLine" ></asp:TextBox>
                                </td>
                            </tr>
                            
                        </table>
                    </div>
                    <div class="right">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Posting Date</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPostingDate" runat="server" class="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Document Date</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDocumentDate" runat="server"  class="txtDate" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Remarks</span>
                                </td>
                                    <td>
                                         <asp:TextBox ID="txtRemarks" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Journal Remark</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtJournalRemark" runat="server" Width="250px" ></asp:TextBox>
                                </td>
                            </tr>
                            
                        </table>
                    </div>
                    <div class="right">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>From Warehouse</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFromWarehouse" runat="server" class="txtDate" 
                                        Enabled="False"></asp:TextBox>
                                    <asp:HyperLink ID="HyperLink1" NavigateUrl="javascript:Main.openDialog('../Popup_EditWareHouse.aspx','');"
                                        runat="server">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Price List</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPriceList" runat="server" Width="120px">
                                    </asp:DropDownList>
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
                            <li><a href="#">Attachments</a></li>
                        </ul>
                        <div id="tabs-1">
                            <asp:Button ID="btnAddRecord" Text="Add" runat="server" 
                                OnClick="_btnAddRecord_Click" />
                            
                            <asp:ListView ID="lvStage" runat="server"
                                OnItemInserted="lvStage_ItemInserted"
                                OnItemInserting="lvStage_ItemInserting" OnItemCommand="lvStage_ItemCommand"
                                OnItemEditing="lvStage_ItemEditing" onitemupdating="lvStage_ItemUpdating" 
                                ViewStateMode="Enabled" onitemcreated="lvStage_ItemCreated" 
                                onitemdeleting="lvStage_ItemDeleting" onitemcanceling="lvStage_ItemCanceling"
                                 >
                                <LayoutTemplate>
                                    <table class="data_table">
                                        <tr>
                                            <th id="thButtons" runat="server" style="width: 70px">
                                            </th>
                                            <th style="width: 25px">
                                                <span>#</span>
                                            </th>
                                            <th >
                                                <span>Item Code</span>
                                            </th>
                                            <th >
                                                <span>Description</span>
                                            </th>
                                            <th >
                                                <span>Quantity</span>
                                            </th>
                                           
                                            <th >
                                                <span>Warehouse</span>
                                            </th>
                                            
                                            
                                        </tr>
                                        <tr ID="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                         <td>
                                            <asp:LinkButton ID="imgbEdit" runat="server" CommandName="Edit" Text="Edit" ImageUrl="~/skin/icon/edit_icon_mono.gif" />
                                            <asp:LinkButton ID="imgbDelete" runat="server" CommandName="Delete" Text="Delete"
                                                ImageUrl="~/skin/icon/delete_icon_mono.gif" OnClientClick="return confirm('Are you sure you want to delete this row?');"
                                                ToolTip="Delete" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNo" runat="server"><%#Eval("No") %></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblItemCode" runat="server"><%#Eval("ItemCode") %></asp:Label>
                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="lblItemName" runat="server"><%#Eval("Dscription")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblQuantity" runat="server"><%#Eval("Quantity")%></asp:Label>
                                        </td>
                                       
                                        <td>
                                            <asp:Label ID="lblWarehouse" runat="server"><%#Eval("WhsCode")%></asp:Label>
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
                                             <asp:Label ID="lblNoEdit" runat="server" Text='<%# Bind("No") %>'/>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>'/>
                                            <asp:HyperLink ID="linkItems" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"../Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="imgItems" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("Dscription") %>'/>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQuantityEdit" runat="server" Text='<%# Bind("Quantity") %>'/>
                                        </td>
                                       
                                        <td>
                                            <asp:Label ID="lblWarehouse" runat="server" Text='<%# Bind("WhsCode") %>'/>
                                            <asp:HyperLink ID="linkWarehouseLoad" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"../Popup_EditWareHouse.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="imgWarehouseLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                        
                                       
                                    </tr>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="imgbUpdate" runat="server" CommandName="Insert" Text="Update"
                                                ImageUrl="~/skin/icon/save_icon_mono.gif" CausesValidation="true" ValidationGroup="vgrpSaveContact" />
                                            <asp:LinkButton ID="imgbCancel" runat="server" CommandName="Cancel" Text="Cancel"
                                                ImageUrl="~/skin/icon/undo_icon_mono.gif" CausesValidation="false" />
                                        </td>
                                        <td>
                                             <asp:Label ID="lblNoInsert" runat="server" Text='<%# Bind("No") %>'/>
                                        </td>
                                        <td>
                                           <asp:Label runat="server" ID="lblItemCode"></asp:Label>
                                            <asp:HyperLink ID="linkItems" NavigateUrl='#'
                                                runat="server">
                                                <asp:Image ID="imgItems" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                             
                                        </td>
                                        <td>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("Dscription") %>'/>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQuantityInsert" runat="server" Text='<%# Bind("Quantity") %>'/>
                                        </td>
                                        
                                        <td>
                                            <asp:Label runat="server" ID="lblWarehouse"></asp:Label>
                                             <asp:HyperLink ID="linkWarehouse" NavigateUrl='#'
                                                runat="server">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                       
                                    </tr>
                                </InsertItemTemplate>
                                <EmptyDataTemplate>
                                    <table class="data_table">
                                        <tr>
                                            <th style="width: 25px">
                                                <span>#</span>
                                            </th>
                                            <th >
                                                <span>Item Code</span>
                                            </th>
                                            <th >
                                                <span>Description</span>
                                            </th>
                                            <th >
                                                <span>Quantity</span>
                                            </th>
                                            <th >
                                                <span>Warehouse</span>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <span>No Data</span>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                            <asp:DataPager ID="ProductListPagerCombo" runat="server" 
                               PagedControlID="lvStage" PageSize="5" 
                                onprerender="ProductListPagerCombo_PreRender">
                               <Fields>
                                  <asp:NextPreviousPagerField FirstPageText="&lt;&lt;" ShowFirstPageButton="True" 
                                         ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                  <asp:NumericPagerField />
                                  <asp:NextPreviousPagerField LastPageText="&gt;&gt;" ShowLastPageButton="True" 
                                         ShowNextPageButton="False" ShowPreviousPageButton="False" />
                               </Fields>
                            </asp:DataPager>
                        </div>                     
                    </div>
                    <br />
                 
                </div>
                <div id="footer-form">
                    <table>
                        <td style="width: 100%;" align="left" valign="bottom">
                        <asp:ImageButton ID="btnAdd" runat="server" AlternateText="button"  OnClientClick="Dialog.showLoader();"
                            CommandName="Login" Height="19px" Width="65px" 
                            ImageUrl="~/skin/images/SAP_Add.png" onclick="btnAdd_Click" />
                        </td>
                    </table>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
