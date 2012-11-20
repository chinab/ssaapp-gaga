﻿<%@ Page Title="Production Order" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ProductionOrder.aspx.cs" Inherits="SAP.ProductionOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Main.myUpdatePanelId = '<%= SalesOpportunityUpdatePanel.ClientID %>';
    </script>
    <asp:UpdatePanel ID="SalesOpportunityUpdatePanel" runat="server">
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
                            <asp:HyperLink ID="linkNew" runat="server"> <asp:Image ID="Image1" runat="server" ImageUrl="~/skin/icon/addnew.png" /></asp:HyperLink>
                        </li>

                        <li>
                            <asp:HyperLink ID="linkFirst" runat="server"> <asp:Image ID="Image4" runat="server" ImageUrl="~/skin/icon/first.png" /></asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="linkPrevious" runat="server"> <asp:Image ID="Image2" runat="server" ImageUrl="~/skin/icon/previous.png" /></asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="linkNext" runat="server"> <asp:Image ID="Image3" runat="server" ImageUrl="~/skin/icon/next.png" /></asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="linkLast" runat="server"> <asp:Image ID="Image7" runat="server" ImageUrl="~/skin/icon/last.png" /></asp:HyperLink>
                        </li>
                 </ul>                    
                </div>
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
                                        Width="120px" AutoPostBack="true"
                                        onselectedindexchanged="ddlType_SelectedIndexChanged">
                                         <asp:ListItem Text="Standard" Value="S"></asp:ListItem>
                                        <asp:ListItem Text="Special" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Disassembly" Value="D"></asp:ListItem>
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
                                        <asp:ListItem Text="Planned" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Released" Value="R"></asp:ListItem>
                                        <asp:ListItem Text="Closed" Value="L"></asp:ListItem>
                                        <asp:ListItem Text="Cancelled" Value="C"></asp:ListItem>
                                       
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
                                                runat="server"> <asp:Image ID="imgItems" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
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
                                    <asp:TextBox ID="txtQuantity" runat="server" 
                                        ontextchanged="txtQuantity_TextChanged" AutoPostBack=true></asp:TextBox>
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
                                        NavigateUrl="javascript:Main.openDialog('../Popup_EditWareHouse.aspx?id=-1','');"> <asp:Image ID="Image9" runat="server" 
                                        ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                </td>
                            </tr>
                            
                        </table>
                    </div>
                    <div class="right">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>No.</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNo" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Order Date</span>
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
                                    <span>Customer
                                    </span>
                                    <asp:HyperLink ID="lBP" runat="server"> <asp:Image 
                                        ID="Image14" runat="server" ImageUrl="~/skin/icon/link.png"/></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCardCode" runat="server" Enabled="False" ></asp:TextBox>
                                    <asp:HyperLink ID="HyperLink3" runat="server" 
                                        NavigateUrl="javascript:Main.openDialog('../Popup_EditBusinessPartner.aspx?CardType=C','');"> <asp:Image ID="Image6" runat="server" 
                                        ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Project</span></td>
                                <td>
                                    <asp:TextBox ID="txtProject" runat="server" Enabled="False"></asp:TextBox>
                                    <asp:HyperLink ID="HyperLink5" runat="server" 
                                        NavigateUrl="javascript:Main.openDialog('../Popup_EditProject.aspx','');"> <asp:Image ID="Image8" runat="server" 
                                        ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
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
                            <li><a href="#tabs-2">Summary</a></li>
                        </ul>
                        <div id="tabs-1">
                            <asp:Button ID="btnAddRecord" Text="Add" runat="server" 
                                OnClick="_btnAddRecord_Click" />
                            
                            <asp:ListView ID="lvContents" runat="server" OnItemCommand="lvContents_ItemCommand"
                                OnItemEditing="lvContents_ItemEditing" onitemupdating="lvContents_ItemUpdating" 
                                ViewStateMode="Enabled" >
                                <LayoutTemplate>
                                    <table class="data_table">
                                        <tr>
                                            <th id="thButtons" runat="server" style="width: 78px">
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
                                                <span>Base Qty</span>
                                            </th>
                                            <th >
                                                <span>Planned Qty</span>
                                            </th>
                                            <th >
                                                <span>Issued Qty</span>
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
                                            <asp:LinkButton ID="imgbEdit" runat="server" CommandName="Edit" Text="Edit"  />
                                            <asp:LinkButton ID="imgbDelete" runat="server" CommandName="Delete" Text="Delete"
                                                 OnClientClick="return confirm('Are you sure you want to delete this row?');"
                                                ToolTip="Delete" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNo" runat="server"><%#Eval("No") %></asp:Label>
                                        </td>
                                        
                                        <td style="text-align: left">
                                            <asp:Label ID="lblItemCode" runat="server"><%#Eval("ItemCode") %></asp:Label>
                                        </td>
                                        
                                        <td style="text-align: left">
                                            <asp:Label ID="lblItemName" runat="server"><%#Eval("Dscription")%></asp:Label>
                                        </td>
                                        
                                        <td style="text-align: right">
                                            <asp:Label ID="lblQuantity" runat="server"><%#Eval("BaseQty")%></asp:Label>
                                        
                                        </td>

                                        <td style="text-align: right">
                                            <asp:Label ID="Label1" runat="server"><%#Eval("PlannedQty")%></asp:Label>
                                        
                                        </td>

                                         <td style="text-align: right">
                                            <asp:Label ID="Label2" runat="server"><%#Eval("IssuedQty")%></asp:Label>
                                        
                                        </td>
                                        
                                        <td style="text-align: left">
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
                                                CausesValidation="true" ValidationGroup="vgrpSaveContact" />
                                            <asp:LinkButton ID="imgbCancel" runat="server" CommandName="CancelUpdate" Text="Cancel"
                                                CausesValidation="false" />
                                        </td>
                                        <td>
                                             <asp:Label ID="lblNoEdit" runat="server" Text='<%# Bind("No") %>'/>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>'/>
                                            <asp:HyperLink ID="linkItems" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"../Popup_EditItem.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server"> <asp:Image ID="imgItems" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("Dscription") %>'/>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("BaseQty") %>'/>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQuantityEdit" runat="server" Text='<%# Bind("PlannedQty") %>'/>
                                        </td>
                                       <td>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("IssuedQty") %>'/>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblWarehouse" runat="server" Text='<%# Bind("wareHouse") %>'/>
                                            <asp:HyperLink ID="linkWarehouseLoad" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"../Popup_EditWareHouse.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server"> <asp:Image ID="imgWarehouseLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                        </td>

                                        <td>
                                            <asp:DropDownList ID="ddlIssueType" runat="server" SelectedValue=<%# Bind("IssueType") %>>
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
                                                <span>Base Qty</span>
                                            </th>
                                            <th >
                                                <span>Planned Qty</span>
                                            </th>
                                            <th >
                                                <span>Issued Qty</span>
                                            </th>
                                            <th >
                                                <span>Warehouse</span>
                                            </th>
                                            <th >
                                                <span>Issue Method</span>
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
                               PagedControlID="lvContents" PageSize="5" 
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
                        <div id="tabs-2">
                            <table  class="detail_table">
                                <tr>
                                    <td class="detail_table_td_100" style="width: 127px">
                                        <span>Cost</span>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="detail_table_td_100" style="width: 121px">
                                        <span>Quantities</span>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080; width: 127px;" 
                                        rowspan="1">
                                        <span>Actual Component Cost</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtComCost" runat="server" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" 
                                        style="border-bottom: dotted 1px #808080; width: 121px;">
                                        <span>Planned Quantity</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPlQty" runat="server" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080; width: 127px;" 
                                        rowspan="1">
                                        <span>Actual Additional Cost</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddCost" runat="server" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" 
                                         style="border-bottom: dotted 1px #808080; width: 121px;">
                                        <span>Completed Quantity</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCplQty" runat="server" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080; width: 127px;" 
                                        rowspan="1">
                                        <span>Actual Product Cost</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtProCost" runat="server" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" 
                                         style="border-bottom: dotted 1px #808080; width: 121px;">
                                        <span>Rejected Quantity</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRjtQty" runat="server" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080; width: 127px;" 
                                        rowspan="1">
                                        <span>Journal Remark</span>
                                        <asp:HyperLink ID="hlJE" runat="server"> <asp:Image ID="Image15" runat="server" ImageUrl="~/skin/icon/link.png"/></asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>              
                    </div>
                    <br />
                 
                </div>
                <div>
                    <table  class="detail_table">
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
                <div id="footer-form">
                    <table>
                        <tr>
                            <td align="left" style="width: 100%;" valign="bottom">
                                <asp:ImageButton ID="btnAdd" runat="server" AlternateText="button" 
                                    CommandName="Login" Height="19px" ImageUrl="~/skin/images/SAP_Add.png" 
                                    onclick="btnAdd_Click" OnClientClick="Dialog.showLoader();" Width="65px" />
                                <asp:ImageButton ID="btnUpdate" runat="server" AlternateText="button" 
                                    CommandName="Login" Height="19px" ImageUrl="~/skin/images/SAP_Update.png" 
                                    onclick="btnUpdate_Click" OnClientClick="Dialog.showLoader();" 
                                    Width="65px" />
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
