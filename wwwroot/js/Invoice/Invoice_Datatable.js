$(document).ready(function () {
    document.title = 'Lista de Factura';

    $("#tblTransactions").DataTable({
        paging: true,
        select: true,

        orderCellsTop: true,
        fixedHeader: true,

        "order": [[0, "desc"]],
        dom: 'Bfrtip',

        buttons: [
            'pageLength',
            {
                extend: 'collection',
                text: 'Export',
                buttons: [
                    {
                        extend: 'pdfHtml5',
                        customize: function (doc) {
                            //doc.content[1].margin = [100, 0, 100, 0];
                            //Remove the title created by datatTables
                            doc.content.splice(0, 1);
                            //Create a date string that we use in the footer. Format is dd-mm-yyyy
                            var now = new Date();
                            var jsDate = now.getDate() + '-' + (now.getMonth() + 1) + '-' + now.getFullYear();

                            doc.pageMargins = [20, 60, 20, 30];
                            // Set the font size fot the entire document
                            doc.defaultStyle.fontSize = 7;
                            // Set the fontsize for the table header
                            doc.styles.tableHeader.fontSize = 10;


                            doc['header'] = (function () {
                                return {
                                    columns: [
                                        {
                                            alignment: 'left',  //center
                                            italics: true,
                                            text: 'Lista de Factura',
                                            fontSize: 18,
                                            margin: [0, 0]
                                        }
                                    ],
                                    margin: 20
                                }
                            });

                            // Create a footer object with 2 columns
                            doc['footer'] = (function (page, pages) {
                                return {
                                    columns: [
                                        {
                                            alignment: 'left',
                                            text: ['Created on: ', { text: jsDate.toString() }]
                                        },
                                        {
                                            alignment: 'right',
                                            text: ['page ', { text: page.toString() }, ' of ', { text: pages.toString() }]
                                        }
                                    ],
                                    margin: 5
                                }
                            });
                            // Change dataTable layout (Table styling)
                            // To use predefined layouts uncomment the line below and comment the custom lines below
                            // doc.content[0].layout = 'lightHorizontalLines'; // noBorders , headerLineOnly
                            var objLayout = {};
                            objLayout['hLineWidth'] = function (i) { return .5; };
                            objLayout['vLineWidth'] = function (i) { return .5; };
                            objLayout['hLineColor'] = function (i) { return '#aaa'; };
                            objLayout['vLineColor'] = function (i) { return '#aaa'; };
                            objLayout['paddingLeft'] = function (i) { return 4; };
                            objLayout['paddingRight'] = function (i) { return 4; };
                            doc.content[0].layout = objLayout;
                        },


                        orientation: 'portrait', // landscape
                        pageSize: 'A4',
                        pageMargins: [0, 0, 0, 0], // try #1 setting margins
                        margin: [0, 0, 0, 0], // try #2 setting margins
                        text: '<u>PDF</u>',
                        key: { // press E for export PDF
                            key: 'e',
                            altKey: false
                        },
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5], //column id visible in PDF
                            modifier: {
                                // DataTables core
                                order: 'index',  // 'current', 'applied', 'index',  'original'
                                page: 'all',      // 'all',     'current'
                                search: 'none'     // 'none',    'applied', 'removed'
                            }
                        }
                    },
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5],
                            page: 'all'
                        }
                    }
                ]
            }
        ],

        "processing": true,
        "serverSide": true,
        "filter": true, //Search Box
        "orderMulti": false,
        "stateSave": true,

        "ajax": {
            "url": "/Invoice/GetDataTabelData",
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            {
                data: "TranId", "name": "TranId", render: function (data, type, row) {
                    return "<a href='#' onclick=ViewTranDetails('" + row.TranId + "');>" + row.TranId + "</a>";
                }
            },
            { "data": "NetAmount", "name": "NetAmount", "autoWidth": true },
            {
                "data": "NetPayable",
                "name": "NetPayable",
                "autoWidth": true,
                "render": function (data, type, full) {
                    return parseFloat(data).toFixed(2);
                }
            },
            { "data": "PaidAmount", "name": "PaidAmount", "autoWidth": true },
            {
                "data": "ChangedAmount",
                "name": "ChangedAmount",
                "autoWidth": true,
                "render": function (data, type, full) {
                    return parseFloat(data).toFixed(2);
                }
            },
            { "data": "DueAmount", "name": "DueAmount", "autoWidth": true },
            { "data": "ModeOfPayment", "name": "ModeOfPayment", "autoWidth": true },
            {
                data: "TranId", "name": "TranId", render: function (data, type, row) {
                    return "<a href='#' onclick=ViewCustomerDetails('" + row.TranId + "');>" + row.CustomerName + "</a>";
                }
            },

            {
                "data": "TranDate",
                "name": "TranDate",
                "autoWidth": true,
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            {
                "data": "DueDate",
                "name": "DueDate",
                "autoWidth": true,
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },

            {
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='btn btn-link btn-xs' onclick=PrintInvoice('" + row.TranId + "');><span class='glyphicon glyphicon-print'></span>Print</a>";
                }
            },
            {
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='btn btn-info btn-xs' onclick=EditTransactions('" + row.TranId + "');>Edit</a>";
                }
            },
            {
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='btn btn-danger btn-xs' onclick=DeleteTran('" + row.TranId + "'); >Delete</a>";
                }
            }
        ],

        'columnDefs': [{
            'targets': [9, 10, 11],
            'orderable': false,
        }],
        "lengthMenu": [[20, 10, 15, 25, 50, 100, 200], [20, 10, 15, 25, 50, 100, 200]]
    });
});