<%@ Page Title="All TimeSheet" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="AllTimeSheet.aspx.cs" Inherits="SAP.AllTimeSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Main.myUpdatePanelId = '<%= SalesOpportunityUpdatePanel.ClientID %>';
    </script>
    <asp:UpdatePanel ID="SalesOpportunityUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="contentData" style="padding-left: 15px;">
                <div id="title-form" style="border-bottom: 2px solid black;">
                    <h2>All TimeSheet</h2>
                </div>
                <div id="header-form">
                    <div style="width: 706px">
                        <table class="detail_table">
                            <tr>
                                <td class="detail_table_td_100" style="width: 195px">
                                    <span>From Date</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFromDate" runat="server" class="txtDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail_table_td_100" style="width: 195px">
                                    <span>To Date</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToDate" runat="server"  class="txtDate" ></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="detail_table_td_100" style="width: 195px">
                                    <asp:CheckBox ID="ckViewAll" runat="server" Text="View All" />
                                </td>
                                <td>
                                    <asp:Button ID="btnView" runat="server" 
                                        Text="View" onclick="btnView_Click" Width="65px" />
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
                            <li><a href="#tabs-1">Open List</a></li>
                            <li><a href="#tabs-2">All</a></li>
                        </ul>
                        <div id="tabs-1" style="overflow: auto; height: 400px; margin-top: 0px;">                                                        
                            <asp:ListView ID="lvStage" runat="server" onitemcommand="lvStage_ItemCommand" >
                                <LayoutTemplate>
                                    <table class="data_table">
                                        <tr>
                                            <th id="thButtons" runat="server" style="width: 70px">
                                            </th>
                                            <th style="width: 25px">
                                                <span>#</span>
                                            </th>
                                            <th >
                                                <span>Details</span>
                                            </th>
                                            <th >
                                                <span>Notes</span>
                                            </th>
                                            <th >
                                                <span>Recontact</span>
                                            </th>
                                            <th >
                                                <span>User</span>
                                            </th>
                                        </tr>
                                        <tr ID="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                         <td>
                                            <asp:LinkButton ID="imgbEdit" runat="server" CommandName="View" Text="View" CommandArgument='<%#Eval("ClgCode")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="ClgCode" runat="server"><%#Eval("ClgCode")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Details" runat="server"><%#Eval("Details")%></asp:Label>
                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="Notes" runat="server"><%#Eval("Notes")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Recontact" runat="server"><%#Eval("Recontact")%></asp:Label>
                                        </td>
                                         <td>
                                            <asp:Label ID="U_UserID" runat="server"><%#Eval("U_UserID")%></asp:Label>
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
                                                <span>Details</span>
                                            </th>
                                            <th >
                                                <span>Notes</span>
                                            </th>
                                            <th >
                                                <span>Recontact</span>
                                            </th>
                                            <th >
                                                <span>User</span>
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
                            <asp:DataPager ID="ProductListPagerCombo" runat="server" 
                               PagedControlID="lvStage" PageSize="15" 
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
                        <div id="tabs-2" style="overflow: auto; height: 400px; margin-top: 0px;">                                                        
                            <asp:ListView ID="lvAll" runat="server" onitemcommand="lvAll_ItemCommand" >
                                <LayoutTemplate>
                                    <table class="data_table">
                                        <tr>
                                            <th id="thButtons" runat="server" style="width: 70px">
                                            </th>
                                            <th style="width: 25px">
                                                <span>#</span>
                                            </th>
                                            <th >
                                                <span>Details</span>
                                            </th>
                                            <th >
                                                <span>Notes</span>
                                            </th>
                                            <th >
                                                <span>Recontact</span>
                                            </th>
                                            <th >
                                                <span>User</span>
                                            </th>
                                        </tr>
                                        <tr ID="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                         <td>
                                            <asp:LinkButton ID="imgbEdit" runat="server" CommandName="View" Text="View" CommandArgument='<%#Eval("ClgCode")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="ClgCode" runat="server"><%#Eval("ClgCode")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Details" runat="server"><%#Eval("Details")%></asp:Label>
                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="Notes" runat="server"><%#Eval("Notes")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Recontact" runat="server"><%#Eval("Recontact")%></asp:Label>
                                        </td>
                                         <td>
                                            <asp:Label ID="U_UserID" runat="server"><%#Eval("U_UserID")%></asp:Label>
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
                                                <span>Details</span>
                                            </th>
                                            <th >
                                                <span>Notes</span>
                                            </th>
                                            <th >
                                                <span>Recontact</span>
                                            </th>
                                            <th >
                                                <span>User</span>
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
                            <asp:DataPager ID="DataPager1" runat="server" 
                               PagedControlID="lvAll" PageSize="15" 
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
                    <div class="left">
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
