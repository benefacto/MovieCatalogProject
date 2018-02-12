var GridController = function ($scope) {
    $scope.init = function () {
        console.log("init was fired");
        $scope.movies = // TO-DO: Make GET request to service for data
            [
                {
                    "id": "818fd4ec-2919-43f1-b515-1b3afe083dd9",
                    "title": "Andrei Rublev",
                    "director": "Andrei Tarkovsky",
                    "year": "1966",
                    "runningTime": "183"
                },
                {
                    "id": "8710636f-50f7-49cb-9100-7067e72d8ade",
                    "title": "Solaris",
                    "director": "Andrei Tarkovsky",
                    "year": "1972",
                    "runningTime": "98"
                },
                {
                    "id": "5c63eefb-8843-4214-a38a-93e15dd826cb",
                    "title": "Stalker",
                    "director": "Andrei Tarkovsky",
                    "year": "1979",
                    "runningTime": "161"
                },
                {
                    "id": "9e099dbd-c3f3-4b8a-8278-74c127190468",
                    "title": "Nostalghia",
                    "director": "Andrei Tarkovsky",
                    "year": "1983",
                    "runningTime": "125"
                },
                {
                    "id": "3f18a7c6-60a9-48a8-a089-1e8680121a51",
                    "title": "The Sacrifice",
                    "director": "Andrei Tarkovsky",
                    "year": "1986",
                    "runningTime": "142"
                }
            ];
    }

    // TO-DO: Make sure event is firing (does not seem to be doing so currently)
    $scope.gridOptions = {
        onRegisterApi: function (gridApi) {
            gridApi.edit.on.afterCellEdit($scope, function (rowEntity, colDef, newValue, oldValue) {
                console.log("afterCellEdit fired");
                if (newValue !== oldValue && $scope.recentEditedValue !== newValue) {
                    // TO-DO: Make PUT request to service to update data
                }
                init();
            });
        }
    }
    $scope.init();
};

GridController.$inject = ['$scope'];