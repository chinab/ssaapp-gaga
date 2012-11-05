<%@ Page Title="Activity" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Activity.aspx.cs" Inherits="SAP.Activity" %>
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
                            <asp:HyperLink ID="linkNew" runat="server"> <asp:Image ID="Image2" runat="server" ImageUrl="~/skin/icon/addnew.png" /></asp:HyperLink>
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
                <div id="title-form" style="border-bottom: 2px solid black;">
                    <h2>Activity</h2>
                    <asp:Label BackColor="Red" ID="lblResults" runat="server" />
                </div>
                <div id="header-form">
                    <div class="left">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Activity</span>
                                    
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlActivity" runat="server" Width="200px">
                                        <asp:ListItem Text="Phone Call" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="Meeting" Value="M"></asp:ListItem>
                                        <asp:ListItem Text="Task" Value="T"></asp:ListItem>
                                        <asp:ListItem Text="Note" Value="E"></asp:ListItem>
                                        <asp:ListItem Text="Campaign" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Other" Value="N"></asp:ListItem>
                                    </asp:DropDownList>
                                     
                                </td>
                               
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Type</span>
                                </td>
                                <td>
                                     <asp:DropDownList ID="ddlType" runat="server" Width="200px" AutoPostBack="true"
                                         onselectedindexchanged="ddlType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Subject</span>
                                </td>
                                <td>
                                     <asp:DropDownList ID="ddlSubject" runat="server" Width="200px"  
                                         AutoPostBack="true" onselectedindexchanged="ddlSubject_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Assigned To</span></td>
                                <td>
                                    <asp:DropDownList ID="ddlAssignTo" runat="server" Width="70px" 
                                        onselectedindexchanged="ddlAssignTo_SelectedIndexChanged" AutoPostBack="true"> 
                                        <asp:ListItem Text="User" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="Employee" Value="M"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlUser" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="right">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_150" style="border-bottom: dotted 1px #808080;">
                                    <span>No.</span></td>
                                <td>
                                    <asp:TextBox ID="txtNo" runat="server" Enabled="false" Text="" 
                                        BackColor="#D5DCE0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="border-bottom: dotted 1px #808080;">
                                    <span>BP Code</span>
                                    <asp:HyperLink ID="lBP" runat="server">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/skin/icon/link.png"/>
                                    </asp:HyperLink>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBP" runat="server" Enabled="false" Text="" 
                                        BackColor="#D5DCE0"></asp:TextBox>
                                    <asp:HyperLink ID="linkVendorsLoad" NavigateUrl="javascript:Main.openDialog('../Popup_EditCustomer.aspx','');"
                                        runat="server"> <asp:Image ID="imgVendorsLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="border-bottom: dotted 1px #808080;">
                                    <span>BP Name</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBPName" runat="server" Enabled="false" BackColor="#D5DCE0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="border-bottom: dotted 1px #808080;">
                                    <span>Contact Person</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlContactPerson" runat="server" Width="120px">
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
                            <li><a href="#tabs-1">General</a></li>
                            <li><a href="#tabs-2">Content</a></li>
                            <li><a href="#tabs-3">Linked Doc.</a></li>
                        </ul>
                        <div id="tabs-1">
                        <table >
                        <tr>
                            <td class="detail_table_td_100" 
                                style="border-bottom: dotted 1px #808080; font-weight: 700;">
                                <span>From Time</span></td>
                            <td>
                                <asp:TextBox ID="txtFromDate" runat="server" Text="" CssClass="txtDate" 
                                    Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFromTime" runat="server" Text="" Width="40px"></asp:TextBox>
                               
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="detail_table_td_100" 
                                style="border-bottom: dotted 1px #808080; font-weight: 700;">
                                <span>To Time</span></td>
                             <td>
                                <asp:TextBox ID="txtToDate" runat="server" Text="" CssClass="txtDate" 
                                     Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtToTime" runat="server" Text="" Width="40px"></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="detail_table_td_100" 
                                style="border-bottom: dotted 1px #808080; font-weight: 700;">
                                    <span>Priority</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" 
                                        Width="100px">
                                        <asp:ListItem Text="Low" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Normal" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="High" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                 </td>
                        </tr>
                    </table>
                            <div style="height: 28px"></div>
                            <table class="detail_table">
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Subject (*)</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSubject" runat="server" Width="650px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div style="height: 47px"></div>
                            <asp:CheckBox ID="cbClosed" Text="Closed" runat="server" Font-Bold="True" />
                        </div>
                        <div id="tabs-2">
                            <table class="detail_table">
                                <tr>
                                    <td>
                                        <CKEditor:CKEditorControl ID="txtRemark" runat="server" Height="135px" Width=""></CKEditor:CKEditorControl>
                                        <%--<asp:TextBox ID="txtRemark" runat="server" Height="200px" 
                                            TextMode="MultiLine" Width="652px"></asp:TextBox>--%>
                                    </td>
                            </table>
                        </div>
                        <div id="tabs-3">
                            <table class="detail_table">
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Document Type</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList2" runat="server">
                                            <asp:ListItem Text="A/R Invoice" Value="13"></asp:ListItem>
                                            <asp:ListItem Text="A/R Credit Memo" Value="14"></asp:ListItem>
                                            <asp:ListItem Text="Delivery" Value="15"></asp:ListItem>
                                            <asp:ListItem Text="Returns" Value="16"></asp:ListItem>
                                            <asp:ListItem Text="Sales Order" Value="17"></asp:ListItem>
                                            <asp:ListItem Text="A/P Invoice" Value="18"></asp:ListItem>
                                            <asp:ListItem Text="A/P Credit Memo" Value="19"></asp:ListItem>
                                            <asp:ListItem Text="Goods Receipt" Value="20"></asp:ListItem>
                                            <asp:ListItem Text="Goods Return" Value="21"></asp:ListItem>
                                            <asp:ListItem Text="Purchase Order" Value="22"></asp:ListItem>
                                            <asp:ListItem Text="Purchase Quotation" Value="23"></asp:ListItem>
                                            <asp:ListItem Text="Goods Receipt PO" Value="24"></asp:ListItem>
                                            <asp:ListItem Text="Journal Entry" Value="30"></asp:ListItem>
                                            <asp:ListItem Text="Outgoing Payment" Value="46"></asp:ListItem>
                                            <asp:ListItem Text="Goods Receipt" Value="59"></asp:ListItem>
                                            <asp:ListItem Text="Goods Issue" Value="60"></asp:ListItem>
                                            <asp:ListItem Text="Inventory Transfer Request" Value="1250000001"></asp:ListItem>
                                            <asp:ListItem Text="Inventory Transfer" Value="67"></asp:ListItem>
                                            <asp:ListItem Text="Inventory Revaluation" Value="162"></asp:ListItem>
                                            <asp:ListItem Text="Production Order" Value="202"></asp:ListItem>
                                            <asp:ListItem Text="Items" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Document No.</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" Enabled="False"></asp:TextBox>
                                        <asp:HyperLink ID="HyperLink1" NavigateUrl="javascript:Main.openDialog('../Popup_EditCustomer.aspx','');"
                                        runat="server"> <asp:Image ID="Image1" runat="server" ImageUrl="~/skin/images/item-pointer.gif" /></asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    
                    <br />
                </div>
                <div class="clear">
                </div>
                <div id="footer-form">
                    <div class="left">
                        <asp:ImageButton ID="imgAdd" runat="server" AlternateText="button" 
                             Height="19px" Width="65px" 
                            ImageUrl="~/skin/images/SAP_Add.png" onclick="imgAdd_Click" OnClientClick="Dialog.showLoader();"/>
                    <asp:ImageButton ID="imgUpdate" runat="server" AlternateText="button" 
                             Height="19px" Width="65px" 
                            ImageUrl="~/skin/images/SAP_update.png" onclick="imgAdd_Click" OnClientClick="Dialog.showLoader();"/>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
