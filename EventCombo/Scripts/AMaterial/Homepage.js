eventComboApp.controller('HomeController', ['$scope', '$window', '$cookies', '$http', 'broadcastService',
  function ($scope, $window, $cookies, $http, broadcastService) {

    $scope.eventsList = {};
    $scope.coords = { longitude: 0.0, latitude: 0.0 };
    $scope.displayVEPopups = 'block';
    $scope.popLoading = false;
    $scope.popInfoMessage = false;
    $scope.curCity = { longitude: 0.0, latitude: 0.0 };
    $scope.curEventType = 0;
    $scope.allCities = false;
    $scope.allEventTypes = false;
    $scope.curCityId = 0;


    $scope.showLoadingMessage = function (show, message) {
      $scope.popLoading = show;
      $scope.LoadingMessage = message;
    }

    $scope.showInfoMessage = function (show, message) {
      $scope.popInfoMessage = show;
      $scope.InfoMessage = message;
    }

    $scope.UpdateEventList = function () {
      var c = $cookies.getObject('ECCurrentCoordinates');
      console.log(c.latitude);
      console.log($scope.coords.latitude);
      if (!c || ((c.latitude == $scope.coords.latitude) && (c.longitude == $scope.coords.longitude))) {
        return;
      }

      $scope.showLoadingMessage(false, 'Update events info');
      $scope.coords = c;
      $http.get('/home/GetEventsList', { params: { lat: c.latitude, lng: c.longitude } }).then(function (response) {
        $scope.showLoadingMessage(false, '');
        $scope.eventsList = response.data;
      }, function (error) {
        $scope.showLoadingMessage(false, '');
      });
    };

    $scope.UpdateEventList();

    $scope.$on('CurrentCoordinatesChanged', function (val) {
      console.log("CurrentCoordinatesChanged");
      $scope.UpdateEventList();
    });

    $scope.CallDiscoveryEvents = function () {
      if ((($scope.curCity && $scope.curCity.latitude && $scope.curCity.longitude) || $scope.allCities) &&
           ($scope.curEventType || $scope.allEventTypes)) {
        var et = $scope.allEventTypes ? 'evt' : $scope.curEventType;
        var coords = $scope.allCities ? 'lat/lng' : $scope.curCity.latitude + '/' + $scope.curCity.longitude;
        if ($scope.curCity && $scope.curCity.latitude && $scope.curCity.longitude)
          $cookies.putObject('ECCurrentCoordinates', $scope.curCity, { path: "/" });
        $window.location.href = '/et/' + et + '/evc/all/page/' + coords + '/rel/none';
      }
    };

    $scope.OnCityChange = function (lat, lng, id) {
      $scope.allCities = (Math.abs(lat) > 90) && (Math.abs(lng) > 180);
      $scope.curCity.latitude = $scope.allCities ? 0 : lat;
      $scope.curCity.longitude = $scope.allCities ? 0 : lng;
      $scope.curCityId = id;
      $scope.CallDiscoveryEvents();
    };

    $scope.OnEventTypeChange = function (et) {
      $scope.allEventTypes = et < 0;
      $scope.curEventType = $scope.allEventTypes ? 0 : et;
      $scope.CallDiscoveryEvents();
    }

  }]);