eventComboApp.factory('MenuService', function () {
  return {
    navigation: [
                { id: 1, text: 'Discover Events', link: '/home/discoverevents', class: '', loginlink: '' },
                { id: 2, text: 'Get the Buzz', link: '/article/buzz', class: '', loginlink: '' },
                { id: 3, text: 'Create Event', link: '/eventmanagement/createevent', class: 'createevent', loginlink: '/eventmanagement/createevent' }
    ],
    selectedMenu: 0
  };
});

eventComboApp.controller('HamburgerController', ['$scope', '$window', 'MenuService', '$mdDialog', '$mdMenu', 'broadcastService', 'accountService',
  function ($scope, $window, MenuService, $mdDialog, $mdMenu, broadcastService, accountService) {

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

    $scope.$on('LoggedIn', function (event, param) {
      var paramArray = param.split('_');
      var link = paramArray[paramArray.length - 1];
      if (paramArray[0] === 'HeaderHamburger') {
        broadcastService.ReloadPage(link);
      }
    });

    $scope.clickLink = function (link, e) {
      if (!link || $scope.userRegistered)
        return;
      accountService.StartLogin('HeaderHamburger_' + link);
      e.preventDefault();
    }

  }]);


eventComboApp.controller('SearchEventController', ['$scope', '$window', '$http', '$q', '$cookies', 'broadcastService', 'geoService', 
  function ($scope, $window, $http, $q, $cookies, broadcastService, geoService) {

    $scope.eventString = '';
    $scope.selectedEvent = null;
    $scope.cityString = '';
    $scope.selectedCity = null;
    $scope.foundCities = [];

    $scope.gmapsService = new google.maps.places.AutocompleteService();
    $scope.placeService = new google.maps.places.PlacesService(document.getElementById('search').appendChild(document.createElement('div')));

    $scope.setCity = function (cityname) {
      $scope.cityString = cityname;
    }

    function setCityByCoordinates(lat, lng) {
      geoService.GetCityByCoordinates(lat, lng, $scope.setCity);
    }

    $scope.$on('CurrentCoordinatesChanged', function (event, val) {
      $scope.cityString = geoService.GetCurrentCity($scope.setCity);
    });

    $scope.cityString = geoService.GetCurrentCity($scope.setCity);

    $scope.DiscoverByEvent = function () {
      var coords = geoService.GetCoordinates();
      if ($scope.eventString) {
        var data = {
          EventId: 0,
          RecordTypeId: 0,
          EventTitle: $scope.eventString,
          Latitude: coords.latitude,
          Longitude: coords.longitude,
        }
        if ($scope.selectedEvent) {
          data.EventId = $scope.selectedEvent.EventId;
          data.EventId = $scope.selectedEvent.EventId;
          data.RecordTypeId = $scope.selectedEvent.RecordTypeId;
        }

        $http.get('/home/SearchEvents', { params: { json: angular.toJson(data) } }).then(function (response) {
          if (response.data)
            $window.location = response.data;
        }, function (error) { });
      }
    }

    $scope.eventSearch = function (str) {
      return $http.get("/commonAPI/FilterEventsByTitle", { params: { title: $scope.eventString } }).then(function (response) {
        return response.data;
      });
    }

    $scope.citySearch = function (str) {
      var deferred = $q.defer();
      $scope.getCitySearchResults(str).then(
        function (predictions) {
          $scope.foundCities = predictions ? predictions : [];
          deferred.resolve($scope.foundCities);
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
        var coords = geoService.SetCoordinates(city.geometry.location.lat(), city.geometry.location.lng(), true);
        $window.location = '/et/evt/evc/all/page/' + coords.latitude + '/' + coords.longitude + '/rel/none';
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

