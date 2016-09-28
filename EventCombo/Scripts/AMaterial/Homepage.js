eventComboApp.controller('HomeController', ['$scope', '$window', '$cookies', '$http', 'broadcastService',
                          'eventFavoriteService', 'socialService', 'geoService',
  function ($scope, $window, $cookies, $http, broadcastService, eventFavoriteService, socialService, geoService) {

    $scope.eventsList = {};
    $scope.displayVEPopups = 'block';
    $scope.popLoading = false;
    $scope.popInfoMessage = false;
    $scope.popHomeShareDialog = false;
    $scope.curCity = { longitude: 0.0, latitude: 0.0 };
    $scope.curEventType = 0;
    $scope.allCities = false;
    $scope.allEventTypes = false;
    $scope.curCityId = 0;
    $scope.shareEvent = null;
    $scope.bgImageStyle = {};


    $scope.showLoadingMessage = function (show, message) {
      $scope.popLoading = show;
      $scope.LoadingMessage = message;
    }

    $scope.showInfoMessage = function (show, message) {
      $scope.popInfoMessage = show;
      $scope.InfoMessage = message;
    }

    $scope.UpdateEventList = function () {
      $scope.showLoadingMessage(false, 'Update events info');
      var coords = geoService.GetCoordinates();
      $http.get('/home/GetEventsList', { params: { lat: coords.latitude, lng: coords.longitude } }).then(function (response) {
        $scope.showLoadingMessage(false, '');
        $scope.eventsList = response.data;
      }, function (error) {
        $scope.showLoadingMessage(false, '');
      });
    };

    $scope.UpdateEventList();
    broadcastService.SetCitySearchRedirect(false);

    $scope.ProcessAddedFavorite = function (val) {
      if (val.Processed && $scope.eventsList)
        $scope.eventsList.forEach(function (entry, index) {
          if (entry.EventId == val.EventId) {
            entry.UserFavorite = true;
          }
        });
    }

    $scope.$on('CurrentCoordinatesChanged', function (event, val) {
      $scope.UpdateEventList();
    });

    $scope.$on('AddFavoriteWithLoginProcessed', function (event, val) {
      $scope.UpdateEventList();
    });

    $scope.$on('AddFavoriteProcessed', function (event, val) {
      $scope.ProcessAddedFavorite(val);
    });


    $scope.CallDiscoveryEvents = function () {
      if ((($scope.curCity && $scope.curCity.latitude && $scope.curCity.longitude) || $scope.allCities) &&
           ($scope.curEventType || $scope.allEventTypes)) {
        var et = $scope.allEventTypes ? 'evt' : $scope.curEventType;
        var coords = $scope.allCities ? 'lat/lng' : $scope.curCity.latitude + '/' + $scope.curCity.longitude;
        if ($scope.curCity && $scope.curCity.latitude && $scope.curCity.longitude)
          geoService.SetCoordinates($scope.curCity.latitude, $scope.curCity.longitude, true);
        $window.location.href = '/et/' + et + '/evc/all/page/' + coords + '/rel/none';
      }
    };

    $scope.OnCityChange = function (lat, lng, id, el) {
      $scope.allCities = (Math.abs(lat) > 90) && (Math.abs(lng) > 180);
      $scope.curCity.latitude = $scope.allCities ? 0 : lat;
      $scope.curCity.longitude = $scope.allCities ? 0 : lng;
      $scope.curCityId = id;
      $scope.CallDiscoveryEvents();
      onCityClick(el);
    };

    $scope.OnEventTypeChange = function (et) {
      $scope.allEventTypes = et < 0;
      $scope.curEventType = $scope.allEventTypes ? 0 : et;
      $scope.CallDiscoveryEvents();
    }

    $scope.OnAddFavorite = function (ev) {
      if (ev.UserFavorite)
        return;
      eventFavoriteService.AddFavorite(ev.EventId);
    };

    $scope.OnShareEvent = function (ev) {
      $scope.shareEvent = ev;
      $scope.popHomeShareDialog = true;
      console.log($scope.shareEvent.EventPath);
    }

    $scope.FormatHashTags = function (tag1, tag2) {
      tag1 = tag1 ? tag1 : "";
      tag2 = tag2 ? tag2 : "";
      if ((tag1.length + tag2.length) == 0)
        return '';
      if ((tag1.length + tag2.length) > 24) {
        if ((tag1.length > 13) && (tag2.length > 13)) {
          tag1 = tag1.substr(0, 10) + '\u2026';
          tag2 = tag2.substr(0, 10) + '\u2026';
        } else if (tag1.length > 13) {
          tag1 = tag1.substr(0, 22 - tag2.length) + '\u2026'
        } else {
          tag2 = tag2.substr(0, 22 - tag1.length) + '\u2026'
        }
      }

      return '#' + tag1 + '\u00A0\u00A0#' + tag2;
    }

    $scope.InitBGImages = function (startImage, realImage) {
      if (startImage)
        $scope.bgImageStyle = { "background-image": "url(" + startImage + ")" }
      else
        $scope.bgImageStyle = { "background-image": "url(" + realImage + ")" }

      if (!startImage)
        return;

      var img = new Image();
      img.onload = function () {
        $scope.bgImageStyle = { "background-image": "url(" + img.src + ")" }
        $scope.$apply();
      }
      img.src = realImage;
    }


    $scope.FacebookShare = function (href) {
      socialService.FacebookShare(href);
    }

    $scope.TwitterShare = function (title, href) {
      socialService.TwitterShare(title, href);
    }

    $scope.LinkedInShare = function (title, href, desc) {
      socialService.LinkedInShare(title, href, desc);
    }
  }]);

function onCityClick(el) {
  $('.cityPicker').fadeOut();
  $('#' + el).children('.cityPicker').fadeIn('slow', function () {
    $('html,body').animate({
      scrollTop: $("#eventlist").offset().top - 100
    }, 'slow');
  });
}