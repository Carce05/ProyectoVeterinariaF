$(document).ready(function () {
    document.title = 'Invoice Item List';

    $("#tblTranDetails").DataTable({
        paging: true,
        select: true,
        "order": [[0, "desc"]],
        dom: 'Bfrtip',


        buttons: [
            'pageLength',
        ],


        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": false,
        "stateSave": true,

        "ajax": {
            "url": "/InvoiceDetails/GetDataTabelData",
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            {
                data: "TranId", "name": "TranId", render: function (data, type, row) {
                    return "<a href='#' onclick=ViewTranDetails('" + row.TranId + "');>" + row.TranId + "</a>";
                }
            },
            {
                data: "ItemId", "name": "ItemId", render: function (data, type, row) {
                    return "<a href='#' onclick=ViewItemHistory('" + row.ItemId + "');>History-" + row.ItemId + "</a>";
                }
            },
            { "data": "ItemName", "name": "ItemName", "autoWidth": true },
            { "data": "CompanyName", "name": "CompanyName", "autoWidth": true },
            { "data": "ItemUnitPrice", "name": "ItemUnitPrice", "autoWidth": true },
            { "data": "ItemSellPrice", "name": "ItemSellPrice", "autoWidth": true },
            { "data": "TranQuantity", "name": "TranQuantity" },
            { "data": "TranTotalPrice", "name": "TranTotalPrice", "autoWidth": true },
            {
                "data": "DateAdded",
                "name": "DateAdded",
                "autoWidth": true,
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
        ],
        "lengthMenu": [[20, 10, 15, 25, 50, 100, 200], [20, 10, 15, 25, 50, 100, 200]],
        "columnDefs": [{
            //"width": 50, targets: 0
        }],
        fixedColumns: true
    });

});
