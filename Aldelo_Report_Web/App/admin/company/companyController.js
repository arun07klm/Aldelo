app.controller('companyController', ["$scope", "companyService", "$location", "$routeParams", "$filter", function ($scope, companyService, $location, $routeParams,$filter) {
    var vm = this;
    vm.isEdit = false;
    vm.company = {};

    vm.saveCompany = function () {
        if ($scope.companyForm.$valid) {
            companyService.saveCompany(vm.company).then(function (res) {
                if (res.data) {
                    alert("Company saved successfully..!")
                    vm.cancel();
                }
            }, function (re) {
                alert("Failed to save new company..!")
            })
        }
    }
    vm.updateCompany = function () {
        if ($scope.companyForm.$valid) {
            companyService.updateCompany(vm.company).then(function (res) {
                if (res.data) {
                    alert("Company updated successfully..!")
                    vm.cancel();
                }
            }, function (re) {
                alert("Failed to update company..!")
            })
        }
    }
    if ($routeParams.id) {
        vm.isEdit = true;
        companyService.getCompanyById($routeParams.id).then(function (re) {
            vm.company = re.data;
            vm.company.passwordExpireOn = new Date(re.data.passwordExpireOn);
            var menuList = angular.copy(vm.company.menuListDto);
            if (re.data != null) {
                companyService.getAllMenu().then(function (res) {
                    vm.company.menuListDto = [];
                    angular.forEach(res.data, function (value, key) {
                        var menu = $filter("filter")(menuList, value.menuId, true);
                        if (menu.length > 0) {
                            value.isChecked = true;
                        }
                        vm.company.menuListDto.push(value);
                    });
                })
            }
        })
    } else {
        companyService.getAllMenu().then(function (res) {
            vm.company.menuListDto = res.data;
        })
    }

    vm.cancel = function () {
        $location.path('companyList');
    }

    
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd
        }
        if (mm < 10) {
            mm = '0' + mm
        }

        vm.mindate = yyyy + '-' + mm + '-' + dd;
   
}]);