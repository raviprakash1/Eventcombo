eventComboApp.factory('MenuService', function () {
  return {
    navigation: [
                { id: 1, text: 'Discover Events', href: '#', class: '' },
                { id: 2, text: 'Get the Buzz', href: '#', class: '' },
                { id: 3, text: 'Create Event', href: '#', class: 'createevent' }
    ],
    selctedMenu: 0
  };
});

eventComboApp.controller('SearchEventController', ['$scope', '$window', '$http', '$q', 'MenuService', '$mdDialog', '$mdMenu',
  function ($scope, $window, $http, $q, MenuService, $mdDialog, $mdMenu) {

    $scope.navigation = MenuService.navigation;
    var originatorEv;
    this.menuHref = "http://www.google.com/design/spec/components/menus.html#menus-specs";
    this.openMenu = function ($mdOpenMenu, ev) {
      originatorEv = ev;
      $mdOpenMenu(ev);
    };

    var w = angular.element($window);

    $scope.$watch(
			function () {
			  return $window.innerWidth;
			},
			function (value) {
			  $scope.windowWidth = value;
			},
			true
		);

    w.bind('resize', function () {
      $scope.$apply();
      if ($scope.windowWidth > 960) {
        $mdMenu.hide();
      }
    });




    $scope.eventString = '';
    $scope.selectedEvent = null;
    $scope.cityString = '';
    $scope.selectedCity = null;

    $scope.geocoords = {
      latitude: '40.712784',
      longitude: '-74.0059413'
    }

    if ($window.navigator.geolocation) {
      $window.navigator.geolocation.getCurrentPosition(function (pos) {
        $scope.geocoords.latitude = pos.coords.latitude;
        $scope.geocoords.longitude = pos.coords.longitude;
        console.log($scope.geocoords);
      });
    }

    console.log($scope.geocoords);

    $scope.foundCities = null;

    $scope.gmapsService = new google.maps.places.AutocompleteService();
    $scope.placeService = new google.maps.places.PlacesService(document.getElementById('SearchContainer').appendChild(document.createElement('div')));


    $scope.DiscoverByEvent = function () {
      if ($scope.eventString) {
        lat = $scope.geocoords.latitude;
        lng = $scope.geocoords.longitude;
        if ($scope.selectedEvent) {
          lat = $scope.selectedEvent.Latitude;
          lng = $scope.selectedEvent.Longitude;
        }
        $window.location = '/et/evt/evc/all/page/' + lat + '/' + lng + '/rel/none/' + encodeURIComponent($scope.eventString.substring(0,53));
      }
    }

    $scope.eventSearch = function (str) {
      return $http.get("/commonAPI/FilterEventsByTitle", { params: { title: $scope.eventString } }).then(function (response) {
        console.log(response);
        return response.data;
      });
    }

    $scope.citySearch = function (str) {
      var deferred = $q.defer();
      $scope.getCitySearchResults(str).then(
        function (predictions) {
          $scope.foundCities = predictions;
          deferred.resolve(predictions);
        }
      );
      return deferred.promise;
    }

    $scope.getCitySearchResults = function (str) {
      var deferred = $q.defer();
      $scope.gmapsService.getPlacePredictions({ input: str, types: ['(cities)'] }, function (data) {
        deferred.resolve(data);
      });
      return deferred.promise;
    }

    $scope.DiscoverByCity = function () {
      if ((!$scope.foundCities) || (!Array.isArray($scope.foundCities)) || (!$scope.foundCities.length))
        return;

      if (!$scope.selectedCity) {
        $scope.selectedCity = $scope.foundCities[0];
      }

      $scope.placeService.getDetails({ placeId: $scope.selectedCity.place_id }, function (city) {
        $window.location = '/et/evt/evc/all/page/' + city.geometry.location.lat() + '/' + city.geometry.location.lng() + '/rel/none';
      })
    }

  }]);

eventComboApp.directive('pressEnter', function () {
  return {
    restrict: 'A',
    link: function (scope, element, attrs) {
      element.bind("keydown keypress", function (event) {
        if (event.which === 13) {
          scope.$apply(function () {
            scope.$eval(attrs.pressEnter);
          });
          event.preventDefault();
        }
      });
    }
  }
});

