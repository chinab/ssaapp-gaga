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
                                <td class="detail_table_td_100" style="width: 132px">
                                    <span>Business Partner Code</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVendor" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="width: 132px">
                                    <span>Business Partner Name</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="width: 132px">
                                    <span>Contact Person</span>
                                </td>
                                    <td>
                                        <asp:DropDownList ID="ddlContactPerson" runat="server" Width="122px">
                                        </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="width: 132px; height: 23px;">
                                    <span>Total Amount Invoiced</span>
                                </td>
                                <td style="height: 23px">
                                    <asp:TextBox ID="txtRefNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Business Partner Territory</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDocumentDate3" runat="server" CssClass="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Sales Employee</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDocumentDate4" runat="server" CssClass="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Owner</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDocumentDate5" runat="server" CssClass="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="right">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_150" dir="rtl">
                                    <span>Opportunity Name</span></td>
                                <td dir="rtl">
                                    <asp:TextBox Enabled="false" ID="txtStatus" Text="" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Opportunity No.</span></td>
                                <td>
                                    <asp:TextBox ID="txtPostingDate" runat="server" CssClass="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Status</span></td>
                                <td>
                                    <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Start Date</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDocumentDate" runat="server" CssClass="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Closing Date</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDocumentDate0" runat="server" CssClass="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Open Activities</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDocumentDate1" runat="server" CssClass="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_150">
                                    <span>Closing %</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDocumentDate2" runat="server" CssClass="txtDate"></asp:TextBox>
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
                        </div>
                        <div id="tabs-2">
                        </div>
                        <div id="tabs-3">
                        </div>
                        <div id="tabs-4">
                        </div>
                        <div id="tabs-5">
                        </div>
                        <div id="tabs-6">
                        </div>
                    </div>
                    <br />
                 
                </div>
                <div id="footer-form">
                    <div class="left">
                        <asp:Button ID="btnAdd" Text="Add" runat="server" />
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" />
                    </div>
                    <div class="right">
                        <asp:Button ID="btnCopyFrom" Text="Copy From" runat="server" />
                        <asp:Button ID="btnCopyTo" Text="Copy To" runat="server" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
