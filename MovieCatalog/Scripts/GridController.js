var GridController = function ($scope, $http) {
    // TO-DO: Make GET request to service to obtain data
    var init = function () {
        $http.get('http://localhost:51893/Service.svc/movie')
            .then(function (data) {
                console.log(JSON.parse(data));
                return JSON.parse(data);
            }, function (data) {
                alert("error!");
            });
    }

    $scope.gridOptions = {
        enableCellEdit: true,
        data: init(),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
            gridApi.edit.on.afterCellEdit($scope, function (rowEntity, colDef, newValue, oldValue) {
                console.log("afterCellEdit fired");
                if (newValue !== oldValue && $scope.recentEditedValue !== newValue) {
                    // TO-DO: Get PUT request to service to update data working
                    $http.put('http://localhost:51893/Service.svc/movie/' +
                        JSON.stringify(rowEntity))
                        .then(function (data) {
                            return JSON.parse(data.GetMoviesResult);
                        }, function (data) {
                            alert("error!");
                        });
                }
            });
        }
    }
};

GridController.$inject = ['$scope', '$http'];