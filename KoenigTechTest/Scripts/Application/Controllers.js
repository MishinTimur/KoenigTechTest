
// ** paymentSystemsController **
mainModule.controller("paymentSystemsController",
[
    "$scope", "dataService", function ($scope, dataService) {

        var root = $scope.$root;

        $scope.isBusy = false;
        $scope.paymentSystems = [];

        $scope.setGive = function(sys) {
            $scope.giveId = sys.Id;
            sendCalculateExchangersMsg();
        }

        $scope.setGet = function(sys) {
            $scope.getId = sys.Id;
            sendCalculateExchangersMsg();
        }

        var sendCalculateExchangersMsg = function() {
            root.$broadcast('paymentsChanged', { give: $scope.giveId, get: $scope.getId });
        }

        var refresh = function () {
            $scope.isBusy = true;
            dataService.getPaymentSystems(function(data) {
                $scope.paymentSystems = data;
                $scope.isBusy = false;
            });
        }

        refresh();
    }
]);

// ** exchangersController **
mainModule.controller("exchangersController",
["$scope", "dataService", function($scope, dataService) {

        $scope.exchangers = [];

        $scope.amount = 1;
        $scope.calculateGive = null;
        $scope.payments = {};

        $scope.$on('paymentsChanged', function (event, args) {
            $scope.amount = 1;
            $scope.calculateGive = null;
            $scope.payments = { give: args.give, get: args.get };
            $scope.calculateExchangers();

        });

        $scope.calculateExchangers = function () {
            var get = $scope.payments.get;
            var give = $scope.payments.give;
            if (get == undefined || give == undefined)
                return;
            dataService.getExchangers(
            {
                give: give,
                get: get,
                amount: $scope.amount,
                calculateGive: $scope.calculateGive
            }, function(data) {
                $scope.exchangers = data;
            });
        }
    }
]);