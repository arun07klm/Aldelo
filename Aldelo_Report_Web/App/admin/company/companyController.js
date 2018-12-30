app.controller('companyController', ["$scope", "companyService", "$location", function ($scope, companyService, $location) {
    var vm = this;
    vm.company = {};
    vm.saveCompany = function () {
        if ($scope.companyForm.$valid) {
            companyService.saveCompany(vm.company).then(function (res) {
                alert("Company saved successfully..!")
                vm.cancel();
            }, function (re) {
                alert("Failed to save new company..!")
            })
        }
    }
    vm.getAllMenu = function () {
        companyService.getAllMenu().then(function (res) {
            vm.company.menuListDto = res.data;
        })
    }
    vm.cancel = function () {
        $location.path('companyList');
    }
}]);