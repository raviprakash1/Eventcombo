eventComboApp.service('broadcastService', ['$rootScope',
  function ($rootScope) {
    var broadcastMessage = function (message, param) {
      $rootScope.$broadcast(message, param);
    }

    var errorExternalLogin = function (param) {
      broadcastMessage('ErrorExternalLogin', param);
    }

    return {
      ErrorExternalLogin: errorExternalLogin
    }
  }]);