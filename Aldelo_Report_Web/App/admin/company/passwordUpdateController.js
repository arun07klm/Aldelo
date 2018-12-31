app.controller('passwordUpdateController', ["$scope", "companyService", "$location", "$routeParams", "$filter", function ($scope, companyService, $location, $routeParams, $filter) {
    var vm = this;
    vm.company = {};

    vm.updatePassword = function () {
        if ($routeParams.id) {
            if ($scope.passwordUpdateForm.$valid) {
                vm.company.companyId = $routeParams.id;
                companyService.updatePasswordService(vm.company).then(function (res) {
                    if (res.data) {
                        alert("Password successfully updated...!")
                        vm.cancel();
                    } else {
                        alert("Password failed to update...!")
                    }
                }, function () {
                    alert("Password failed to update...!")
                })
            }
        }
    }
    vm.cancel = function () {
        $location.path('companyList');
    }
}])
