<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetBusinessPartner.aspx.cs"
    Inherits="SAP.GetBusinessPartner" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
    .modalBackground
    {
      background-color:#CCCCFF;
      filter:alpha(opacity=40);
      opacity:0.5;
    }
    .ModalWindow
    {
      border: solid1px#c0c0c0;
      background:#f0f0f0;
      padding: 0px10px10px10px;
      position:absolute;
      top:-1000px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkPopup" 
        PopupControlID="panEdit" BackgroundCssClass="modalBackground" CancelControlID="btnCancel"
        PopupDragHandleControlID="panEdit">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="panEdit" runat="server" Height="180px" Width="400px" CssClass="ModalWindow">
        <h1>
            Edit/h1>
            <table width="100%">
                <tr>
                    <td class="formtext" style="height: 23px; width: 150px;" align="left">
                        Fields1:
                    </td>
                    <td>
                        <asp:TextBox ID="txtFields1" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Button" />
            <asp:Button ID="btnCancel" runat="server" Text="Button" />
    </asp:Panel>
    <asp:LinkButton ID="lnkPopup" runat="server" Text="Show Popup"/>
    </form>
</body>
</html>
