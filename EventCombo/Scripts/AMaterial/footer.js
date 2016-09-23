eventComboApp.controller('FooterController', ['$scope', 'geoService',
  function ($scope, geoService) {

    $scope.cCoords = geoService.GetCoordinates();

    $scope.onCityClick = function (lat, lng) {
      console.log(lat);
      var coords = geoService.SetCoordinates(lat, lng, true);
      console.log(coords);
      debugger;
    }
  }]);