app.factory("fastestMovingItemService", ["$http", "$q", function ($http, $q) {
   
    var getAllFastestMovingItem = function (value) {
        console.log(value)
        return $http.post("http://localhost:57130/api/FastestMovingItem/GetFastestMovingItem", [{ "FromDate": value.fromDate, "ToDate": value.toDate }]);
    }
    //var getAllCategory = function () {
    //    return $http.get("http://localhost:57130/api/FastestMovingItem/GetFastestMovingItem");
    //}
   
    return {        
        getAllFastestMovingItem: getAllFastestMovingItem,
        //getAllCategory: getAllCategory
    }
}]);