<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SystemRoleEdit.aspx.cs" Inherits="SAP.Admin.SystemRoleEdit" %>

<asp:content id="pageContent" runat="Server" contentplaceholderid="ContentPlaceHolder1">
    <fieldset id="systemRoleEdit">
    <div>
        <label>
            <asp:localize id="roleNameLocalize" runat="server" meta:resourcekey="LabelRoleName">
            </asp:localize>            
            <span id="requriedSymbolSpan" runat="server" class="RequiredField">*</span>
        </label>        
        <asp:textbox id="roleNameTextBox" runat="server" cssclass="InputBox">
        </asp:textbox>
        <asp:requiredfieldvalidator id="roleNameRequiredFieldValidator" runat="server" CssClass="RequiredField"
            controltovalidate="roleNameTextBox" display="Dynamic" errormessage="Required" />
    </div>
    <div>
    <label>
            <asp:localize id="Localize1" runat="server" meta:resourcekey="LabelPermissions">
            </asp:localize>            
        </label>  
    <asp:gridview id="permissionGridView" runat="server" autogeneratecolumns="False"  skinid="rolePermissionGridViewSkin"  >
        <columns>
            <asp:boundfield datafield="PageName" HeaderStyle-CssClass="PermissionHeaderColumn" HeaderText="&nbsp;&nbsp;Page Name" />
            <asp:templatefield HeaderStyle-CssClass="PermissionHeaderColumn" itemstyle-cssclass="AccessCheckBoxColumn"> 
                <HeaderTemplate>
                     &nbsp;<input type="checkbox" value="false" id="chkCheckAll" onclick="ChangeAllCheckBoxStates();" title="Check/uncheck all items"/>  
                </HeaderTemplate> 
                <ItemTemplate>
				    <asp:CheckBox id="accessableCheckBox" runat="server" ToolTip="check/uncheck for accessing this page" ></asp:CheckBox>
			    </ItemTemplate>
            </asp:templatefield> 
        </columns> 
    </asp:gridview>
    </div>
    <div>
        <label>
            &nbsp;</label>
        <asp:button id="saveButton" runat="server" cssclass="Button" onclick="SaveButton_Click" 
            text="Save" />
        <input id="resetButton" runat="server" class="Button" type="reset" value="Reset" />
        <asp:button id="deleteButton" runat="server" cssclass="Button" onclick="DeleteButton_Click"
            text="Delete" />
        <asp:button id="cancelButton" runat="server" cssclass="Button" text="Cancel"
            onclientclick="javascript:location.href='SystemRoles.aspx'; return false;" />
    </div>
</fieldset>
<script  type="text/javascript" language="javascript">

    function ChangeCheckBoxState(id, checkState) {
        var cb = document.getElementById(id);
        if (cb != null) {
            cb.checked = checkState;
        }
        else {
            alert('id: ' + id + ' was not found');
        }
    }

    function ChangeAllCheckBoxStates() {
        // Toggles through all of the checkboxes defined in the CheckBoxIDs array
        // and updates their value to the checkState input parameter
        var chkCheckAll = document.getElementById("chkCheckAll");
        if (CheckBoxIDs != null) {
            for (var i = 0; i < CheckBoxIDs.length; i++) {
                //alert("do ChangeAllCheckBoxState " + chkCheckAll.value);
                ChangeCheckBoxState(CheckBoxIDs[i], chkCheckAll.checked);
            }
        }
    }
 

</script>  
</asp:content>

