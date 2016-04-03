angular.module("umbraco")
       .controller("CodeGecko.uLogDash", ["$scope", function ($scope) {

           $scope.load = function () {

           }

           $http.get("")
                .then(function (data) {
                    $scope.loaded = true;
                    $Scope.logEntries = data;
        })

}]);