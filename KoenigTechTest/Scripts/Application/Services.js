
//dataService
mainModule.service("dataService",
[
    "$http", function($http) {
        return {
            getPaymentSystems: function(callback) {
                $http.get('/Home/PaymentSystems').success(callback);
            }

//        self.getExchangers = function(give, get, callback) {
//            $http.get('/Home/Exchangers/')
//                    }
        }
    }
]);