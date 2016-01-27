
//PaymentSystemsController
mainModule.controller("paymentSystemsController",
[
    "$scope", "dataService", function($scope, dataService) {
        $scope.paymentSystems = [];
        $scope.Give = {};
        $scope.Get = {};

        $scope.setGive = function(sys) {
            $scope.Give = sys;
        }

        $scope.setGet = function(sys) {
            $scope.Get = sys;
        }

        this.refresh = function() {
            dataService.getPaymentSystems(function(data) {
                $scope.paymentSystems = data;
            });
        }

        this.refresh();
    }
]);

mainModule.controller("exchangersController",
[
    "$scope", "dataService", function($scope, dataService) {
        
    }
]);