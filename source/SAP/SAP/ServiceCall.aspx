<%@ Page Title="AR Invoice" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ServiceCall.aspx.cs" Inherits="SAP.ServiceCall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Main.myUpdatePanelId = '<%= purchaseOrderUpdatePanel.ClientID %>';
    </script>
    <asp:UpdatePanel ID="purchaseOrderUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="contentData" style="padding-left: 15px;">
                <div id="title-form" style="border-bottom: 2px solid black;">
                    <h2>Service Call</h2>
                    <asp:Label BackColor="Red" ID="lblResults" runat="server" />
                </div>
                <div id="header-form">
                    <div class="left">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Customer Code</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVendor" runat="server" Width="186px" Enabled="false" BackColor="#D5DCE0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Customer Name</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName0" runat="server" Width="189px" Enabled="false" BackColor="#D5DCE0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Contact Person</span>
                                </td>
                                <td>
                                     <asp:TextBox ID="TextBox1" runat="server" Width="189px" Enabled="false" BackColor="#D5DCE0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100">
                                    <span>Telephone No.</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStatus0" runat="server" Text=""></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="right">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_150" style="width: 98px">
                                    <span>Call Status</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStatus" runat="server" Enabled="false" Text="" BackColor="#D5DCE0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="width: 98px">
                                    <span>Created On</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCreatedDate" runat="server" Enabled="false" Text="" BackColor="#D5DCE0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="width: 98px">
                                    <span>Closed On</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtClosedDate" runat="server" Enabled="false" BackColor="#D5DCE0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150" style="width: 98px">
                                    <span>Contract No.</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContractNo" runat="server" Enabled="false" BackColor="#D5DCE0"></asp:TextBox>
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
                        </ul>
                        <div id="tabs-1">
                            <table class="detail_table">
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Subject</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark0" runat="server" Width="655px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="detail_table_td_100">
                                        <span>Remarks</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJournalRemark1" runat="server" Height="80px" 
                                            TextMode="MultiLine" Width="652px"></asp:TextBox>
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
                            CommandName="Login" Height="19px" Width="65px" 
                            ImageUrl="~/skin/images/SAP_Add.png" onclick="imgAdd_Click" />
                    
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
