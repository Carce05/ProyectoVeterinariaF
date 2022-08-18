var ViewTranDetails = function (TranId) {
    var url = "/Facturacion/Index?TranId=" + TranId;
    $('#titlePrintModal').html("Invoice Summary: " + TranId);
    loadPrintModal(url);
};

var ViewItemHistory = function (ItemId) {
    var url = "/ItemsHistory/ViewItemHistory?ItemId=" + ItemId;
    $('#titleBigModal').html("Item History. Item ID: " + ItemId);
    loadBigModal(url);
};