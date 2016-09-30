eventComboApp.service('geoService', ['$rootScope', '$cookies', '$window', 'broadcastService',
  function ($rootScope, $cookies, $window, broadcastService) {

    var geocoder = new google.maps.Geocoder();

    function getAddressPartByType(addresses, type) {
      if (!Array.isArray(addresses))
        return '';
      var res = {};
      addresses.forEach(function (addr) {
        var found = false;
        if (addr.types && Array.isArray(addr.types) && addr.types.some(function (val) {
          return val === type;
        })) {
          res = addr;
        }
      });
      return res;
    }

    var getCityByCoordinates = function (lat, lng, callback) {
      var latlng = new google.maps.LatLng(lat, lng);
      geocoder.geocode({
        'latLng': latlng
      }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
          var city = getAddressPartByType(results, 'locality');
          var cityname = getAddressPartByType(city.address_components, 'locality');
          var statename = getAddressPartByType(city.address_components, 'administrative_area_level_1');
          if (cityname && statename && callback)
            callback(cityname.long_name + ', ' + statename.short_name);
        }
      });
    }

    var getCoordinates = function () {
        if (typeof $cookies.getObject('ECGeoCoordinates') === 'undefined') {
            //no cookie
            return {
                latitude: '40.712784',
                longitude: '-74.0059413'
            }
        } else {
            //have cookie
            var coord = $cookies.getObject('ECGeoCoordinates');
            return {
                latitude: coord.latitude,
                longitude: coord.longitude
            }
        }
    }

    var saveCoordinates = function (lat, lng, preventBroadcast) {
      var geocoords = getCoordinates();
      if ((geocoords.latitude != lat) && (geocoords.longitude != lng)){
        geocoords.latitude = lat;
        geocoords.longitude = lng;
        var cdate = new Date();
        cdate.setDate(cdate.getDate() + 365);
        $cookies.putObject('ECGeoCoordinates', geocoords, { path: "/", expires: cdate });
        if (!preventBroadcast)
          broadcastService.CurrentCoordinatesChanged();
      }
      return geocoords;
    }

    var getCurrentCity = function (callback) {
      var coord = getCoordinates();
      return getCityByCoordinates(coord.latitude, coord.longitude, callback);
    }

    var geocoords = getCoordinates();

    if (!geocoords) {
      geocoords = {
        latitude: '40.712784',
        longitude: '-74.0059413'
      }
      if ($window.navigator.geolocation) {
        $window.navigator.geolocation.getCurrentPosition(function (pos) {
          saveCoordinates(pos.coords.latitude, pos.coords.longitude, false);
        }, function (err) {
          saveCoordinates(geocoords.latitude, geocoords.longitude, false);
        });
      }
      else {
        saveCoordinates(geocoords.latitude, geocoords.longitude, false);
      }
    }

    return {
      GetCoordinates: getCoordinates,
      SetCoordinates: saveCoordinates,
      GetCityByCoordinates: getCityByCoordinates,
      GetCurrentCity: getCurrentCity
    }

  }]);