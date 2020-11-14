﻿
function onSearch() {
    search('');
}

function search(pageNumber) {
    startLoader();
    getGridData(pageNumber);

    //var e = document.getElementById("srchJobId");
    //var srchJobId = e.options[e.selectedIndex].value;
    //
    //if(srchJobId==0)
    //    srchJobId='';
    //
    //var e = document.getElementById("srchCityId");
    //var srchCityId = e.options[e.selectedIndex].value;
    //
    //if(srchCityId==0)
    //    srchCityId='';
    //
    //var e = document.getElementById("srchRegionId");
    //var srchRegionId = e.options[e.selectedIndex].value;
    //
    //if(srchRegionId==0)
    //    srchRegionId='';
    //
    //var e = document.getElementById("srchTypeId");
    //var srchTypeId = e.options[e.selectedIndex].value;
    //
    //if(srchTypeId==0)
    //    srchTypeId='';
    //
    //var e = document.getElementById("srchPageSize");
    //var srchPageSize = e.options[e.selectedIndex].value;
    //
    //if(srchPageSize==0)
    //    srchPageSize='';
    //
    //var e = document.getElementById("srchMap");
    //var srchMap = e.options[e.selectedIndex].value;
    //
    //if(srchMap==0)
    //    srchMap='';
    //
    //var srchUrl = $('#srchUrl').val();
    //var srchFAQ = $('#srchFAQ').val();
    //var srchContractEndDate = $('#srchContractEndDate').val();
    //var srchPartOfTitle = $('#srchPartOfTitle').val();
    //
    //getGridData(srchUrl, srchJobId, srchTypeId, srchCityId, srchRegionId,srchMap,srchFAQ,srchContractEndDate,srchPartOfTitle,srchPageSize, pageNumber);
}

function getGridData(pageNumber) {
    var serviceURL = '/WebsiteContract/';

    //var url =  serviceURL + "Search" + "?Url=" + srchUrl + "&JobId=" + srchJobId + "&srchTypeId=" + srchTypeId  + "&cityId=" + srchCityId 
    //                      + "&RegionId=" + srchRegionId + "&map=" + srchMap + "&FAQ=" + srchFAQ + "&ContractEndDate=" + srchContractEndDate 
    //                      + "&partOfTitle=" + srchPartOfTitle+ "&PageSize=" + srchPageSize+ "&pageNumber=" + pageNumber;

    var url = serviceURL + "Search" + "?PageNumber=" + pageNumber;

    $.ajax({
        type: "POST",
        data: {},
        traditional: true,
        contentType: "application/json; charset=utf-8",
        url: url,
        dataType: "json",
        async: true,
        success: function (response) {
            fillGrid(response.ContractList, pageNumber, response.PageSize);
            setPageCount(response.PageCount, pageNumber);
            endLoader();
        }
    });
}

$(document).ready(function () {
    getGridData('');
});

function fillGrid(Array, pageNumber, pageSize) {

    if (pageNumber == 0)
        pageNumber = 1;

    clearGrid();
    var rowIndex = 0;

    var ids = '';
    for (var i = 1; i <= Array.length; i++) {
        rowIndex++;

        if (rowIndex != 1) {
            ids += ',';
        }

        var table = document.getElementById('dataGrid');
        if (table != null) {
            var row = table.insertRow(table.row);
            var cell1 = row.insertCell(0);
            var cell2 = row.insertCell(1);
            var cell3 = row.insertCell(2);
            var cell4 = row.insertCell(3);
            var cell5 = row.insertCell(4);
            var cell6 = row.insertCell(5);
            var cell7 = row.insertCell(6);
            var cell8 = row.insertCell(7);
            var cell9 = row.insertCell(8);

            cell1.innerHTML = ((pageNumber - 1) * pageSize) + rowIndex;
            cell2.innerHTML = Array[i - 1].ContractNo;
            cell3.innerHTML = Array[i - 1].PersianContractDate;
            cell4.innerHTML = Array[i - 1].CustomerName;
            cell5.innerHTML = Array[i - 1].CustomerManagerName;
            cell6.innerHTML = Array[i - 1].CustomerMobile;
            cell7.innerHTML = Array[i - 1].DomainId;
            cell8.innerHTML = Array[i - 1].PaymentTotalPriceStr;
            cell9.innerHTML = generateInRowToolbar('WebsiteContract', Array[i - 1].Id, ' قرارداد ' + Array[i - 1].Title);

            ids += Array[i - 1].Id;
        }
    }

    $('#gridIds').val(ids);


    var grIds = $('#gridIds').val();
}

