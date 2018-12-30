app.factory("companyService", ["$http", "$q", function ($http, $q) {

    var getAllCompany = function () {
        return $http.get("/Company/GetAllCompany");
    }
    var saveCompany = function (company) {
        return $http.post("/Company/SaveCompany",company);
    }
    return {
        getAllCompany: getAllCompany,
        saveCompany: saveCompany
    }
}]);