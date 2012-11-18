<%@ Page Title="Business Partner Master" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="BusinessPartnerMaster.aspx.cs" Inherits="SAP.BusinessPartnerMaster" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
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
                                    <asp:DropDownList ID="ddlSeries" runat="server" Width="60px" 
                                        AutoPostBack="true" onselectedindexchanged="ddlSeries_SelectedIndexChanged">
                                    </asp:DropDownList>
                                
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
                                    <span>Account Balance</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAcctBalance" runat="server" Enabled="false" Text="" Style="text-align: right"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trDelivery" runat="server">
                                <td class="detail_table_td_150" style="width: 98px;border-bottom: dotted 1px #808080;">
                                    <span>Deliveries</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDelivery" runat="server" Enabled="false" Text="" Style="text-align: right"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trOrder" runat="server">
                                <td class="detail_table_td_150" style="width: 98px;border-bottom: dotted 1px #808080;">
                                    <span>Orders</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrder" runat="server" Enabled="false" Style="text-align: right"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trOppor" runat="server">
                                <td class="detail_table_td_150" style="width: 98px;border-bottom: dotted 1px #808080;">
                                    <span>Opportunities</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOpportunity" runat="server" Enabled="false" Style="text-align: right"></asp:TextBox>
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
                            <li><a href="#tabs-2">Contact Person</a></li>
                            <li><a href="#tabs-3">Address</a></li>
                            <li><a href="#tabs-4">Remark</a></li>
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
                                        <asp:TextBox ID="txtFederalTax" runat="server"></asp:TextBox>
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
                                        <asp:TextBox ID="txtAlias" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Project</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtProject" runat="server" Enabled="False"></asp:TextBox>
                                        <asp:HyperLink ID="linkVendorsLoad" NavigateUrl="javascript:Main.openDialog('../Popup_EditProject.aspx','');"
                                        runat="server">
                                        <asp:Image ID="imgVendorsLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                        
                                    </asp:HyperLink>
                                    </td>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Industry </span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlIndustry" runat="server">
                                    </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                      <div id="tabs-2" style="overflow: auto; height: 300px; margin-top: 0px;">
                            <asp:Button ID="btnAddContact" Text="Add" runat="server"  OnClick="btnAddContact_Click" />
                            <br />
                            <asp:ListView ID="lvContactPerson" runat="server" OnItemInserted="lvContactPerson_ItemInserted"
                                OnItemCommand="lvContactPerson_ItemCommand" OnItemUpdating="lvContactPerson_ItemUpdating"
                                ViewStateMode="Enabled" OnItemEditing="lvContactPerson_ItemEditing">
                                <LayoutTemplate>
                                    <table class="data_table">
                                        <tr>
                                            <th id="thButtons" runat="server" style="width: 78px">
                                            </th>
                                            <th style="width: 25px">
                                                <span>#</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Contact ID</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>First Name</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Middle Name</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Last Name</span>
                                            </th>
                                            <th style="width: 70px">
                                                <span>Title</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Position</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Address</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Telephone 1</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Telephone 2</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Mobile Phone</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>E-Mail</span>
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="imgbEdit" runat="server" CommandName="Edit" Text="Edit" />
                                            <asp:LinkButton ID="imgbDelete" runat="server" CommandName="DeleteItem" Text="Delete"
                                                 OnClientClick="return confirm('Are you sure you want to delete this row?');"
                                                ToolTip="Delete" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblNo" Text='<%#Eval("No")%>'></asp:Label>
                                        </td>
                                        <td  style="text-align: left">
                                            <asp:Label runat="server" ID="lblName"><%#Eval("Name") %></asp:Label>
                                        </td>
                                        <td  style="text-align: left">
                                            <asp:Label runat="server" ID="lblFirstName" Style="text-align: left"><%#Eval("FirstName")%></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label runat="server" ID="lblMiddleName" Text='<%# Eval("MiddleName") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label runat="server" ID="lblLastName" Text='<%# Eval("LastName") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label runat="server" ID="lblTitle"><%#Eval("Title")%></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label runat="server" ID="lblPosition"><%#Eval("Position")%></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label runat="server" ID="lblAddress"><%#Eval("Address")%></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label runat="server" ID="lblTel1"><%#Eval("Tel1")%></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label runat="server" ID="lblTel2"><%#Eval("Tel2")%></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label runat="server" ID="lblCellolar"><%#Eval("Cellolar")%></asp:Label>
                                        </td>
                                        <td style="text-align: left" >
                                            <asp:Label runat="server" ID="lblE_MailL"><%#Eval("E_MailL")%></asp:Label>
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
                                            <asp:Label runat="server" ID="lblNo" Text='<%#Eval("No")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblName" Text='<%#Eval("Name") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblFirstName" Style="text-align: left" Text='<%#Eval("FirstName")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblMiddleName" Text='<%# Eval("MiddleName") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblLastName" Text='<%# Eval("LastName") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblTitle" Text='<%#Eval("Title")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblPosition" Text='<%#Eval("Position")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblAddress" Text='<%#Eval("Address")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblTel1" Text='<%#Eval("Tel1")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblTel2" Text='<%#Eval("Tel2")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblCellolar" Text='<%#Eval("Cellolar")%>' />
                                        </td>
                                        <td >
                                            <asp:TextBox runat="server" ID="lblE_MailL" Text='<%#Eval("E_MailL")%>' />
                                        </td>
                                    </tr>
                                </EditItemTemplate>
                                <EmptyDataTemplate>
                                    <table class="data_table">
                                        <tr>
                                           <th style="width: 25px">
                                                <span>#</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Contact ID</span>
                                            </th>
                                            <th style="width: 200px">
                                                <span>First Name</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Middle Name</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Last Name</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Title</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Position</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Address</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Telephone 1</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Telephone 2</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Mobile Phone</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>E-Mail</span>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="12">
                                                <span>No Data</span>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                            <asp:DataPager ID="ContactPager" runat="server" PagedControlID="lvContactPerson"
                                PageSize="5" OnPreRender="ContactPager_PreRender">
                                <Fields>
                                    <asp:NextPreviousPagerField FirstPageText="&lt;&lt;" ShowFirstPageButton="True" ShowNextPageButton="False"
                                        ShowPreviousPageButton="False" />
                                    <asp:NumericPagerField />
                                    <asp:NextPreviousPagerField LastPageText="&gt;&gt;" ShowLastPageButton="True" ShowNextPageButton="False"
                                        ShowPreviousPageButton="False" />
                                </Fields>
                            </asp:DataPager>
                        </div>
                        <div id="tabs-3" style="overflow: auto; height: 300px; margin-top: 0px;">
                            <asp:Button ID="btnAddAddress" Text="Add" runat="server" 
                                OnClick="btnAddAddress_Click" />
                          
                            <asp:ListView ID="lvAddress" runat="server" OnItemInserted="lvAddress_ItemInserted"
                                OnItemCommand="lvAddress_ItemCommand" OnItemUpdating="lvAddress_ItemUpdating"
                                ViewStateMode="Enabled" OnItemEditing="lvAddress_ItemEditing">
                                <LayoutTemplate>
                                    <table class="data_table">
                                        <tr>
                                            <th id="thButtons" runat="server" style="width: 45px">
                                            </th>
                                            <th style="width: 25px">
                                                <span>#</span>
                                            </th>
                                            <th style="width: 50px">
                                                <span>Type</span>
                                            </th>
                                            <th style="width: 200px">
                                                <span>Address</span>
                                            </th>
                                             <th style="width: 70px">
                                                <span>Building</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Street No.</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Street</span>
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="imgbEdit" runat="server" CommandName="Edit" Text="Edit"  />
                                            <asp:LinkButton ID="imgbDelete" runat="server" CommandName="DeleteItem" Text="Delete"
                                                 OnClientClick="return confirm('Are you sure you want to delete this row?');"
                                                ToolTip="Delete" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblNo" Text='<%#Eval("No")%>'></asp:Label>
                                        </td>
                                        <td Style="text-align: left">
                                            <asp:Label runat="server" ID="lblAdresType" Text='<%# Eval("AdresType") %>'></asp:Label>
                                        </td>
                                        <td Style="text-align: left">
                                            <asp:Label runat="server" ID="lblAddress" Text='<%# Eval("Address") %>'></asp:Label>
                                        </td>
                                        <td  Style="text-align: left">
                                            <asp:Label runat="server" ID="lblBuilding"><%#Eval("Building")%></asp:Label>
                                        </td>
                                        <td  Style="text-align: left">
                                            <asp:Label runat="server" ID="lblStreetNo"><%#Eval("StreetNo")%></asp:Label>
                                        </td>
                                        <td Style="text-align: left">
                                            <asp:Label runat="server" ID="lblStreet" Text='<%# Eval("Street") %>'></asp:Label>
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
                                            <asp:Label runat="server" ID="lblNo" Text='<%#Eval("No")%>' />
                                        </td>
                                        <td>
                                             <asp:DropDownList ID="ddlAdresType" runat="server" >
                                                <asp:ListItem Text="Bill To" Value="B"></asp:ListItem>
                                                <asp:ListItem Text="Ship To" Value="S"></asp:ListItem>
                                            </asp:DropDownList> 
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblAddress" Text='<%#Eval("Address")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblBuilding" Text='<%#Eval("Building")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblStreetNo" Text='<%# Eval("StreetNo") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblStreet" Text='<%# Eval("Street") %>' />
                                        </td>
                                        
                                    </tr>
                                </EditItemTemplate>
                                <EmptyDataTemplate>
                                    <table class="data_table">
                                        <tr>
                                           <th style="width: 25px">
                                                <span>#</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Type</span>
                                            </th>
                                            <th style="width: 200px">
                                                <span>Address</span>
                                            </th>
                                             <th style="width: 70px">
                                                <span>Building</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Street No.</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Street</span>
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
                            <asp:DataPager ID="AddressPager" runat="server" PagedControlID="lvAddress"
                                PageSize="5" OnPreRender="AddressPager_PreRender">
                                <Fields>
                                    <asp:NextPreviousPagerField FirstPageText="&lt;&lt;" ShowFirstPageButton="True" ShowNextPageButton="False"
                                        ShowPreviousPageButton="False" />
                                    <asp:NumericPagerField />
                                    <asp:NextPreviousPagerField LastPageText="&gt;&gt;" ShowLastPageButton="True" ShowNextPageButton="False"
                                        ShowPreviousPageButton="False" />
                                </Fields>
                            </asp:DataPager>
                        </div>
                         <div id="tabs-4">
                            <table class="detail_table">
                                <tr>
                                    <td>
                                        <CKEditor:CKEditorControl ID="txtRemark" runat="server" Height="135px" Width=""></CKEditor:CKEditorControl>
                                    </td>
                            </table>
                        </div>
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
                        <asp:ImageButton ID="btnActivity" runat="server" AlternateText="button" 
                             Height="19px" Width="72px" 
                            ImageUrl="~/skin/images/activity.png" onclick="btnActivity_Click" 
                            OnClientClick="Dialog.showLoader();"/>
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