function clearGrid() {
    $("#dataGrid").find("tr:gt(0)").remove();
}

function setPageCount(size, pageNumber) {
    if (size > 1) {
        var div = document.getElementById('gridPageBtn');
        div.innerHTML = '';

        if (pageNumber == '')
            pageNumber = 1;

        var startIndex = 1;
        var startIndexValue = 5 - pageNumber;

        while (startIndexValue <= 0) {
            startIndex++;
            startIndexValue++;
        }

        var rowItem = '';
        for (var i = startIndex; i < startIndex + 5; i++) {
            rowItem += '<div class="btn btn-default" href="" onclick="search(' + i + ')" style="cursor:pointer">' + i + '</div>';
        }

        $('div[class="gridPageBtn"').append(rowItem);
    }
}


function GenerateToolbar(controllerName) {
    // var toolbar = document.getElementById('gridToolbar');
    //  var createBtn = '<div class="btn btn-success"><i class="fa fa-plus" ><span style="padding-right:2px; font-size:large; font-family: ' +
    //     'Vazir' + ', sans-serif !important;">جدید</span></i></div>';
    // $('div[class="gridtoolbar"').append(createBtn)
}

function generateInRowToolbar(ControllerName, param, content) {
    var viewBtn = '<div class="btn btn-info margin2" style="cursor:pointer" data-toggle="tooltip" data-placement="top" title="مشاهده" onclick="goToControllerAction(' + '\'' +
        ControllerName + '\'' + ',' + '\'' + 'view' + '\'' + ',' + param + ')"><i class="fa fa-eye"></i></div>';
    var updateBtn = '<div class="btn btn-warning margin2" style="cursor:pointer" data-toggle="tooltip" data-placement="top" title="ویرایش" onclick="goToControllerAction(' + '\'' +
        ControllerName + '\'' + ',' + '\'' + 'update' + '\'' + ',' + param + ')"><i class="fa fa-edit"></i></div>';
    var deleteBtn = '<div class="btn btn-danger margin2"  style="cursor:pointer" data-toggle="tooltip" data-placement="top" title="حذف" onclick="deleteRow(' + '\'' +
        ControllerName + '\'' + ',' + '\'' + 'Index' + '\'' + ',' + param + ',' + '\'' + content + '\'' + ')"><i class="fa fa-remove"></i></div>';
    var GalleryBtn = '<div class="btn btn-primary margin2" style="cursor:pointer" data-toggle="tooltip" data-placement="top" title="گالری" onclick="goToControllerAction(' + '\'' +
        ControllerName + '\'' + ',' + '\'' + 'Gallery' + '\'' + ',' + param + ')"><i class="fa fa-image"></i></div>';


    var context = viewBtn;
    //var context = viewBtn +  deleteBtn;

    return context;
}

function removeRow() {
    var rowIndex = $('#rowIndex').val();
    if (rowIndex != undefined && rowIndex != 0 && rowIndex != 1) {
        var grid = document.getElementById("GridData");
        grid.deleteRow(rowIndex - 1);
        $('#rowIndex').val(--rowIndex);
    }
}

function addRow() {
    var rowIndex = $('#rowIndex').val();
    if (rowIndex == undefined || rowIndex == 0) {
        rowIndex = 1
    }

    var table = document.getElementById('GridData');
    if (table != null) {
        var row = table.insertRow(table.row);
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);
        var cell4 = row.insertCell(3);
               
        cell1.innerHTML = rowIndex;
        cell2.innerHTML = '<input class="form-control" id="Amount' + rowIndex + '" />';
        cell3.innerHTML = '<input type="text" id="Date' + rowIndex + '"class="form-control" />';
        cell4.innerHTML = '<input type="checkbox" id="IsPrePayment' + rowIndex + '"class="form-control" style="margin-top:10px;" />';
                             
        kamaDatepicker('Date'+rowIndex, { buttonsColor: "red", forceFarsiDigits: true, markHolidays: true, gotoToday: true, placeholder: '' });

        $('#rowIndex').val(++rowIndex);
    }
}

