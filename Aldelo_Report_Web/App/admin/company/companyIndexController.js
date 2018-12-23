app.controller('companyIndexController', ["$scope", "companyService", function ($scope, companyService) {
   $scope.companyList=[];

    $scope.getAllCompany = function () {
        companyService.getAllCompany().then(function (result) {
            $scope.companyList =result.data;
        });
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