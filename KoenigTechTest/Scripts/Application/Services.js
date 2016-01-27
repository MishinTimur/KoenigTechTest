﻿
//dataService
mainModule.service("dataService",
[
    "$http", function($http) {
        return {
            getPaymentSystems: function(callback) {
                $http.get('/Home/PaymentSystems').success(callback);
            },


            getExchangers: function (params, callback) {
                $http({ method: 'GET', url: '/Home/Exchangers', params: {'give': params.give, 'get': params.get} }).success(callback);
            }

        }
    }
]);
