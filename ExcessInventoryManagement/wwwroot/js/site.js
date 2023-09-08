// Global Variables
var markdownPlanList;
var selectedProductID;
var selectedPlan;

$(document).ready(function () {
    getProducts();
    getMarkdownPlans();
    $('#markdownSelector').change(function () {
        selectMarkdownPlan($(this).val());
    });
    $('#salesMarkdownSelector').change(function () {
        selectSalesMarkdownPlan($(this).val());
    });
    $('#productSelector').change(function () {
        selectedProductID = $(this).val();
    });
});

//Home Page functions
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
            // DO SOMETHING
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
            // DO SOMETHING
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
            // DO SOMETHING
        },
        failure: function (xhr, status, err) {
            console.log(xhr + " " + status + " " + err);
        }
    });
}

function selectSalesMarkdownPlan(id) {
    for (i = 0; i < markdownPlanList.length; i++) {
        if (id == markdownPlanList[i].markdownPlanId) {
            selectedPlan = markdownPlanList[i];
        }
    }
}