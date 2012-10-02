<%@ Page Title="Goods Receipt" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="GoodsReceipt.aspx.cs" Inherits="SAP.GoodsReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Main.myUpdatePanelId = '<%= SalesOpportunityUpdatePanel.ClientID %>';
    </script>
    <asp:UpdatePanel ID="SalesOpportunityUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="contentData" style="padding-left: 15px;">
                <div id="title-form" style="border-bottom: 2px solid black;">
                    <h2>Goods Receipt</h2>
                </div>
                <div id="header-form">
                    <div style="width: 706px">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100" style="width: 195px">
                                    <span>Posting Date</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPostingDate" runat="server" class="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="width: 195px">
                                    <span>Document Date</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDocumentDate" runat="server"  class="txtDate" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="width: 195px">
                                    <span>Remarks</span>
                                </td>
                                    <td>
                                         <asp:TextBox ID="txtRemarks" runat="server" Width="495px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="width: 195px">
                                    <span>Journal Remark</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtJournalRemark" runat="server" Width="496px" ></asp:TextBox>
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
                                ViewStateMode="Enabled" onitemcreated="lvStage_ItemCreated" onitemdeleting="lvStage_ItemDeleting"
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
                                                <span>Unit Price</span>
                                            </th>
                                            <th >
                                                <span>Total</span>
                                            </th>
                                            <th >
                                                <span>Warehouse</span>
                                            </th>
                                            <th>
                                                <span>Account Code</span>
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
                                            <asp:Label ID="lblPrice" runat="server"><%#Eval("Price")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotal" runat="server"><%#Eval("LineTotal")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblWarehouse" runat="server"><%#Eval("WhsCode")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAccount" runat="server"><%#Eval("AcctCode")%></asp:Label>
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
                                            <asp:HyperLink ID="linkItems" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
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
                                            <asp:TextBox ID="txtPriceEdit" runat="server" Text='<%# Bind("Price") %>'/>     
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTotalEdit" runat="server" Text='<%# Bind("LineTotal") %>'/>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblWarehouse" runat="server" Text='<%# Bind("WhsCode") %>'/>
                                            <asp:HyperLink ID="linkWarehouseLoad" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditWareHouse.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="imgWarehouseLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                             <asp:Label ID="lblAccount" runat="server" Text='<%#Bind("AcctCode")%>' />
                                             <asp:HyperLink ID="linkAccount" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditAccount.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
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
                                            <asp:TextBox ID="txtPriceInsert" runat="server" Text='<%# Bind("Price") %>'/>     
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTotalInsert" runat="server" Text='<%# Bind("LineTotal") %>'/>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblWarehouse"></asp:Label>
                                             <asp:HyperLink ID="linkWarehouse" NavigateUrl='#'
                                                runat="server">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                             <asp:Label runat="server" ID="lblAccount"></asp:Label>
                                              <asp:HyperLink ID="linkAcount" NavigateUrl='#'
                                                runat="server">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
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
                                                <span>Unit Price</span>
                                            </th>
                                            <th >
                                                <span>Total</span>
                                            </th>
                                            <th >
                                                <span>Warehouse</span>
                                            </th>
                                            <th>
                                                <span>Account Code</span>
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
                    <div class="left">
                        <asp:ImageButton ID="btnAdd" runat="server" AlternateText="button" 
                            CommandName="Login" Height="19px" Width="65px" 
                            ImageUrl="~/skin/images/SAP_Add.png" onclick="btnAdd_Click" />
                    </div>
                    
                    <div class="clear">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
