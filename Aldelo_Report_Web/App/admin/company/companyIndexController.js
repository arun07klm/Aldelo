app.controller('companyIndexController', ["$scope", "companyService", "$location", function ($scope, companyService, $location) {
    var vm = this;
   $scope.companyList=[];

    $scope.getAllCompany = function () {
        companyService.getAllCompany().then(function (result) {
            $scope.companyList =result.data;
        });
    }
    vm.addNewCompany = function () {
        $location.path('company');
    }

  
}]);