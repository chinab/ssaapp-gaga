<%@ Page Title="Sales Opportunity" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="SalesOpportunity.aspx.cs" Inherits="SAP.SalesOpportunity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Main.myUpdatePanelId = '<%= SalesOpportunityUpdatePanel.ClientID %>';
    </script>
    <asp:UpdatePanel ID="SalesOpportunityUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="contentData" style="padding-left: 15px;">
                <div id="title-form" style="border-bottom: 2px solid black;">
                    <h2>Sales Opportunity</h2>
                </div>
                <div id="header-form">
                    <div class="left">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100" style="width: 195px">
                                    <span>Business Partner Code (*)</span>
                                    <asp:HyperLink ID="lBP" NavigateUrl="~/BusinesspartnerMaster.aspx" runat="server">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/skin/icon/link.png"/>
                                    
                                    </asp:HyperLink>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCustomerCode" runat="server"></asp:TextBox>
                                    <asp:HyperLink ID="linkVendorsLoad" NavigateUrl="javascript:Main.openDialog('Popup_EditCustomer.aspx','');"
                                        runat="server">
                                        <asp:Image ID="imgVendorsLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                    
                                    </asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="width: 195px">
                                    <span>Business Partner Name</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCustomerName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="width: 195px">
                                    <span>Contact Person</span>
                                </td>
                                    <td>
                                        <asp:DropDownList ID="ddlContactPerson" runat="server" Width="124px" >
                                        </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="width: 195px; height: 23px;">
                                    <span>Total Amount Invoiced</span>
                                </td>
                                <td style="height: 23px">
                                    <asp:TextBox  Enabled="false" ID="txtRefNo" runat="server"  BackColor="#D5DCE0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="width: 195px">
                                    <span>Business Partner Territory</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTerritory" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txtterritryID" runat="server" Visible=false></asp:TextBox>
                                    <asp:HyperLink ID="HyperLink3" NavigateUrl="javascript:Main.openDialog('Popup_EditTerritory.aspx','');"
                                        runat="server">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                    
                                    </asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="width: 195px">
                                    <span>Sales Employee</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBuyer" runat="server" Width="122px">
                                        </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="width: 195px">
                                    <span>Owner</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOwner" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txtOwnerCode" runat="server" Visible=false></asp:TextBox>
                                    <asp:HyperLink ID="linkOwner" NavigateUrl="javascript:Main.openDialog('Popup_EditEmployee.aspx','');"
                                        runat="server">
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                    
                                    </asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="right">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Opportunity Name</span></td>
                                <td>
                                    <asp:TextBox ID="txtStatus" Text="" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Opportunity No.</span></td>
                                <td>
                                    <asp:TextBox  Enabled="false" ID="txtPostingDate" runat="server" 
                                        BackColor="#D5DCE0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Status</span></td>
                                <td>
                                    <asp:TextBox  Enabled="false" ID="txtDeliveryDate" runat="server"  BackColor="#D5DCE0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Start Date (*)</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Closing Date</span>
                                </td>
                                <td>
                                    <asp:TextBox  Enabled="false" ID="txtDocumentDate0" runat="server"  BackColor="#D5DCE0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Open Activities</span>
                                </td>
                                <td>
                                    <asp:TextBox  Enabled="false" ID="txtDocumentDate1" runat="server"  BackColor="#D5DCE0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Closing %</span>
                                </td>
                                <td>
                                    <asp:TextBox  Enabled="false" ID="TextBox1" runat="server" BackColor="#D5DCE0"></asp:TextBox>
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
                            <li><a href="#tabs-1">Potential</a></li>
                            <li><a href="#tabs-2">General</a></li>
                            <li><a href="#tabs-3">Stages</a></li>
                            <li><a href="#tabs-4">Partners</a></li>
                            <li><a href="#tabs-5">Competitors</a></li>
                            <li><a href="#tabs-6">Summary</a></li>
                        </ul>
                        <div id="tabs-1"">
                            <table class="detail_table">
                                <tr>
                                    <td class="detail_table_td_150">
                                        <span>Predicted Closing In</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDocumentDate6" runat="server"></asp:TextBox>
                                        <asp:DropDownList ID="DropDownList4" runat="server" Width="124px" >
                                            <asp:ListItem Text="Months" Value="M" />
                                            <asp:ListItem Text="Weeks" Value="W" />
                                            <asp:ListItem Text="Days" Value="D" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_150">
                                        <span>Predicted Closing Date</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPredDate" runat="server" CssClass="txtDate"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_150">
                                        <span>Potential Amount</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPotentialAmt" runat="server" >0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_150">
                                        <span>Weighted Amount</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWeightedAmt" runat="server" >0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_150">
                                        <span>Gross Profit %</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDocumentDate10" runat="server">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_150">
                                        <span>Gross Profit Total</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDocumentDate11" runat="server">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_150">
                                        <span>Level of Interest</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlInterest" runat="server" Width="124px" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_150">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </div>
                        <div id="tabs-2">
                            <table class="detail_table">
                                <tr>
                                    <td class="detail_table_td_100" style="width: 149px">
                                        <span>BP Channel Code</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark0" runat="server"></asp:TextBox>
                                        <asp:HyperLink ID="HyperLink1" NavigateUrl="javascript:Main.openDialog('Popup_EditVendor.aspx','');"
                                        runat="server">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                        
                                    </asp:HyperLink>
                                    </td>
                                    <td class="detail_table_td_100" style="width: 131px">
                                        <span>BP Project</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBPProjectName" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtBPProjectCode" runat="server" Visible=false></asp:TextBox>
                                        <asp:HyperLink ID="HyperLink2" NavigateUrl="javascript:Main.openDialog('Popup_EditProject.aspx','');"
                                        runat="server">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                        
                                    </asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="width: 149px">
                                        <span>BP Channel Name</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark1" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="width: 131px">
                                        <span>Information Source</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSource" runat="server" Width="122px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="width: 149px">
                                        <span>BP Channel Contact</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark2" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="width: 131px">
                                        <span>Industry</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlIndustry" runat="server" Width="122px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="width: 149px">
                                        <span>Remarks</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark3" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="detail_table_td_100" style="width: 131px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100" style="width: 149px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="detail_table_td_100" style="width: 131px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </div>
                        <div id="tabs-3">
                            <asp:Button ID="btnAddRecord" Text="Add" runat="server" 
                                OnClick="_btnAddRecord_Click" />
                            
                            <asp:ListView ID="lvStage" runat="server"
                                OnItemInserted="lvStage_ItemInserted"
                                OnItemInserting="lvStage_ItemInserting" OnItemCommand="lvStage_ItemCommand"
                                OnItemEditing="lvStage_ItemEditing" onitemcreated="lvStage_ItemCreated" onitemupdating="lvStage_ItemUpdating" 
                                ViewStateMode="Enabled"
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
                                                <span>Start Date</span>
                                            </th>
                                            <th >
                                                <span>Closing Date</span>
                                            </th>
                                            <th >
                                                <span>Sales Employee</span>
                                            </th>
                                            <th >
                                                <span>Stage</span>
                                            </th>
                                            <th >
                                                <span>%</span>
                                            </th>
                                            <th >
                                                <span>Potential Amount</span>
                                            </th>
                                            <th>
                                                <span>Weighted Amount</span>
                                            </th>
                                            <th>
                                                <span>Document Type</span>
                                            </th>
                                            <th >
                                                <span>Show BPs Docs</span>
                                            </th>
                                            <th>
                                                <span>Doc. No.</span>
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
                                            <asp:Label ID="lblStartDate" runat="server"><%#Eval("StartDate") %></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblClosingDate" runat="server"><%#Eval("ClosingDate")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSalesEmployee" runat="server"><%#Eval("SalesEmployee")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStage" runat="server"><%#Eval("Stage")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPercent" runat="server"><%#Eval("Percent")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPotentialAmt" runat="server"><%#Eval("PotentialAmt")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblWeightedAmt" runat="server"><%#Eval("WeightedAmt")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDocType" runat="server"><%#Eval("DocType")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblShowBP" runat="server"><%#Eval("ShowBP")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDocNo" runat="server"><%#Eval("DocNo")%></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="imgbUpdate" runat="server" CommandName="Update" Text="Update"
                                                ImageUrl="~/skin/icon/save_icon_mono.gif" CausesValidation="true" ValidationGroup="vgrpSaveContact" />
                                            <asp:LinkButton ID="imgbCancel" runat="server" CommandName="Cancel" Text="Cancel"
                                                ImageUrl="~/skin/icon/undo_icon_mono.gif" CausesValidation="false" />
                                        </td>
                                        <td>
                                             <asp:Label ID="lblNoEdit" runat="server" Text='<%# Bind("No") %>'/>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtStartDateEdit" runat="server" Text='<%# Bind("StartDate") %>'/>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtClosingDateEdit" runat="server" Text='<%# Bind("ClosingDate") %>'/>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSalesEmployeeEdit" runat="server" >
                                              </asp:DropDownList>
                                        </td>
                                        <td>
                                             <asp:DropDownList ID="ddlStageEdit" runat="server" >
                                              </asp:DropDownList>      
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPercentEdit" runat="server" Text='<%# Bind("Percent") %>'/>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPotentialAmtEdit" runat="server" Text='<%# Bind("PotentialAmt") %>'/>
                                        </td>
                                        <td>
                                             <asp:TextBox ID="txtWeightedAmtEdit" runat="server" Text='<%#Bind("WeightedAmt")%>' />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDocTypeEdit" runat="server" >
                                                <asp:ListItem Text="Sales Order" Value="17"></asp:ListItem>
                                                <asp:ListItem Text="Delivery" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="AR Invoice" Value="13"></asp:ListItem>
                                              </asp:DropDownList> 
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ckShowBPEdit" runat="server" Text='<%#Bind("ShowBP")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="txtDocNoEdit" runat="server" Text='<%#Bind("ShowBP")%>' />
                                            <asp:HyperLink ID="HyperLink10" NavigateUrl='#' runat="server">
                                           <asp:Image ID="Image12" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
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
                                            <asp:TextBox ID="txtStartDateInsert" runat="server" Text='<%# Bind("StartDate") %>'  CssClass="txtDate"/>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtClosingDateInsert" runat="server" Text='<%# Bind("ClosingDate") %>'/>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSalesEmployeeInsert" runat="server" >
                                              </asp:DropDownList> 
                                        </td>
                                        <td>
                                             <asp:DropDownList ID="ddlStageInsert" runat="server" >
                                              </asp:DropDownList>      
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPercentInsert" runat="server" Text='<%# Bind("Percent") %>'/>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPotentialAmtInsert" runat="server" Text='<%# Bind("PotentialAmt") %>'/>
                                        </td>
                                        <td>
                                             <asp:TextBox ID="txtWeightedAmtInsert" runat="server" Text='<%#Bind("WeightedAmt")%>' />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDocTypeInsert" runat="server">
                                                <asp:ListItem Text="Sales Order" Value="17"></asp:ListItem>
                                                <asp:ListItem Text="Delivery" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="AR Invoice" Value="13"></asp:ListItem>
                                            </asp:DropDownList> 
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ckShowBPInsert" runat="server" Text='<%#Bind("ShowBP")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="txtDocNoInsert" runat="server" Text='<%#Bind("ShowBP")%>' />
                                            <asp:HyperLink ID="HyperLink10" NavigateUrl='#' runat="server">
                                                <asp:Image ID="Image12" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
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
                                                <span>Start Date</span>
                                            </th>
                                            <th >
                                                <span>Closing Date</span>
                                            </th>
                                            <th >
                                                <span>Sales Employee</span>
                                            </th>
                                            <th >
                                                <span>Stage</span>
                                            </th>
                                            <th >
                                                <span>%</span>
                                            </th>
                                            <th >
                                                <span>Potential Amount</span>
                                            </th>
                                            <th>
                                                <span>Weighted Amount</span>
                                            </th>
                                            <th>
                                                <span>Document Type</span>
                                            </th>
                                            <th >
                                                <span>Show BPs Docs</span>
                                            </th>
                                            <th>
                                                <span>Doc. No.</span>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="11">
                                                <span>No Data</span>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                            <asp:DataPager ID="lvDataPager1" runat="server" PagedControlID="lvStage" PageSize="5">
                                <Fields>
                                    <asp:NumericPagerField ButtonType="Link" />
                                </Fields>
                            </asp:DataPager>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                        </div>
                        <div id="tabs-4">
                            <asp:ListView ID="lvPartner" runat="server" >
                                <LayoutTemplate>
                                    <table class="data_table">
                                        <tr>
                                            <th style="width: 25px">
                                                <span>#</span>
                                            </th>
                                            <th >
                                                <span>Name</span>
                                            </th>
                                            <th >
                                                <span>Relationship</span>
                                            </th>
                                            <th >
                                                <span>Related BP</span>
                                            </th>
                                            <th >
                                                <span>Remarks</span>
                                            </th>
                                            
                                        </tr>
                                        <tr ID="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNo" runat="server"><%#Eval("No")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCode" runat="server"><%#Eval("Code") %></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescription" runat="server"><%#Eval("Description")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server"><%#Eval("Description")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server"><%#Eval("Description")%></asp:Label>
                                        </td>
                                        
                                    </tr>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNo" runat="server"><%#Eval("No")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCode" runat="server"><%#Eval("Code") %></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescription" runat="server"><%#Eval("Description")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label9" runat="server"><%#Eval("Description")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label10" runat="server"><%#Eval("Description")%></asp:Label>
                                        </td>
                                        
                                    </tr>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCode" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label17" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label18" runat="server"></asp:Label>
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
                                                <span>Name</span>
                                            </th>
                                            <th >
                                                <span>Relationship</span>
                                            </th>
                                            <th >
                                                <span>Related BP</span>
                                            </th>
                                            <th >
                                                <span>Remarks</span>
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
                        </div>
                        <div id="tabs-5">
                            <asp:ListView ID="lvCompetitor" runat="server" >
                                <LayoutTemplate>
                                    <table class="data_table">
                                        <tr>
                                            <th style="width: 25px">
                                                <span>#</span>
                                            </th>
                                            <th >
                                                <span>Name</span>
                                            </th>
                                            <th >
                                                <span>Threat Level</span>
                                            </th>
                                            <th >
                                                <span>Remarks</span>
                                            </th>
                                            <th >
                                                <span>Won</span>
                                            </th>
                                        </tr>
                                        <tr ID="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNo" runat="server"><%#Eval("No")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCode" runat="server"><%#Eval("Code") %></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescription" runat="server"><%#Eval("Description")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server"><%#Eval("Description")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server"><%#Eval("Description")%></asp:Label>
                                        </td>
                                        
                                    </tr>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNo" runat="server"><%#Eval("No")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCode" runat="server"><%#Eval("Code") %></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescription" runat="server"><%#Eval("Description")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label9" runat="server"><%#Eval("Description")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label10" runat="server"><%#Eval("Description")%></asp:Label>
                                        </td>
                                        
                                    </tr>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCode" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label17" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label18" runat="server"></asp:Label>
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
                                                <span>Name</span>
                                            </th>
                                            <th >
                                                <span>Threat Level</span>
                                            </th>
                                            <th >
                                                <span>Remarks</span>
                                            </th>
                                            <th >
                                                <span>Won</span>
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
                        </div>
                        <div id="tabs-6">
                            <td class="detail_table_td_150">
                                <span>Opportunity Status</span>
                            </td>
                            <br />
                            <asp:CheckBox ID="TextBox2" Text="Open" runat="server"></asp:CheckBox>
                            <br />
                            <asp:CheckBox ID="CheckBox1" Text="Won" runat="server"></asp:CheckBox>
                            <br />
                            <asp:CheckBox ID="CheckBox2" Text="Lost" runat="server"></asp:CheckBox>
                            <br />
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
                    <div class="right">
                        <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="button" 
                                CommandName="Login" Height="19px" Width="122px" ImageUrl="~/skin/images/relatedactivity.png" />
                        <asp:ImageButton ID="ImageButton2" runat="server" AlternateText="button" 
                                CommandName="Login" Height="19px" Width="122px" ImageUrl="~/skin/images/relateddocument.png" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
