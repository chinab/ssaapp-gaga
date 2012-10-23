<%@ Page Title="ABEO_TIMESHEET" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ABEO_TIMESHEET.aspx.cs" Inherits="SAP.ABEO_TIMESHEET" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Main.myUpdatePanelId = '<%= purchaseOrderUpdatePanel.ClientID %>';
    </script>
    <asp:UpdatePanel ID="purchaseOrderUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="contentData" style="padding-left: 15px;">
             <div id="menu-action" style="margin-left: -15px;">
                    <ul>
                        <li>
                            <asp:ImageButton ID="ImageButton2" runat="server" AlternateText="button" 
                                    CommandName="Login" ImageUrl="~/skin/icon/preview.png" 
                                    onclick="btnAddNew_Click" Height="22px" />
                        </li>
                        <li>
                            <asp:ImageButton ID="ImageButton3" runat="server" AlternateText="button" 
                                    CommandName="Login" ImageUrl="~/skin/icon/print.png" 
                                    onclick="btnAddNew_Click" Height="22px" />
                        </li>
                        <li>
                            <asp:ImageButton ID="ImageButton4" runat="server" AlternateText="button" 
                                    CommandName="Login" ImageUrl="~/skin/icon/email.png" 
                                    onclick="btnAddNew_Click" Height="22px" />
                        </li>
                        <li>
                            <asp:ImageButton ID="ImageButton5" runat="server" AlternateText="button" 
                                    CommandName="Login" ImageUrl="~/skin/icon/excel.png" 
                                    onclick="btnAddNew_Click" Height="22px" />
                        </li>
                        <li>
                            <asp:ImageButton ID="ImageButton6" runat="server" AlternateText="button" 
                                    CommandName="Login" ImageUrl="~/skin/icon/pdf.png" 
                                    onclick="btnAddNew_Click" Height="22px" />
                        </li>
                        <li>
                             <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="button" 
                                    CommandName="Login" ImageUrl="~/skin/icon/word.png" 
                                    onclick="btnAddNew_Click" Height="22px" />
                        <li>
                            <asp:ImageButton ID="btnAddNew" runat="server" AlternateText="button" 
                                    CommandName="Login" ImageUrl="~/skin/icon/addnew.png" 
                                    onclick="btnAddNew_Click" Height="22px"/>
                        </li>
                        <li>
                            <asp:ImageButton ID="ImageButton7" runat="server" AlternateText="button" 
                                    CommandName="Login" ImageUrl="~/skin/icon/first.png" 
                                    onclick="btnAddNew_Click" Height="22px"/>
                        </li>
                        <li>
                            <asp:ImageButton ID="ImageButton8" runat="server" AlternateText="button" 
                                    CommandName="Login" ImageUrl="~/skin/icon/previous.png" 
                                    onclick="btnAddNew_Click" Height="22px" />
                        </li>
                        <li>
                            <asp:ImageButton ID="ImageButton9" runat="server" AlternateText="button" 
                                    CommandName="Login" ImageUrl="~/skin/icon/next.png" 
                                    onclick="btnAddNew_Click" Height="22px" />
                        </li>
                        <li>
                            <asp:ImageButton ID="ImageButton10" runat="server" AlternateText="button" 
                                    CommandName="Login" ImageUrl="~/skin/icon/last.png" 
                                    onclick="btnAddNew_Click" Height="22px"/>
                        </li>
                    </ul>                    
                </div>
                <div id="title-form" style="border-bottom: 2px solid black;">
                    <h2>ABEO TIMESHEET</h2>
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
                                    <asp:DropDownList ID="ddlActivity" runat="server" Width="200px" Enabled="False">
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
                                     <asp:DropDownList ID="ddlType" runat="server" Width="200px" Enabled="False" 
                                         onselectedindexchanged="ddlType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Subject (*)</span>
                                </td>
                                <td>
                                     <asp:DropDownList ID="ddlSubject" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                    <span>Assigned To</span></td>
                                <td>
                                    <asp:DropDownList ID="ddlAssignTo" runat="server" Width="70px" Enabled="False">
                                        <asp:ListItem Text="User" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="Employee" Value="M"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextBox1" runat="server" Enabled="false" Text="" 
                                        BackColor="#D5DCE0" Width="120px"></asp:TextBox>
                                    <asp:HyperLink ID="HyperLink1" NavigateUrl="#"
                                        runat="server">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
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
                                    <span>BP Code (*)</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBP" runat="server" Enabled="false" Text="" 
                                        BackColor="#D5DCE0"></asp:TextBox>
                                    <asp:HyperLink ID="linkVendorsLoad" NavigateUrl="javascript:Main.openDialog('Popup_EditCustomer.aspx','');"
                                        runat="server">
                                        <asp:Image ID="imgVendorsLoad" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                    </asp:HyperLink>
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
                                    <asp:DropDownList ID="ddlContactPerson" runat="server" Width="120px" 
                                        Enabled="False">
                                    </asp:DropDownList>
                                    
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="clear">
                    <table class="detail_table">
                        <tr>
                            <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                <span>Date</span></td>
                            <td>
                                <asp:TextBox ID="txtDate" runat="server" Text="" CssClass="txtDate"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                <span>From Time</span></td>
                            <td>
                                <asp:TextBox ID="txtFromTime" runat="server" Text=""></asp:TextBox>
                               
                            </td>
                        </tr>
                        <tr>
                            <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                <span>To Time</span></td>
                            <td>
                                <asp:TextBox ID="txtToTime" runat="server" Text=""></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="content-form">
                    <div id="tabs">
                        <ul>
                            <li><a href="#tabs-1">General</a></li>
                        </ul>
                        <div id="tabs-1">
                            <table class="detail_table">
                                <tr>
                                    <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                        <span>Summary</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSubject" runat="server" Width="650px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Details (*)</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRemark" runat="server" Height="200px" 
                                            TextMode="MultiLine" Width="652px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        &nbsp;</td>
                                    <td>
                                        <asp:CheckBox ID="cbClosed" Text="Closed" runat="server" Font-Bold="True" /></td>
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
                            ImageUrl="~/skin/images/SAP_Add.png" onclick="imgAdd_Click" />
                    
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
