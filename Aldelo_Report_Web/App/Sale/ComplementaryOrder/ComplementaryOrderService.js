app.factory("complementaryorderService", ["$http", "$q", function ($http, $q) {
   
    var getComplemnetaryOrder = function (value) {
        console.log(value)
        return $http.post("http://localhost:57130/api/ComplementaryOrder/GetComplementaryOrder", [{ "FromDate": value.fromDate, "ToDate": value.toDate }]);
    }
    var getInitialData = function () {
        return $http.get("http://localhost:57130/api/TotalDeliveryCharge/GetInitialData");
    }
 
    return {        
        getComplemnetaryOrder: getComplemnetaryOrder,
        getInitialData: getInitialData
    }
}]);