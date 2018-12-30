app.controller('companyController', ["$scope", "companyService","$location", function ($scope, companyService,$location) {
    var vm = this;

    vm.saveCompany = function () {
        companyService.saveCompany(vm.company).then(function (res) {
            alert("Company saved successfully..!")
            vm.cancel();
        }, function (re) {
            alert("Failed to save new company..!")
        })
    }
    vm.cancel = function () {
        $location.path('companyList');
     }
}]);