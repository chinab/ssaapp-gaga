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
                                        <CKEditor:CKEditorControl ID="txtRemark" runat="server" Height="135px" Width="">
                                        </CKEditor:CKEditorControl>
                                        <%--<asp:TextBox ID="txtRemark" runat="server" Height="200px" 
                                            TextMode="MultiLine" Width="652px"></asp:TextBox>--%>
                                    </td>
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
