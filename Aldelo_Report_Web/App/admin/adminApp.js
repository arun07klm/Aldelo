var app = angular.module('AdminApp', ['ngRoute']);
app.config(function ($routeProvider, $locationProvider) {
    $locationProvider.hashPrefix('');
    $routeProvider
    .when('/', {
        redirectTo: function () {
            return '/home';
        }
    })
    //function
    //$locationProvider.html5Mode(false).hashPrefix('!');
})