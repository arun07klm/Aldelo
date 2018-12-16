app.factory("custService", ["$http", "$q", function ($http, $q) {
   
    var getAllCust = function () {
        return $http.get("http://localhost:57130/api/Sale/Getcustomer");
    }
   
    return {        
        getAllCust: getAllCust        
    }
}]);