app.factory("totalPayoutService", ["$http", "$q", function ($http, $q) {
   
    var getTotalPayout = function (value) {
        console.log(value)
        return $http.post("http://localhost:57130/api/RefundAmountDetails/GetTotalRefundAmount", [{  "FromDate": value.fromDate, "ToDate": value.toDate }]);
    }
    var getInitialData = function () {
        return $http.get("http://localhost:57130/api/TotalDeliveryCharge/GetInitialData");
    }
 
    return {        
        getTotalPayout: getTotalPayout,
        getInitialData: getInitialData
    }
}]);