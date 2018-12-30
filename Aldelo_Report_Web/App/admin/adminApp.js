var app = angular.module('AdminApp', ['ngRoute','ui.bootstrap','ngMessages']);
app.config(function ($routeProvider, $locationProvider) {
    $locationProvider.hashPrefix('');
    $routeProvider
    .when('/', {
        redirectTo: function () {
            return '/companyList';
        }
    }).when('/companyList', {
        templateUrl: '/App/admin/company/companyIndex.html',
        controller: 'companyIndexController',
        controllerAs:"vm"
    }).when('/company', {
        templateUrl: '/App/admin/company/companyDialog.html',
        controller: 'companyController',
        controllerAs: "vm"
    }).when('/company/:id', {
        templateUrl: '/App/admin/company/companyDialog.html',
        controller: 'companyController',
        controllerAs: "vm"
    })
    //function
    //$locationProvider.html5Mode(false).hashPrefix('!');
})