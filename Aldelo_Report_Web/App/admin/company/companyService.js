app.factory("companyService", ["$http", "$q", function ($http, $q) {
   
    var getAllCompany = function () {
        return $http.get("/Company/GetAllCompany");
    }
   
    return {        
        getAllCompany: getAllCompany
    }
}]);