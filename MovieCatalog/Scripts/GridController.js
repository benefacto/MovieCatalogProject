var GridController = function ($scope, $http) {
    var endpoint = "http://localhost:51893/Service.svc/movie";

    var init = function () {
        $http.get(endpoint)
            .then(function (response) {
                $scope.gridOptions.data =
                    angular.fromJson(response.data.GetMoviesResult);
            }, function (response) {
                alert("Error getting movies: " + response.data.GetMoviesResult);
            });
    }

    var update = function (rowEntity) {
        var newMovie = rowEntity;
        var data = { "movieJson" : rowEntity };
        $http.put(endpoint, angular.toJson(data))
            .then(function (response) {
            }, function (response) {
                alert("Error updating movie: " + response.data.UpdateMovieResult);
            }).then(function () {
                init();
            });
    }

    $scope.gridOptions = {
        columnDefs: [{ name: 'id', enableCellEdit: false },
            { name: 'title', enableCellEdit: true },
            { name: 'director', enableCellEdit: true },
            { name: 'year', enableCellEdit: true },
            { name: 'runningTime', enableCellEdit: true }
        ],
        onRegisterApi: function (gridApi) {
            gridApi.edit.on.afterCellEdit($scope, function
                (rowEntity, colDef, newValue, oldValue) {
                if (newValue !== oldValue && $scope.recentEditedValue !== newValue) {
                    update(rowEntity);
                }
            });
        },
    }

    init();
};

GridController.$inject = ['$scope', '$http'];