app.factory("totallDeliveryChargeService", ["$http", "$q", function ($http, $q) {
   
    var getTotalDeliveryCharge = function (value) {
        console.log(value)
        return $http.post("http://localhost:57130/api/TotalDeliveryCharge/GetTotalDeliveryCharge", [{ "EmployeeId":value.employeeId,"FromDate": value.fromDate, "ToDate": value.toDate }]);
    }
    var getInitialData = function () {
        return $http.get("http://localhost:57130/api/TotalDeliveryCharge/GetInitialData");
    }
 
    return {        
        getTotalDeliveryCharge: getTotalDeliveryCharge,
        getInitialData: getInitialData
    }
}]);