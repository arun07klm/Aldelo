var app = angular.module('MyApp', ['ngRoute']);
app.config(function ($routeProvider, $locationProvider) {
    $locationProvider.hashPrefix('');
    $routeProvider
    .when('/', {
        redirectTo: function () {
            return '/home';
        }
    })
    .when('/home', {
        templateUrl: 'App/home/homeHtml.html',
         controller: 'homeController'
      
    })
     .when('/cust', {
         templateUrl: 'App/cust/custHtml.html',
         controller: 'custController'
     })
     .when('/saleCategories', {
         templateUrl: 'App/sale/SaleCategory/saleCategory.html',
         controller: 'saleCategoryController'
     })
     .when('/saleMenuItem', {
         templateUrl: 'App/sale/SaleMenuItem/saleMenuItem.html',
         controller: 'saleMenuItemController'
     })
    .when('/fastestMoving', {
        templateUrl: 'App/sale/FastestMovingItem/FastestMovingItem.html',
        controller: 'fastestMovingItemController'
    })
    .when('/driverDelivery', {
        templateUrl: 'App/sale/DriverDelivery/DriverDelivery.html',
        controller: 'DriverDeliveryController'
    })
    .when('/totalDeiverCharge', {
        templateUrl: 'App/sale/TotalDeliveryCharge/TotalDeliveryCharge.html',
        controller: 'TotalDeliveryChargeController'
    })
    .when('/totalRefunds', {
        templateUrl: 'App/Sale/RefundAmount/RefundAmountDetails.html',
        controller: 'TotalRefundController'
    })
    .when('/totalpayouts', {
        templateUrl: 'App/Sale/TotalPayout/TotalPayout.html',
        controller: 'TotalPayoutController'
    })
    .when('/complementaryorder', {
        templateUrl: 'App/Sale/ComplementaryOrder/ComplementaryOrder.html',
        controller: 'ComplementaryOrder'
    })
    //function
    //$locationProvider.html5Mode(false).hashPrefix('!');
})
