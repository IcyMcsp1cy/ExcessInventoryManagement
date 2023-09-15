// Global Variables
var markdownPlanList;
var selectedProductID;
var selectedPlan;

$(document).ready(function () {
    //MarkdownPlan page
    getProducts();
    getMarkdownPlans();
    $('#markdownSelector').change(function () {
        selectMarkdownPlan($(this).val());
    });
    $('#productSelector').change(function () {
        selectedProductID = $(this).val();
    });

    //DailyMetrics page
    $('#dailyMetricsMarkdownSelector').change(function () {
        selectDailyMetricsMarkdownPlan($(this).val());
        if (selectedPlan != undefined) {
            getDailyMetrics(selectedPlan);
        }
    });
});

//MarkdownPlan Page functions
function getProducts() {
    $.ajax({
        url: '/api/Products/getProducts',
        type: 'GET',
        success: function (data) {
            // Loop through the data and add options to the select element
            for (var i = 0; i < data.length; i++) {
                $('#productSelector').append($('<option>', {
                    value: data[i].productId,
                    text: data[i].productName
                }));
            }
        },
        error: function (error) {
            // handle the error
        }
    });
}

function getMarkdownPlans() {
    $.ajax({
        url: '/api/MarkdownPlan/getMarkdownPlans',
        type: 'GET',
        success: function (data) {
            markdownPlanList = data;
            // Loop through the data and add options to the select element
            for (var i = 0; i < data.length; i++) {
                $('#markdownSelector').append($('<option>', {
                    value: data[i].markdownPlanId,
                    text: data[i].planName
                }));
                $('#dailyMetricsMarkdownSelector').append($('<option>', {
                    value: data[i].markdownPlanId,
                    text: data[i].planName
                }));
            }
        },
        error: function (error) {
            // handle the error
        }
    });
}


function selectMarkdownPlan(id) {
    for (i = 0; i < markdownPlanList.length; i++) {
        if (id == markdownPlanList[i].markdownPlanId) {
            selectedPlan = markdownPlanList[i];
        }
    }
    if (selectedPlan != null) {
        $('#planName').val(selectedPlan.planName);
        $('#startDate').val(selectedPlan.startDate);
        $('#endDate').val(selectedPlan.endDate);
        $('#initialReduction').val(selectedPlan.initialPriceReduction);
        $('#midwayReduction').val(selectedPlan.midwayPriceReduction);
        $('#finalReduction').val(selectedPlan.finalPriceReduction);
        const $selectProduct = document.querySelector('#productSelector');
        $selectProduct.value = selectedPlan.productId;
    }

}

function createMarkdownPlan() {
    var currentPlanId;
    if (selectedPlan != null) {
        currentPlanId = selectedPlan.markdownPlanId
    }
    else {
        currentPlanId = 0;
    }
    var dataObject = JSON.stringify({
        markdownPlanId: currentPlanId,
        ProductId: selectedProductID,
        'PlanName': $('#planName').val(),
        'StartDate': new Date($('#startDate').val()),
        'EndDate': new Date($('#endDate').val()),
        'InitialPriceReduction': $('#initialReduction').val(),
        'MidwayPriceReduction': $('#midwayReduction').val(),
        'FinalPriceReduction': $('#finalReduction').val(),
    });

    console.log(dataObject);

    $.ajax({
        url: 'api/MarkdownPlan/CreateMarkdownPlan',
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        cache: false,
        data: dataObject,
        dataType: 'json',
        success: function (result) {    // Returns True/False
            // TODO add success/failure message
        },
        failure: function (xhr, status, err) {
            console.log(xhr + " " + status + " " + err);
        }
    });
}

function deleteMarkdownPlan() {
    $.ajax({
        url: 'api/MarkdownPlan/DeleteMarkdownPlan?markdownPlanId=' + selectedPlan.markdownPlanId,
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        cache: false,
        dataType: 'json',
        success: function (result) {    // Returns True/False
            // TODO add success/failure message
        },
        failure: function (xhr, status, err) {
            console.log(xhr + " " + status + " " + err);
        }
    });
}


// Sales Page functions
function createSalesData() {
    var salesObject = JSON.stringify({
        markdownPlanId: selectedPlan.markdownPlanId,
        'SalesData': $('#salesData').val(),
    });

    console.log(salesObject);

    $.ajax({
        url: 'api/Sales/CreateSalesData',
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        cache: false,
        data: salesObject,
        dataType: 'json',
        success: function (result) {    // Returns True/False
            // TODO add success/failure message
        },
        failure: function (xhr, status, err) {
            console.log(xhr + " " + status + " " + err);
        }
    });
}

// DailyMetrics page functions
function selectDailyMetricsMarkdownPlan(id) {
    for (i = 0; i < markdownPlanList.length; i++) {
        if (id == markdownPlanList[i].markdownPlanId) {
            selectedPlan = markdownPlanList[i];
        }
    }
}

function getDailyMetrics(markdownPlan) {
    $.ajax({
        url: '/api/DailyMetrics/GetDailyMetrics?markdownPlanId=' + markdownPlan.markdownPlanId,
        type: 'GET',
        success: function (data) {
            var tbl = document.getElementById('dailyMetricsTable');
            for (i = 0; i < data.length; i++) {
                row = tbl.insertRow(tbl.rows.length);
                createCell(row.insertCell(0), i + 1);
                createCell(row.insertCell(1), data[i].unitsSold);
                createCell(row.insertCell(2), data[i].margin);
                createCell(row.insertCell(3), data[i].profit);
                createCell(row.insertCell(4), data[i].remainingInventory);
            }
        },
        error: function (error) {
            // handle the error
        }
    });
}

function createCell(cell, text) {
    var div = document.createElement('div'), // create DIV element
        txt = document.createTextNode(text); // create text node
    div.appendChild(txt);                    // append text node to the DIV
    cell.appendChild(div);                   // append DIV to the table cell
}