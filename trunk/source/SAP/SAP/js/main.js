$(document).ready(function () {
    // layout
    $("#splitterContainer").splitter({
        minAsize: 250,
        maxAsize: 300,
        splitVertical: true,
        A: $('#leftPane'),
        B: $('#rightPane'),
        closeableto: 0
    });
    $("#splitterContainer").height($('body').height() - 55);

    // Accordion
    $("#accordion").accordion({ header: "h3" });

    // Tabs
    $('#tabs').tabs();
});