app.factory("companyService", ["$http", "$q", function ($http, $q) {

    var getAllCompany = function () {
        return $http.get("/Company/GetAllCompany");
    }
    var saveCompany = function (company) {
        return $http.post("/Company/SaveCompany",company);
    }
    var getAllMenu = function () {
        return $http.get("/Company/GetAllMenu");
    }
    return {
        getAllCompany: getAllCompany,
        saveCompany: saveCompany,
        getAllMenu: getAllMenu
    }
}]);