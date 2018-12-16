app.factory("totallRefundService", ["$http", "$q", function ($http, $q) {
   
    var getTotalRefundAmount = function (value) {
        console.log(value)
        return $http.post("http://localhost:57130/api/RefundAmountDetails/GetTotalRefundAmount", [{  "FromDate": value.fromDate, "ToDate": value.toDate }]);
    }
    var getInitialData = function () {
        return $http.get("http://localhost:57130/api/TotalDeliveryCharge/GetInitialData");
    }
 
    return {        
        getTotalRefundAmount: getTotalRefundAmount,
        getInitialData: getInitialData
    }
}]);