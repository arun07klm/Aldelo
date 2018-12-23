var app = angular.module('AdminApp', ['ngRoute']);
app.config(function ($routeProvider, $locationProvider) {
    $locationProvider.hashPrefix('');
    $routeProvider
    .when('/', {
        redirectTo: function () {
            return '/company';
        }
    }).when('/company', {
        templateUrl: '/App/admin/company/companyIndex.html',
        controller: 'companyIndexController'

    })
    //function
    //$locationProvider.html5Mode(false).hashPrefix('!');
})