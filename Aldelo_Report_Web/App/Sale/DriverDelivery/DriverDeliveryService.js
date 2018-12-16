app.factory("driverDeliveryService", ["$http", "$q", function ($http, $q) {
   
    var getTotalDelivery= function (value) {
        console.log(value)
        return $http.post("http://localhost:57130/api/DriverDelivery/getTotalDeliveryCharge", [{ "employeeId": value.employeeId, "FromDate": value.fromDate, "ToDate": value.toDate }]);
    }
    var getAllDrivers = function () {
        return $http.get("http://localhost:57130/api/DriverDelivery/getInitialData");
    }
   
    return {        
        getTotalDelivery: getTotalDelivery,
        getAllDrivers: getAllDrivers
    }
}]);