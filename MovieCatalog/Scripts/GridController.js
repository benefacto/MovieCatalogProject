var GridController = function ($scope, $http) {
    var endpoint = "http://localhost:51893/Service.svc/movie";

    var init = function () {
        $http.get(endpoint)
            .then(function (response) {
                $scope.gridOptions.data = JSON.parse(response.data.GetMoviesResult);
            }, function (response) {
                alert("error!");
            });
    }

    var update = function (rowEntity) {
        // TO-DO: Get PUT request to service to update data working
        console.log(rowEntity);
        $http.put(endpoint + "/" + rowEntity.id, JSON.stringify(rowEntity))
            .then(function (response) {
                console.log(JSON.parse(response.data.UpdateMovieResult));
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