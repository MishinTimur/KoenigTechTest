
//PaymentSystemsController
mainModule.controller("paymentSystemsController",
[
    "$scope", "dataService", function ($scope, dataService) {

        var root = $scope.$root;

        $scope.paymentSystems = [];

        $scope.setGive = function(sys) {
            $scope.Give = sys.Id;
            sendCalculateExchangersMsg();
        }

        $scope.setGet = function(sys) {
            $scope.Get = sys.Id;
            sendCalculateExchangersMsg();
        }

        var sendCalculateExchangersMsg = function() {
            root.$broadcast('calculateExchangersMsg', { give: $scope.Give, get: $scope.Get });
        }

        var refresh = function() {
            dataService.getPaymentSystems(function(data) {
                $scope.paymentSystems = data;
            });
        }

        refresh();
    }
]);

mainModule.controller("exchangersController",
["$scope", "dataService", function($scope, dataService) {

        $scope.exchangers = [];

        $scope.$on('calculateExchangersMsg', function (event, arg) {
            if (arg.give == undefined || arg.get == undefined)
                return;
            dataService.getExchangers(arg, function(data) {
                $scope.exchangers = data;
            });
        });
    }
]);