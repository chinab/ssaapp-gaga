<%@ Page Title="Production Order" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ProductionOrder.aspx.cs" Inherits="SAP.ProductionOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Main.myUpdatePanelId = '<%= SalesOpportunityUpdatePanel.ClientID %>';
    </script>
    <asp:UpdatePanel ID="SalesOpportunityUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="contentData" style="padding-left: 15px;">
                <div id="title-form" style="border-bottom: 2px solid black;">
                    <h2>Production Order</h2>
                </div>
                <div id="header-form">
                    <div class="left">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Type</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlType" runat="server" 
                                        Width="120px">
                                        <asp:ListItem Text="Planned" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Canncelled" Value="R"></asp:ListItem>
                                        <asp:ListItem Text="Released" Value="L"></asp:ListItem>
                                        <asp:ListItem Text="Closed" Value="C"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Status</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server" 
                                        Width="120px">
                                        <asp:ListItem Text="Standard" Value="S"></asp:ListItem>
                                        <asp:ListItem Text="Special" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Disassembly" Value="D"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Product No.</span>
                                </td>
                                    <td>
                                         <asp:TextBox ID="txtItemCode" runat="server" Enabled="False"></asp:TextBox>
                                         <asp:HyperLink ID="linkItems"  NavigateUrl="javascript:Main.openDialog('../Popup_EditBOM.aspx','');"
                                                runat="server">
                                                <asp:Image ID="imgItems" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                         
                                         </asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Product Name</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtItemName" runat="server" Enabled="False" Width="195px" ></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                   <span> Plan Quantity</span>
                                   </td>
                                <td>
                                    <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                                   <span>UOM</span> 
                                   <asp:TextBox ID="txtUOM" runat="server" Enabled="False" Width="40px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Warehouse</span></td>
                                <td>
                                    <asp:TextBox ID="txtWarehouse" runat="server" Enabled="False"></asp:TextBox>
                                    <asp:HyperLink ID="HyperLink6" runat="server" 
                                        NavigateUrl="javascript:Main.openDialog('../Popup_EditWareHouse.aspx','');">
                                        <asp:Image ID="Image9" runat="server" 
                                        ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
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
                                    <span>Due Date</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDueDate" runat="server"  class="txtDate" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                   <span> Sales Order</span>
                                </td>
                                    <td>
                                         <asp:TextBox ID="txtSalesOrder" runat="server"></asp:TextBox>
                                         <asp:HyperLink ID="HyperLink2" runat="server" 
                                             NavigateUrl="javascript:Main.openDialog('../Popup_EditWareHouse.aspx','');">
                                        <asp:Image ID="Image5" runat="server" 
                                             ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Customer</span></td>
                                <td>
                                    <asp:TextBox ID="txtCardCode" runat="server" Enabled="False" ></asp:TextBox>
                                    <asp:HyperLink ID="HyperLink3" runat="server" 
                                        NavigateUrl="javascript:Main.openDialog('../Popup_EditCustomer.aspx','');">
                                        <asp:Image ID="Image6" runat="server" 
                                        ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Distr. Rule</span></td>
                                <td>
                                    <asp:TextBox ID="txtCostCenter" runat="server" class="txtDate" 
                                        Enabled="False"></asp:TextBox>
                                     <asp:HyperLink ID="HyperLink11" NavigateUrl="javascript:Main.openDialog('../Popup_EditCostCenter.aspx?Dimension=1','');"
                                        runat="server">
                                        <asp:Image ID="Image13" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Project</span></td>
                                <td>
                                    <asp:TextBox ID="txtProject" runat="server" Enabled="False"></asp:TextBox>
                                    <asp:HyperLink ID="HyperLink5" runat="server" 
                                        NavigateUrl="javascript:Main.openDialog('../Popup_EditProject.aspx','');">
                                        <asp:Image ID="Image8" runat="server" 
                                        ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
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
                                            <th >
                                                <span>Issue Method</span>
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
                                            <asp:Label ID="lblQuantity" runat="server"><%#Eval("PlannedQty")%></asp:Label>
                                        </td>
                                       
                                        <td>
                                            <asp:Label ID="lblWarehouse" runat="server"><%#Eval("wareHouse")%></asp:Label>
                                        </td>
                                       <td>
                                            <asp:Label ID="lblIssueType" runat="server"><%#Eval("IssueType")%></asp:Label>
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
                                            <asp:TextBox ID="txtQuantityEdit" runat="server" Text='<%# Bind("PlannedQty") %>'/>
                                        </td>
                                       
                                        <td>
                                            <asp:Label ID="lblWarehouse" runat="server" Text='<%# Bind("wareHouse") %>'/>
                                            <asp:HyperLink ID="linkWarehouseLoad" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"../Popup_EditWareHouse.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="imgWarehouseLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>

                                        <td>
                                            <asp:DropDownList ID="ddlIssueType" runat="server">
                                                <asp:ListItem Text="Manual" Value="M"></asp:ListItem>
                                                <asp:ListItem Text="Backflush" Value="B"></asp:ListItem>
                                            </asp:DropDownList> 
                                        </td>
                                    </tr>
                                </EditItemTemplate>
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
                                            <th >
                                                <span>Issue Method</span>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
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
                        <tr>
                            <td align="left" style="width: 100%;" valign="bottom">
                                <asp:ImageButton ID="btnAdd" runat="server" AlternateText="button" 
                                    CommandName="Login" Height="19px" ImageUrl="~/skin/images/SAP_Add.png" 
                                    onclick="btnAdd_Click" OnClientClick="Dialog.showLoader();" Width="65px" />
                            </td>
                        </tr>
                    </table>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
