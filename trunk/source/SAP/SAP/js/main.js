﻿function pageLoad(sender, args) {
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
    widthDialog: 0,
    heightDialog: 0,
    init: function () {
        if ($('#isMainPage').val() == '1') {
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

            //initialize dialog
            $('#dialogFrame').dialog({
                autoOpen: false,
                modal: true,
                width: this.widthDialog,
                height: this.heightDialog
            });
            //

            $(".txtDate").datepicker();
        }
    },
    rebind_init: function () {
        if ($('#isMainPage').val() == '1') {
            // Accordion
            $("#accordion").accordion({ header: "h3" });

            // Tabs
            $('#tabs').tabs();

            //rebinf dialog
            $('#dialogFrame').dialog({
                autoOpen: false,
                modal: true,
                width: this.widthDialog,
                height: this.heightDialog
            });
            //
        }
    },
    openCustomDialog: function (url, _width, _height, param) {
        $('#dialogFrame').dialog({
            autoOpen: false,
            modal: true,
            width: _width,
            height: _height
        });
        this.widthDialog = _width;
        this.heightDialog = _height;
        $('#dialogFrame').dialog('open');
        $('#dialogFrame > #iframeItem').attr('src', url + (param == '' || param == undefined ? '' : ("?" + param)));
    },
    openDialog: function (url, param) {
        this.openCustomDialog(url, 600, 300, param);
    },
    okDialogClick: function (action) {
        window.parent.__doPostBack(window.parent.Main.myUpdatePanelId, action);
        window.parent.$('#dialogFrame').dialog('close');
    },
    cancelDialogClick: function () {
        window.parent.$('#dialogFrame').dialog('close');
    },
}