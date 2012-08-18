function pageLoad(sender, args) {
    if (args.get_isPartialLoad()) {
        //rebind jquery after partial load
        Main.rebind_init();
    }
}
/* init */
$(document).ready(function () {
    Main.init();
});

Main = {
    myUpdatePanelId: '',
    init: function () {
        // layout
        $("#splitterContainer").splitter({
            minAsize: 250,
            maxAsize: 300,
            splitVertical: true,
            A: $('#leftPane'),
            B: $('#rightPane'),
            closeableto: 0
        });
        $("#splitterContainer").height($('body').height() - 75);

        // Accordion
        $("#accordion").accordion({ header: "h3" });

        // Tabs
        $('#tabs').tabs();

        $('#dialogEditItem').dialog({
            autoOpen: false,
            width: 600
        });
        $('#dialogEditVendor').dialog({
            autoOpen: false,
            width: 600
        });
    },
    rebind_init: function () {
        // Accordion
        $("#accordion").accordion({ header: "h3" });

        // Tabs
        $('#tabs').tabs();

        $('#dialogEditItem').dialog({
            autoOpen: false,
            width: 600
        });
        $('#dialogEditVendor').dialog({
            autoOpen: false,
            width: 600
        });
    },
    openEditItem: function (param) {
        $('#dialogEditItem').dialog('open');
        $('#dialogEditItem > #iframeEditItem').attr('src', 'PurchaseOrder_EditItem.aspx?id=' + param);
        return false;
    },
    okEditItemClick: function () {
        window.parent.__doPostBack(window.parent.Main.myUpdatePanelId, 'EditItemCallBack');
        window.parent.$('#dialogEditItem').dialog('close');
        return false;
    },
    cancelEditItemClick: function () {
        window.parent.$('#dialogEditItem').dialog('close');
        return false;
    },
    openEditVendor: function (param) {
        $('#dialogEditVendor').dialog('open');
        $('#dialogEditVendor > #iframeEditVendor').attr('src', 'PurchaseOrder_EditVendor.aspx');
        return false;
    },
    okEditVendorClick: function () {
        alert(1);
        window.parent.__doPostBack(window.parent.Main.myUpdatePanelId, 'EditVendorCallBack');
        alert(2);
        window.parent.$('#dialogEditVendor').dialog('close');
        return false;
    },
    cancelEditVendorClick: function () {
        window.parent.$('#dialogEditVendor').dialog('close');
        return false;
    }
}