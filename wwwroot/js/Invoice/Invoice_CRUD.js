
var ViewTranDetails = function (TranId) {
    var url = "/Invoice/ViewTranDetails?TranId=" + TranId;
    $('#titlePrintModal').html("Invoice Summary: " + TranId);
    loadPrintModal(url);
};



var PrintInvoice = function (TranId) {
    location.href = "/Invoice/PrintInvoice?TranId=" + TranId;
};


var GetByItem = function () {
    var SelectItemValue = $("#ddlItem").val();
    $.ajax({
        type: "GET",
        url: "/Invoice/GetByItem?Id=" + SelectItemValue,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data === null) return;
            $('#itemId').val(data.Id);
            $('#itemDescription').val(data.Name);
            $('#itemHeight').val(data.Measure);
            $('#itemAvailQty').val(data.Quantity);
            $('#itemUnitPrice').val(data.UnitPrice);
            $('#itemSellPrice').val(data.SellPrice);
            $('#itemTransQty').val(1);
            $('#itemTransQtyView').val(1);

            TranItemTotalPrice();
        },
        error: function (response) {
            console.log(response);
        }
    });
};


function TranItemTotalPrice() {
    var itemTransQty = $("#itemTransQty").val();
    var itemSellPrice = $("#itemSellPrice").val();
    $('#itemTransQtyView').val(itemTransQty);
    $('#itemTotalPrice').val(itemTransQty * itemSellPrice);
}


var GetByCustomerInfo = function () {
    var SelectCustomerValue = $("#ddlCustomer").val();

    $.ajax({
        type: "GET",
        url: "/CustomerInfo/GetByCustomerInfo?Id=" + SelectCustomerValue,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data === null) return;
            $('#CustId').val(data.Id);
            $('#CustName').val(data.Name);
            $('#CustPhone').val(data.Phone);

            $('#CustEmail').val(data.Email);
            $('#CustBillingAddress').val(data.BillingAddress);
        },
        error: function (response) {
            console.log(response);
        }
    });
};

var TranEditAddNewItem = function (id) {
    var url = "/Invoice/TranEditAddNewItem?TranId=" + id;
    $('#titleBigModal').html("Add New Invoice Item");
    loadBigModal(url);
};

var EditTransactions = function (TranId) {
    var url = "/Invoice/EditTransactions?TranId=" + TranId;
    location.href = url;
};

var DeleteTran = function (id) {
    Swal.fire({
        title: 'Do you want to delete this item?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/Invoice/DeleteTran?id=" + id,
                success: function (result) {
                    Swal.fire({
                        text: 'Deleted!', title: 'Item has been deleted. Tran ID: ' + result.TranId,
                        type: "success",
                        onAfterClose: () => {
                            $('#tblTransactions').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};

var DeleteTranItem = function (id) {
    Swal.fire({
        title: 'Do you want to delete this item?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/Invoice/DeleteTranItem?id=" + id,
                success: function () {
                    $("#MainTran").load(location.href + " #MainTran");
                }
            });
        }
    });
};

$('body').on('click', "#btnUpdate", function () {
    var myformdata = $("#editTransactionsform").serialize();

    $.ajax({
        type: "PUT",
        url: "/Invoice/EditTransactions/",
        data: myformdata,
        success: function (result) {
            var message = "Item has been updated successfully. Invoice ID: " + result.TranId;
            Swal.fire({
                title: message,
                type: "success"
            }).then(function () {
                location.href = "/Invoice/Index/";
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
});

