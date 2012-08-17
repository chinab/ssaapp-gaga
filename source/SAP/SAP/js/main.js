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
    },
    rebind_init: function () {
        // Accordion
        $("#accordion").accordion({ header: "h3" });

        // Tabs
        $('#tabs').tabs();
    },
    openEditItem: function (param) {
        $('#dialogEditItem').dialog('open');
        $('#dialogEditItem > #iframeEditItem').attr('src', 'PurchaseOrder_EditItem.aspx?id=' + param);
        return false;
    },
    okEditClick: function () {
        window.parent.__doPostBack(window.parent.Main.myUpdatePanelId, 'EditCallBack');
        window.parent.$('#dialogEditItem').dialog('close');
        return false;
    },
    cancelEditClick: function () {
        window.parent.$('#dialogEditItem').dialog('close');
        return false;
    }
}