function getCustomerInfo(){    
     var e = document.getElementById('CustomerId');
     var id = e.options[e.selectedIndex].value;

if(id!=0){
    var url = "/Customer/GetCustomer/"+id;
        $.ajax({url: url, success: function(result){
            
            fillCustomerInfo(result);
        }});
    }
    else{
    clearCustomerInfo();
    }
}

function fillCustomerInfo(result){
    
    document.getElementById('ManagerName').value=result.ManagerName;
    document.getElementById('MobileNumber').value=result.Mobile;

    //$("State select").val(result.StateId);

    //$('.State option[value='+result.StateId+']').attr('selected','selected').change();

    document.getElementById('CustomerCity').value=result.CityId;
    $("#State").val(result.StateId).change();
    //$("#CityId").val(result.CityId).change();

}

function clearCustomerInfo(){
    document.getElementById('ManagerName').value='';
    document.getElementById('MobileNumber').value='';
}

function submit() {
    startLoader();
    hideErrorPopUp();

    var contractItems = getPaymentInfo();

   var e = document.getElementById('CustomerId');
   var customerValue = e.options[e.selectedIndex].value;
   var customerTitle = e.options[e.selectedIndex].label;


   var account = document.getElementById('AccountId');
   var accountValue = account.options[e.selectedIndex].value;

    var Contract = 
    { 
      CustomerId: customerValue,CustomerName:customerTitle,AccountId:accountValue,
      WebsiteContractItems: contractItems, CustomerId: customerValue, PaymentTotalPrice: $('#PaymentTotalPrice').val(),
      CustomerManagerName: $('#ManagerName').val(), CustomerMobile: $('#MobileNumber').val(),CustomerAddress: $('#Address').val(),
      DomainId: $('#DomainId').val(), CustomerCityId: $('#CityId').val(),CustomerPhone: $('#Phone').val(),
      ContractDate:$('#ContractDate').val(),ExecuteDuration:$('#ExecuteDuration').val(),
      DomainRegistrationCost:$('#DomainCost').val(),HostRegistrationCost:$('#HostCost').val(),
    }
    debugger;
    var url = "/WebsiteContract/Submit";
    $.post(url, Contract, function (data) {
        endLoader();
        if (data != '') {
            showErrorPopUp(data);
        }
        else {
            disablePage();
            //document.getElementById('ContractContext').disabled = true;
        }
    });
}

function getPaymentInfo() {
    debugger;
    var rowIndex = $('#rowIndex').val();
    if (rowIndex == undefined || rowIndex == 0) {
        return null;
    }

    var data = [];

    for (var i = 1; i < rowIndex; ++i) {

        //var e = document.getElementById('PlanCode' + i);
        //var PlanCodeValue = e.options[e.selectedIndex].value;
        //var PlanCode = PlanCodeValue;

        var Date = $('#Date' + i).val();
        var Amount = $('#Amount' + i).val();
        var IsPrePayment =document.getElementById('IsPrePayment' + i).checked;

        var item = {
            Date: Date, Amount: Amount, IsPrePayment: IsPrePayment, DisplayOrder : i,
        };

        data.push(item);
    }

    return data;
}

function getStateCities()    {
    
        var e = document.getElementById('State');
        var id = e.options[e.selectedIndex].value;
        var url = "/GeoDivision/GetCities/"+id;

      $.ajax({url: url, success: function(result){
            
                fillCitySelect(result);
        }});
}

function fillCitySelect(data){

var select = document.getElementById('CityId');
 select.innerHTML = '';
var customerCity = document.getElementById('CustomerCity').value;

    for (var i = 0; i<data.length; i++){
        
        var item = data[i];

        if(customerCity==item.Id){
            select.appendChild(new Option(item.Title,item.Id));
            $("#CityId").val(customerCity).change();

        }
        else{
            select.appendChild(new Option(item.Title,item.Id));
        }

    }
}
