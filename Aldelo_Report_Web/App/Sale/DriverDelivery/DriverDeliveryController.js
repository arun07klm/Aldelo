﻿app.controller('DriverDeliveryController', ["$scope", "driverDeliveryService", '$filter', function ($scope, driverDeliveryService ,$filter) {
    $scope.data = [{ "agence": "CTM", "secteur": "Safi", "statutImp": "operationnel" }];

    $scope.categories = [{}];
    $scope.menucategories = [{}];
    $scope.total;
    $scope.id;
    $scope.sale;
    $scope.fromDate;
    $scope.toDate;

    $scope.getTotalDelivery = function () {
        var fromDate =($scope.sale.fromDate === undefined || $scope.sale.fromDate == null || $scope.sale.fromDate == '') ? null : $filter('date')($scope.sale.fromDate, 'dd-MM-yyyy');
        var toDate = ($scope.sale.toDate === undefined || $scope.sale.toDate == null || $scope.sale.toDate == '') ? null : $filter('date')($scope.sale.toDate, 'dd-MM-yyyy');
        var employeeId=$scope.sale.itemid;

        value = {
            "fromDate": (fromDate === undefined || fromDate == null || fromDate == '') ? null : fromDate,
            "toDate": (toDate === undefined || toDate == null || toDate == '') ? null : toDate,
            "employeeId": (employeeId == undefined || employeeId == null || employeeId == '') ? null : employeeId
        }
        driverDeliveryService.getTotalDelivery(value).then(function (result) {
            $scope.categories = (result.data);
        }).catch(function (error) {
            console.log(error);
        });
    }

    $scope.getAllDrivers = function () {
        driverDeliveryService.getAllDrivers().then(function (result) {
            $scope.menucategories = (result.data);
        });
    }
    $scope.search = function () {
        alert("hai" + $scope.sale.itemid + $scope.sale.fromDate + $scope.sale.toDate);
    }
    

    //$scope.print = function () {
    //    html2canvas(document.getElementById('exportthis'), {
    //        onrendered: function (canvas) {
    //            var data = canvas.toDataURL();
    //            var docDefinition = {
    //                content: [{
    //                    image: data,
    //                    width: 500,
    //                }]
    //            };
    //            pdfMake.createPdf(docDefinition).download("test.pdf");
    //        }
    //    });
    //}


    $scope.print=function()
    {
        //var table = tableToJson($('#exportthis').get(0));
        //var doc = new jsPDF('1', 'pt', 'letter', true);
        //doc.cellInitialize();
        //$.each(table, function (i, row) {
        //    $.each(row, function (j, cell) {
        //        doc.cell(1,10,190,20,cell,i)
        //    })
        //})

     //   var doc = new jsPDF()

        // doc.fromHTML($('#exportthis').get(0), 20, 20, { 'width': 500 });


        var columns =
            [
              { title: "customerName", dataKey: "customerName" },
              { title: "cpr", dataKey: "cpr" },
              { title: "item", dataKey: "item" },
              { title: "itemPrice", dataKey: "itemPrice" },
              { title: "totalsale", dataKey: "totalsale" },
           ];   
        var doc = new jsPDF('p', 'pt');
        doc.autoTable(columns, $scope.cust);
        doc.save('a4.pdf')
    }
}]);