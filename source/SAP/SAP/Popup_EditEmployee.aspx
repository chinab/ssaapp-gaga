<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Popup_EditEmployee.aspx.cs" Inherits="SAP.Popup_EditEmployee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="/js/jquery-1.4.3.min.js"></script>
    <script type="text/javascript" src="/js/main.js"></script>
    <link href="skin/skin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerEditItem" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="editEmployeeUpdatePanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">       
        <ContentTemplate>
            <asp:TextBox runat="server" ID="txtFilter"></asp:TextBox>
            <asp:Button runat="server" ID="btnFilter" Text="Filter" onclick="btnFilter_Click" />
            <br />
			<div style="overflow:auto;height:200px;">
            <asp:ListView ID="listItems" runat="server">
				<LayoutTemplate>
					<table class="data_table">
						<tr>
                            <th style="width:25px;">
							</th>
							<th>
								<span>Employee No.</span>
							</th>
							<th>
								<span>Employee Code</span>
							</th>
							<th>
								<span>First Name</span>
							</th>	
                            <th>
								<span>Mid Name</span>
							</th>	
                            <th>
								<span>Last Name</span>
							</th>	
						</tr>
						<tr id="itemPlaceholder" runat="server">
						</tr>
					</table>
				</LayoutTemplate>
				<ItemTemplate>
					<tr>
                        <td style="margin:0 0 0 0;padding:0 0 0 0;">
                            <input  type="radio" name="MyRadioButton" value="<%#Eval("No") %>" checked="<%#Eval("Selected") %>"/>
                        </td>
						<td>
							<asp:Label runat="server" ID="Label1"><%#Eval("No") %></asp:Label>
						</td>
						<td>
							<asp:Label runat="server" ID="Label2"><%#Eval("Code")%></asp:Label>
						</td>
						<td>
							<asp:Label runat="server" ID="Label3"><%#Eval("First Name")%></asp:Label>
						</td>	
                        <td>
							<asp:Label runat="server" ID="Label4"><%#Eval("Mid Name")%></asp:Label>
						</td>	
                        <td>
							<asp:Label runat="server" ID="Label5"><%#Eval("Last Name")%></asp:Label>
						</td>						
					</tr>
				</ItemTemplate>
				<EmptyDataTemplate>
					<table class="data_table">
						<tr>
                            <th>
                            </th>
							<th>
								<span>Employee No.</span>
							</th>
							<th>
								<span>Employee Code</span>
							</th>
							<th>
								<span>First Name</span>
							</th>							
                            <th>
								<span>Mid Name</span>
							</th>				
                            <th>
								<span>Last Name</span>
							</th>				
						</tr>
						<tr>
							<td colspan="8">
								<span>No Data</span>
							</td>
						</tr>
					</table>
				</EmptyDataTemplate>
			</asp:ListView>
            </div>
            <div id="action-form">
                <asp:Button ID="btnAdd" Text="OK" runat="server" onclick="btnAdd_Click" />
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClientClick="return Main.cancelDialogClick()" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger  ControlID="btnFilter" />
        </Triggers>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
