app.factory("saleCategoryService", ["$http", "$q", function ($http, $q) {
   
    var getAllsaleCategory = function () {
        return $http.get("http://localhost:57130/api/SaleCategory/GetSaleCategories");
    }
    var getAllCategory = function () {
        return $http.get("http://localhost:57130/api/SaleCategory/GetAllCategories");
    }
   
    return {        
        getAllsaleCategory: getAllsaleCategory,
        getAllCategory: getAllCategory
    }
}]);