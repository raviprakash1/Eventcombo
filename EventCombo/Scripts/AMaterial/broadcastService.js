eventComboApp.service('broadcastService', ['$rootScope',
  function ($rootScope) {
    var broadcastMessage = function (message, param) {
      $rootScope.$broadcast(message, param);
    }

    var errorExternalLogin = function (param) {
      broadcastMessage('ErrorExternalLogin', param);
    }

    var callLogin = function (param) {
      broadcastMessage('CallLogin', param);
    }

    var completeExternalLogin = function (param) {
      broadcastMessage('CompleteExternalLogin', param);
    }

    var setLocation = function (param) {
      broadcastMessage('SetLocation', param);
    }

    return {
      ErrorExternalLogin: errorExternalLogin,
      CallLogin: callLogin,
      CompleteExternalLogin: completeExternalLogin,
      SetLocation: setLocation
    }
  }]);