app.factory("companyService", ["$http", "$q", function ($http, $q) {

    var getAllCompany = function () {
        return $http.get("/Company/GetAllCompany");
    }
    var saveCompany = function (company) {
        return $http.post("/Company/SaveCompany",company);
    }
    var updateCompany = function (company) {
        return $http.post("/Company/EditCompany", company);
    }
    var getAllMenu = function () {
        return $http.get("/Company/GetAllMenu");
    }
    var getCompanyById = function (id) {
        return $http.get("/Company/GetCompanyById/"+id);
    }
    var updatePasswordService = function (company) {
        return $http.post("/Company/UpdatePassword", company);
    }
    return {
        getAllCompany: getAllCompany,
        saveCompany: saveCompany,
        getAllMenu: getAllMenu,
        getCompanyById:getCompanyById,
        updateCompany:updateCompany,
        updatePasswordService:updatePasswordService
    }
}]);