<%@ Page Title="Journal Entry" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="JournalEntry.aspx.cs" Inherits="SAP.JournalEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Main.myUpdatePanelId = '<%= purchaseOrderUpdatePanel.ClientID %>';
    </script>
    <asp:UpdatePanel ID="purchaseOrderUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="contentData" style="padding-left: 15px;">
                <div id="title-form" style="border-bottom: 2px solid black;">
                    <h2>
                        JOURNAL ENTRY</h2>
                    <asp:Label BackColor="Red" ID="lblResults" runat="server" />
                </div>
                <div id="header-form">
                <table >
                    <tr>
                        <td style="width: 127px">
                            Number
                            <br />
                            <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 127px">
                            <span>Posting Date</span>
                            <br />
                            <asp:TextBox ID="txtPostingDate" runat="server" class="txtDate"></asp:TextBox>
                        </td>
                        <td style="width: 127px">
                            Due<span> Date</span>
                            <br />
                            <asp:TextBox ID="txtDueDate" runat="server" class="txtDate"></asp:TextBox>
                        </td>
                        <td style="width: 127px">
                            Doc.<span> Date</span>
                            <br />
                            <asp:TextBox ID="txtDocDate" runat="server" class="txtDate"></asp:TextBox>
                        </td>
                        <td style="width: 127px">
                            Remarks
                            <br />
                            <asp:TextBox ID="txtRemarks" runat="server" Width="245px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                  <table >
                    <tr>
                        <td style="width: 127px">
                            Origin
                            <br />
                            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 127px">
                            Origin No.<br />
                            <asp:TextBox ID="TextBox6" runat="server" ></asp:TextBox>
                        </td>
                        <td style="width: 127px">
                            Trans. No.
                            <br />
                            <asp:TextBox ID="TextBox7" runat="server" ></asp:TextBox>
                        </td>
                        <td style="width: 127px">
                            Template Type
                            <br />
                            <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 127px">
                            Indicator<br />
                            <asp:TextBox ID="TextBox9" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table >
                    <tr>
                        <td style="width: 127px">
                            Trans. Code<br />
                            <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 127px">
                            Ref. 1<br />
                            <asp:TextBox ID="txtRef1" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 127px">
                            Ref. 2<asp:TextBox ID="txtRef2" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 127px">
                            Ref. 3<asp:TextBox ID="txtRef3" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 127px">
                            &nbsp;</td>
                    </tr>
                </table>
                </div>
                <div class="clear">
                </div>
                <div id="content-form">
                    <div id="tabs">   
                        <ul>
                            <li><a href="#tabs-1">Contents</a></li>
                        </ul>                    
                        <div id="tabs-1" style="overflow: auto; height: 327px; margin-top: 0px;">
                            <asp:Button ID="btnAddRecord" Text="Add" runat="server" OnClick="_btnAddRecord_Click" />
                            <br />
                            <asp:ListView ID="lvContents" runat="server" 
                                OnItemInserted="lvContents_ItemInserted" OnItemCommand="lvContents_ItemCommand" 
                                onitemupdating="lvContents_ItemUpdating" ViewStateMode="Enabled" 
                                onitemediting="lvContents_ItemEditing">
                                <LayoutTemplate>
                                    <table class="data_table">
                                        <tr>
                                            <th id="thButtons" runat="server" style="width: 70px">
                                            </th>
                                            <th style="width: 25px">
                                                <span>#</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>G/L Acct/BP Code</span>
                                            </th>
                                            <th style="width: 200px">
                                                <span>G/L Acct/BP Name</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Debit</span>
                                            </th>
                                            <th style="width: 100px">
                                                <span>Credit</span>
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="imgbEdit" runat="server" CommandName="Edit" Text="Edit" ImageUrl="~/skin/icon/edit_icon_mono.gif" />
                                            <asp:LinkButton ID="imgbDelete" runat="server" CommandName="DeleteItem" Text="Delete"
                                                ImageUrl="~/skin/icon/delete_icon_mono.gif" OnClientClick="return confirm('Are you sure you want to delete this row?');"
                                                ToolTip="Delete" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblNo" Text='<%#Eval("No")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblAcctCode"><%#Eval("Account")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDescription"><%#Eval("Dscription")%></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDebit" Text='<%# Eval("Debit") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblCredit"><%#Eval("Credit")%></asp:Label>
                                        </td>
                                       
                                    </tr>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="imgbUpdate" runat="server" CommandName="Update" Text="Update"
                                                ImageUrl="~/skin/icon/save_icon_mono.gif" CausesValidation="true" ValidationGroup="vgrpSaveContact" />
                                            <asp:LinkButton ID="imgbCancel" runat="server" CommandName="CancelUpdate" Text="Cancel"
                                                ImageUrl="~/skin/icon/undo_icon_mono.gif" CausesValidation="false" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Bind("No") %>'/>
                                        </td>
                                       
                                        <td>
                                            <asp:Label ID="lblAcctCode" runat="server" Text='<%# Bind("Account") %>' />
                                            <asp:HyperLink ID="linkItems" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditAccount.aspx\",{0})", "\"id=" + Eval("No").ToString()+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="imgItems" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Dscription")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDebit" Style="text-align: right" runat="server" Text='<%#Eval("Debit")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCredit" Style="text-align: right" runat="server" Text='<%# Bind("Credit")%>' />
                                        </td>
                                        
                                    </tr>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="imgbUpdate" runat="server" CommandName="Insert" Text="Add" ImageUrl="~/skin/icon/save_icon_mono.gif"
                                                CausesValidation="true" ValidationGroup="vgrpSaveContact" />
                                            <asp:LinkButton ID="imgbCancel" runat="server" Text="Cancel" ImageUrl="~/skin/icon/undo_icon_mono.gif"
                                                CommandName="CancelAddNew" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblNo"></asp:Label>
                                        </td>                                     
                                       
                                        <td>
                                            <asp:Label ID="lblAcctCode" runat="server" Text='<%# Bind("Account") %>' />
                                            <asp:HyperLink ID="linkItems" NavigateUrl='<%# String.Format("javascript:Main.openDialog(\"Popup_EditAccount.aspx\",{0})", "\"id=" + "0"+"\"")%>'
                                                runat="server">
                                                <asp:Image ID="imgItems" runat="server" ImageUrl="~/skin/images/item-pointer.gif" />
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDescription"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDebit" runat="server" Text='0' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCredit" runat="server" Text="0" />
                                        </td>
                                       
                                    </tr>
                                </InsertItemTemplate>
                                <EmptyDataTemplate>
                                    <table class="data_table">
                                        <tr>
                                            <th>
                                                <span>#</span>
                                            </th>
                                            <th>
                                                <span>G/L Acct/BP Code</span>
                                            </th>
                                            <th>
                                                <span>G/L Acct/BP Name</span>
                                            </th>
                                            <th>
                                                <span>Debit</span>
                                            </th>
                                            <th>
                                                <span>Credit</span>
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
                    </div>
                    <br />
                    <div class="clear">
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="clear">
                </div>
                <div id="footer-form">
                    <div class="left">
                            <asp:ImageButton ID="btnAdd" runat="server" AlternateText="button" OnClientClick="Dialog.showLoader();"
                            CommandName="Login" Height="19px" Width="65px" 
                            ImageUrl="~/skin/images/SAP_Add.png" onclick="btnAdd_Click" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
