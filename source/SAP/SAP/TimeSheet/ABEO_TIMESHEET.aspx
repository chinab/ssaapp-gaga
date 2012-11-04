<%@ Page Title="ABEO_TIMESHEET" Language="C#" MasterPageFile="~/Main.Master" validateRequest="false" AutoEventWireup="true"
    CodeBehind="ABEO_TIMESHEET.aspx.cs" Inherits="SAP.ABEO_TIMESHEET" %>
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
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/skin/icon/addnew.png" />
                            </asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="linkFirst" runat="server">
                                <asp:Image ID="Image4" runat="server" ImageUrl="~/skin/icon/first.png" />
                            </asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="linkPrevious" runat="server">
                                <asp:Image ID="Image5" runat="server" ImageUrl="~/skin/icon/previous.png" />
                            </asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="linkNext" runat="server">
                                <asp:Image ID="Image6" runat="server" ImageUrl="~/skin/icon/next.png" />
                            </asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="linkLast" runat="server">
                                <asp:Image ID="Image7" runat="server" ImageUrl="~/skin/icon/last.png" />
                            </asp:HyperLink>
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
                                    <asp:HyperLink ID="linkVendorsLoad" NavigateUrl="javascript:Main.openDialog('../Popup_EditCustomer.aspx','');"
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
                            <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                <span>From Time</span></td>
                            <td>
                                <asp:TextBox ID="txtFromTime" runat="server" Text="" Width="50px"></asp:TextBox>
                               
                            </td>
                            <td class="detail_table_td_100" style="border-bottom: dotted 1px #808080;">
                                <span>To Time</span></td>
                            <td>
                                <asp:TextBox ID="txtToTime" runat="server" Text="" Width="50px"></asp:TextBox>
                            </td>
                        </tr>

                    </table>
                </div>
                <div id="content-form">
                    <div id="tabs">
                        <ul>
                            <li><a href="#tabs-1">General</a></li>
                            <li><a href="#tabs-2">Attachments</a></li>
                        </ul>
                        <div id="tabs-1">
                            <table class="detail_table">
                                <tr>
  
                                    <td>
                                        <asp:TextBox ID="txtSubject" runat="server" Width="650px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                   
                                    <td>
                                         <CKEditor:CKEditorControl ID="txtRemark" runat="server" Height="135px" 
                                             Width=""></CKEditor:CKEditorControl>

                                        <%--<asp:TextBox ID="txtRemark" runat="server" Height="200px" 
                                            TextMode="MultiLine" Width="652px"></asp:TextBox>--%>
                                    </td>
                                </tr>
                                <tr>
                
                                    <td>
                                        <asp:CheckBox ID="cbClosed" Text="Closed" runat="server" Font-Bold="True" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="tabs-2">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            <asp:Button ID="SubmitButton" runat="server" Text="Submit File" OnClick="SubmitButton_Click" />
                            
                            <br />
                           <asp:ListView ID="lvAttachments" runat="server" 
                                onitemcommand="lvAttachments_ItemCommand" >
                                <LayoutTemplate>
                                    <table class="data_table">
                                        <tr>
                                        <th id="thButtons" runat="server" style="width: 70px">
                                            </th>
                                            <th style="width: 25px">
                                                <span>#</span>
                                            </th>
                                            <th >
                                                <span>Path</span>
                                            </th>
                                            <th >
                                                <span>File Name</span>
                                            </th>
                                            <th >
                                                <span>Attachment Date</span>
                                            </th>
                                            
                                        </tr>
                                        <tr ID="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                    <td>
                                            <asp:LinkButton ID="imgbDelete" runat="server" CommandName="DeleteItem" Text="Delete"
                                                ImageUrl="~/skin/icon/delete_icon_mono.gif" OnClientClick="return confirm('Are you sure you want to delete this row?');"
                                                ToolTip="Delete" />
                                        </td>
                                        <td>
                                            <asp:Label ID="No" runat="server"><%#Eval("No")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="trgtPath" runat="server"><%#Eval("trgtPath")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="FileName" runat="server"><%#Eval("FileName")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Date" runat="server"><%#Eval("Date")%></asp:Label>
                                        </td>                         
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table class="data_table">
                                        <tr>
                                            <th style="width: 25px">
                                                <span>#</span>
                                            </th>
                                            <th >
                                                <span>Path</span>
                                            </th>
                                            <th >
                                                <span>File Name</span>
                                            </th>
                                            <th >
                                                <span>Attachment Date</span>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <span>No Data</span>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                    
                    <br />
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
        <Triggers>
            <asp:PostBackTrigger ControlID="SubmitButton"  runat="server" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
