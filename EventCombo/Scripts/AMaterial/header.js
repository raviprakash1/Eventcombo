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

    $scope.foundCities = [];
    $scope.allowRedirect = true;

    $scope.gmapsService = new google.maps.places.AutocompleteService();
    $scope.placeService = new google.maps.places.PlacesService(document.getElementById('search').appendChild(document.createElement('div')));

    $scope.setCity = function (cityname) {
      $scope.$broadcast('angucomplete-alt:changeInput', 'acCitySearch', cityname);
    }

    function setCityByCoordinates(lat, lng) {
      geoService.GetCityByCoordinates(lat, lng, $scope.setCity);
    }

    $scope.$on('CurrentCoordinatesChanged', function (event, val) {
      if ($scope.allowRedirect) {
        var coords = geoService.GetCoordinates();
        if (coords) 
          $window.location = '/et/evt/evc/all/page/' + coords.latitude + '/' + coords.longitude + '/rel/none';
      }
      else
        geoService.GetCurrentCity($scope.setCity);
    });

    $scope.$on('SetCitySearchRedirect', function (event, val) {
      $scope.allowRedirect = val;
    });

    geoService.GetCurrentCity($scope.setCity);

    $scope.DiscoverByEvent = function (item) {
      var coords = geoService.GetCoordinates();
      if (item.originalObject) {
        var data = {
          EventId: 0,
          RecordTypeId: 0,
          EventTitle: '',
          Latitude: coords.latitude,
          Longitude: coords.longitude,
        }
        if (typeof (item.originalObject) == 'object') {
          data.EventId = item.originalObject.EventId;
          data.EventTitle = item.originalObject.EventTitle;
          data.RecordTypeId = item.originalObject.RecordTypeId;
        }
        else if (typeof (item.originalObject) == 'string') {
          data.EventTitle = item.originalObject;
        }

        $http.get('/home/SearchEvents', { params: { json: angular.toJson(data) } }).then(function (response) {
          if (response.data)
            $window.location = response.data;
        }, function (error) { });
      }
    }

    $scope.eventSearch = function (str, timeoutPromise) {
      return $http.get("/commonAPI/FilterEventsByTitle", { params: { title: str } }).then(function (response) {
        return response.data;
      });
    }

    $scope.citySearch = function (str, timeoutPromise) {
      var deferred = $q.defer();
      $scope.getCitySearchResults(str).then(
        function (predictions) {
          $scope.foundCities = predictions ? predictions : [];
          $scope.foundCities.forEach(function (res) {
            if (res.terms.length > 1) {
              var pos = res.terms[res.terms.length - 1].offset;
              res.description = res.description.substring(0, pos - 2);
            }
          });
          deferred.resolve($scope.foundCities);
        }
      );
      return deferred.promise;
    }

    $scope.getCitySearchResults = function (str) {
      var deferred = $q.defer();
      $scope.gmapsService.getPlacePredictions({
        input: str, types: ['(cities)'], componentRestrictions: { country: 'us' }
      }, function (data) {
        deferred.resolve(data);
      });
      return deferred.promise;
    }

    $scope.DiscoverByCity = function (item) {
      if ((!$scope.foundCities) || (!Array.isArray($scope.foundCities)) || (!$scope.foundCities.length) || (!item) || (!item.originalObject))
        return;

      if (typeof (item.originalObject) == 'object') {
        $scope.placeService.getDetails({ placeId: item.originalObject.place_id }, function (city) {
          geoService.SetCoordinates(city.geometry.location.lat(), city.geometry.location.lng(), false);
        })
      } else if (typeof (item.originalObject) == 'string') {
        var promise = $scope.citySearch(item.originalObject);
        promise.then(function (result) {
          if (Array.isArray(result) && (result.length)) {
            $scope.placeService.getDetails({ placeId: result[0].place_id }, function (city) {
              geoService.SetCoordinates(city.geometry.location.lat(), city.geometry.location.lng(), false);
            })
          }
        })
      }
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


