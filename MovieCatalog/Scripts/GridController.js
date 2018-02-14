var GridController = function ($scope, $http) {
    var endpoint = "http://localhost:51893/Service.svc/movie";

    var init = function () {
        $http.get(endpoint)
            .then(function (response) {
                $scope.gridOptions.data = angular.fromJson(response.data.GetMoviesResult);
            }, function (response) {
                alert("error!");
            });
    }

    var update = function (rowEntity) {
        // TO-DO: Get PUT request to service to update data working
        var newMovie = rowEntity;
        var data = { "movieJson" : rowEntity };
        $http.put(endpoint, angular.toJson(data))
            .then(function (response) {
                console.log(angular.fromJson(response.data));
            }, function (response) {
                alert("error!");
            });
    }

    $scope.gridOptions = {
        enableCellEdit: true,
        onRegisterApi: function (gridApi) {
            gridApi.edit.on.afterCellEdit($scope, function (rowEntity, colDef, newValue, oldValue) {
                console.log("afterCellEdit fired");
                if (newValue !== oldValue && $scope.recentEditedValue !== newValue) {
                    update(rowEntity);
                    //init();
                }
            });
        },
    }

    init();
};

GridController.$inject = ['$scope', '$http'];