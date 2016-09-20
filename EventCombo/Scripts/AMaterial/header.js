﻿eventComboApp.factory('MenuService', function () {
  return {
    navigation: [
                { id: 1, text: 'Discover Events', link: '/home/discoverevents', class: '', loginlink: '' },
                { id: 2, text: 'Get the Buzz', link: '/article/buzz', class: '', loginlink: '' },
                { id: 3, text: 'Create Event', link: '/eventmanagement/createevent', class: 'createevent', loginlink: '/eventmanagement/createevent' }
    ],
    selectedMenu: 0
  };
});

eventComboApp.controller('HamburgerController', ['$scope', '$window', 'MenuService', '$mdDialog', '$mdMenu', 'broadcastService',
  function ($scope, $window, MenuService, $mdDialog, $mdMenu, broadcastService) {

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

    $scope.clickLink = function (link, e) {
      if (!link || $scope.userRegistered)
        return;
      broadcastService.CallLogin({ RedirectUrl: link });
      e.preventDefault();
    }

  }]);


eventComboApp.controller('SearchEventController', ['$scope', '$window', '$http', '$q', '$cookies', 'broadcastService', 
  function ($scope, $window, $http, $q, $cookies, broadcastService) {

    $scope.eventString = '';
    $scope.selectedEvent = null;
    $scope.cityString = '';
    $scope.selectedCity = null;

    $scope.geocoords = $cookies.getObject('ECCurrentCoordinates');
    if (!$scope.geocoords)
      $scope.geocoords = $cookies.getObject('ECGeoCoordinates');
    var cdate = new Date();
    cdate.setDate(cdate.getDate() + 365);
    if (!$scope.geocoords) {
      $scope.geocoords = {
        latitude: '40.712784',
        longitude: '-74.0059413'
      }
      if ($window.navigator.geolocation) {
        $window.navigator.geolocation.getCurrentPosition(function (pos) {
          $scope.geocoords = {
            latitude: pos.coords.latitude,
            longitude: pos.coords.longitude
          };
          $cookies.putObject('ECGeoCoordinates', $scope.geocoords, { path: "/", expires: cdate });
        }, function (err) {
          $cookies.putObject('ECGeoCoordinates', $scope.geocoords, { path: "/", expires: cdate });
        });
      }
      else {
        $cookies.putObject('ECGeoCoordinates', $scope.geocoords, { path: "/", expires: cdate });
      }
    }

    var cCoords = $cookies.getObject('ECCurrentCoordinates');
    if (!cCoords) {
      $cookies.putObject('ECCurrentCoordinates', $scope.geocoords, { path: "/" });
      broadcastService.CurrentCoordinatesChanged();
    }


    $scope.foundCities = [];

    $scope.gmapsService = new google.maps.places.AutocompleteService();
    $scope.placeService = new google.maps.places.PlacesService(document.getElementById('search').appendChild(document.createElement('div')));


    $scope.DiscoverByEvent = function () {
      if ($scope.eventString) {
        var lat = $scope.geocoords.latitude;
        var lng = $scope.geocoords.longitude;
        var srchStr = $scope.eventString;
        if ($scope.selectedEvent) {
          lat = $scope.selectedEvent.Latitude ? $scope.selectedEvent.Latitude : lat;
          lng = $scope.selectedEvent.Longitude ? $scope.selectedEvent.Longitude : lng;
          srchStr = $scope.selectedEvent.RecordTypeId == 0 ? srchStr.substring(0, 53) : srchStr.substring(0, 16);
        }
        $window.location = '/et/evt/evc/all/page/' + lat + '/' + lng + '/rel/none/' + encodeURIComponent(srchStr);
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
        $scope.geocoords.latitude = city.geometry.location.lat();
        $scope.geocoords.longitude = city.geometry.location.lng();
        $cookies.putObject('ECCurrentCoordinates', $scope.geocoords, { path: "/" });
        broadcastService.CurrentCoordinatesChanged();
        $window.location = '/et/evt/evc/all/page/' + $scope.geocoords.latitude + '/' + $scope.geocoords.longitude + '/rel/none';
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

