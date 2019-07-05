// StockItem Create

function handleItemTypeSelection() {
    var itemType = $("#ItemType").val();
    var $hardness = $("#Hardness");
    var $length = $("#Length");

    //console.log($("#ItemType").val());
    if (itemType === '0') {
        // When Roll is selected, disable Length and Hardness Width Option
        $hardness.children("option[value=1]").attr('disabled', "true");
        $hardness.val(0);
        $length.attr("readonly", "true");
    } else {
        // When Sheets are selected, return Length and Hardness to default state
        $hardness.children("option[value=1]").removeAttr('disabled');
        $length.removeAttr("readonly");
    }
}