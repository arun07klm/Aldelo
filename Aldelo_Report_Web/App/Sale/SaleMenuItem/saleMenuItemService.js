app.factory("saleMenuItemCategoryService", ["$http", "$q", function ($http, $q) {
   
    var getAllsaleMenuItem = function () {
        return $http.get("http://localhost:57130/api/SaleMenuItem/GetSaleMenuItem");
    }
    var getAllmenuItem = function () {
        return $http.get("http://localhost:57130/api/SaleMenuItem/GetAllmenuItem");
    }
   
    return {        
        getAllsaleMenuItem: getAllsaleMenuItem,
        getAllmenuItem:getAllmenuItem
    }
}]);